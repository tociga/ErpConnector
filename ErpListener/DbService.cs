using ErpConnector.Common;
using ErpConnector.Common.AGREntities;
using ErpConnector.Common.ErpTasks;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ErpConnector.Listener
{
    public class DbService
    {
        BlockingCollection<Task<int>> writeTasks;
        BlockingCollection<Task<int>> readTasks;
        public DbService()
        {
            writeTasks = new BlockingCollection<Task<int>>();
            readTasks = new BlockingCollection<Task<int>>();

            Task.Factory.StartNew(() => RunWriteTasks());
            Task.Factory.StartNew(() => RunReadTasks());
        }

        public void RunWriteTasks()
        {
            foreach(var task in writeTasks.GetConsumingEnumerable())
            {
                try
                {
                    task.Start();
                    task.Wait();
                }
                catch(Exception)
                {
                    DataWriter.UpdateActionStatus(task.Result, 3, task, null);
                }
            }
        }
        public void RunReadTasks()
        {
            foreach (var task in readTasks.GetConsumingEnumerable())
            {
                try
                {
                    task.Start();
                    task.Wait();
                }
                catch (Exception)
                {
                    DataWriter.UpdateActionStatus(task.Result, 3, task, null);
                }
            }
        }

        public int RetryFailedSteps(int taskId, GenericConnector connector, int actionId, DateTime date, int? noParallelProcess, int? retryAttempts)
        {
            if (retryAttempts.HasValue && retryAttempts.Value > 0)
            {
                int counter = 1;
                var steps = DataWriter.GetFailedSteps(taskId, actionId, 1);
                while (counter <= retryAttempts && steps.Count > 0)
                {
                    var task = connector.RetryTaskSteps(actionId, steps, date, noParallelProcess);
                    task.Start();
                    task.Wait();
                    counter++;
                    steps = DataWriter.GetFailedSteps(taskId, actionId, counter);
                }
            }
            return -1;
        }
        public bool? Sync()
        {
            try
            {
                bool includeBAndM = false;
                Boolean.TryParse(ConfigurationManager.AppSettings["includeBAndM"], out includeBAndM);
                var actions = DataWriter.GetPendingActions(includeBAndM);
                foreach (var action in actions)
                {
                    if (action.status == 0)
                    {
                        // Run transfer
                        var erpType = ConfigurationManager.AppSettings["erp_type"];
                        var connector = new GenericConnector(erpType, Factory_ErpTaskCompletedEvent);

                        switch (action.action_type)
                        {
                            case "create_po_to":
                                DataWriter.UpdateActionStatus(action.id, 1, null, null);
                                var orders = DataWriter.GetPoToToCreate(action.reference_id);
                                var connectorTask = connector.CreatePoTo(orders, action.id);
                                writeTasks.Add(connectorTask);
                                //connectorTask.ContinueWith((mark) => DataWriter.UpdateActionStatus(action.id, 2, mark)).Wait();
                                DataWriter.UpdateOrderStatus(action.reference_id);
                                break;
                            case "create_item":
                                if (includeBAndM)
                                {
                                    DataWriter.UpdateActionStatus(action.id, 1, null, null);
                                    connectorTask = connector.CreateItem(action.reference_id, action.id);
                                    writeTasks.Add(connectorTask);
                                    //connectorTask.ContinueWith((mark) => DataWriter.UpdateActionStatus(action.id, 2, mark)).Wait();
                                    //var options = itemsToCreate.Select(x => x.option_id).Distinct();
                                    //foreach (int option in options)
                                    //{
                                    //    UpdateCreatedProductStatus(action.reference_id, option, connectorTask.Result);
                                    //}
                                }
                                else
                                {
                                    DataWriter.UpdateActionStatus(action.id, 2, null, null);
                                }
                                break;
                            case "action_task":
                                DataWriter.UpdateActionStatus(action.id, 1, null, null);
                                var task = DataWriter.GetTask(action.reference_id);
                                DateTime startDate = DataWriter.GetDateById(action.date_reference_id);
                                connectorTask = connector.ExecuteTask(task, action.id, startDate, action.no_parallel_process);
                                readTasks.Add(connectorTask);
                                var retryTask = connectorTask.ContinueWith((y) => RetryFailedSteps(action.reference_id, connector, action.id, startDate, action.no_parallel_process, action.on_failure_retry_attempts));                                
                                retryTask.ContinueWith((x)=>EmailSender.SendEmail(action.id, action.created_at));
                                break;
                            case "single_table":
                                DataWriter.UpdateActionStatus(action.id, 1, null, null);
                                DateTime date = DateTime.MaxValue;
                                if (action.date_reference_id.HasValue)
                                {
                                    date = DataWriter.GetDateById(action.date_reference_id);
                                }
                                var step = DataWriter.GetStep(action.reference_id);
                                var a = connector.GetSingleTable(step, action.id, date);
                                readTasks.Add(a);
                                //DataWriter.UpdateActionStatus(action.id, 2, CreateBaseTaskException(a));
                                break;
                            case "update_plc":
                                DataWriter.UpdateActionStatus(action.id, 1, null, null);                                
                                connectorTask = connector.UpdateProductLifecycleStatus(action.id, action.reference_id);
                                writeTasks.Add(connectorTask);
                                break;
                            default:
                                DataWriter.UpdateActionStatus(action.id, 3, null, new AxBaseException { ApplicationException = new Exception("Unknown action type =" + action.action_type) });
                                break;
                        }
                    }
                }

            }
            catch(Exception e)
            {
                DataWriter.LogError(e.Message, e.StackTrace, this, e.HResult);
            }
            return true;
        }

        private void Factory_ErpTaskCompletedEvent(object sender, ErpTaskCompletedArgs args)
        {
            DataWriter.UpdateActionStatus(args.ActionId, args.Status, null, args.Exception);
        }

    }


}
