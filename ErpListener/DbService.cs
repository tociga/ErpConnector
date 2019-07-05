using ErpConnector.Common.AGREntities;
using ErpConnector.Common.ErpTasks;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ErpConnector.Listener
{
    public class DbService
    {
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
                        var connector = new GenericConnector(erpType);


                        switch (action.action_type)
                        {
                            case "create_po_to":
                                DataWriter.UpdateActionStatus(action.id, 1, null);
                                var orders = DataWriter.GetPoToToCreate(action.reference_id);
                                var connectorTask = connector.CreatePoTo(orders, action.id);
                                connectorTask.ContinueWith((mark) => DataWriter.UpdateActionStatus(action.id, 2, mark)).Wait();
                                DataWriter.UpdateOrderStatus(action.reference_id);
                                break;
                            case "create_item":
                                if (includeBAndM)
                                {
                                    DataWriter.UpdateActionStatus(action.id, 1, null);
                                    connectorTask = connector.CreateItem(action.reference_id, action.id);
                                    connectorTask.ContinueWith((mark) => DataWriter.UpdateActionStatus(action.id, 2, mark)).Wait();
                                    //var options = itemsToCreate.Select(x => x.option_id).Distinct();
                                    //foreach (int option in options)
                                    //{
                                    //    UpdateCreatedProductStatus(action.reference_id, option, connectorTask.Result);
                                    //}
                                }
                                else
                                {
                                    DataWriter.UpdateActionStatus(action.id, 2, null);
                                }
                                break;
                            case "action_task":
                                DataWriter.UpdateActionStatus(action.id, 1, null);
                                var task = DataWriter.GetTask(action.reference_id);
                                connectorTask = connector.ExecuteTask(task, action.id, DataWriter.GetDateById(action.date_reference_id), action.no_parallel_process);
                                connectorTask.ContinueWith((mark) => DataWriter.UpdateActionStatus(action.id, 2, mark)).Wait();
                                EmailSender.SendEmail(action.id, action.created_at);
                                break;
                            case "single_table":
                                DataWriter.UpdateActionStatus(action.id, 1, null);
                                DateTime date = DateTime.MaxValue;
                                if (action.date_reference_id.HasValue)
                                {
                                    date = DataWriter.GetDateById(action.date_reference_id);
                                }
                                var step = DataWriter.GetStep(action.reference_id);
                                var a = connector.GetSingleTable(step, action.id, date);
                                DataWriter.UpdateActionStatus(action.id, 2, CreateBaseTaskException(a));
                                break;
                            case "update_plc":
                                DataWriter.UpdateActionStatus(action.id, 1, null);                                
                                connectorTask = connector.UpdateProductLifecycleStatus(action.id, action.reference_id);
                                var updateTask = connectorTask.ContinueWith((mark) => DataWriter.UpdateActionStatus(action.id, 2, mark));                                
                                break;
                            default:
                                DataWriter.UpdateActionStatus(action.id, 3, CreateBaseTaskException(new AxBaseException { ApplicationException = new Exception("Unknown action type =" + action.action_type) }));
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

        public async Task<AxBaseException> CreateBaseTaskException(AxBaseException a)
        {
            return a;
        }

    }


}
