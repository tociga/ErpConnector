using ErpConnector.Common.ErpTasks;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common.ExternalTask;
using ErpConnector.Common.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common
{
    public class TaskExecute
    {
        private BlockingCollection<Task> Steps { get; set; }
        private List<BlockingCollection<Task>> Queues = new List<BlockingCollection<Task>>();
        public TaskExecute(List<ErpTaskStep> steps, int numberOfQueues, int actionId, DateTime date)
        {
            Steps = new BlockingCollection<Task>();
            if (numberOfQueues > 1)
            {                
                ErpTaskStep.ErpTaskStepComparer c = new ErpTaskStep.ErpTaskStepComparer();
                steps.Sort(c);
                for (int i = 0; i < numberOfQueues; i++)
                {
                    Queues.Add(new BlockingCollection<Task>());
                }

                foreach (var step in steps)
                {
                    //foreach(var queue in Queues)
                    //{
                    Steps.Add(new Task(() => ExecuteTask(actionId, step, date)));
                    //}
                }
                Steps.CompleteAdding();
            }
            else
            {
                // No parallel runs.
                foreach(var step in steps)
                {
                    ExecuteTask(actionId, step, date);
                    System.Threading.Thread.Sleep(2000);
                }
                Steps.CompleteAdding();
            }
        }

        public void Execute()
        {
            foreach (var queue in Queues)
            {
                TryGetTask(Steps, queue);
                Task.Factory.StartNew(() => RunParallelQueue(Steps, queue));
                //queue.CompleteAdding();
            }
            Wait();
        }

        private void Wait()
        {
            
            while (!Steps.IsCompleted)
            {
                System.Threading.Thread.Sleep(1000);
            }

            if (Queues.Any())
            {
                int activeQueueCount = Queues.Select(x => Convert.ToInt32(!x.IsCompleted)).Sum();
                while (activeQueueCount > 0)
                {
                    activeQueueCount = Queues.Select(x => Convert.ToInt32(!x.IsCompleted)).Sum();
                    System.Threading.Thread.Sleep(200);
                }
            }
        }

        private static void TryGetTask(BlockingCollection<Task> taskList, BlockingCollection<Task> parallelCollection)
        {
            try
            {
                if (!taskList.IsCompleted)
                {
                    parallelCollection.Add(taskList.Take());
                }
                else
                {
                    parallelCollection.CompleteAdding();
                }
            }
            catch (Exception)
            {
            }
        }


        private static void RunParallelQueue(BlockingCollection<Task> taskList, BlockingCollection<Task> collection)
        {
            foreach (Task task in collection.GetConsumingEnumerable())
            {
                task.Start();
                task.Wait();
                TryGetTask(taskList, collection);
            }
        }

        private AxBaseException ExecuteTask(int actionId, ErpTaskStep erpStep, DateTime date)
        {
            if (!string.IsNullOrEmpty(erpStep.ExternalProcess))
            {
                DateTime start = DateTime.Now;
                var baseEx= ExternalTaskExec.ExecTask(erpStep);
                DataWriter.LogErpActionStep(actionId, erpStep.StepName, start, baseEx == null, baseEx == null ? null : baseEx.ApplicationException.InnerException.Message,
                    baseEx == null ? null : baseEx.ApplicationException.InnerException.StackTrace);
                return baseEx;
            }
            else
            {
                DataWriter.TruncateSingleTable(erpStep.DbTable);
                if (erpStep.TaskType == ErpTaskStep.ErpTaskType.ODATA_ENDPOINT)
                {
                    if (erpStep.MaxPageSize.HasValue)
                    {
                        MethodInfo method = typeof(ServiceConnector).GetMethod("CallOdataEndpointWithPageSize");
                        MethodInfo generic = method.MakeGenericMethod(erpStep.ReturnType);

                        Object[] parameters = new Object[5];
                        parameters = new object[] { erpStep.EndPoint, erpStep.MaxPageSize.Value, erpStep.DbTable, actionId, Authenticator.GetAuthData(erpStep.AuthenitcationType) };
                        generic.Invoke(null, parameters);
                    }
                    else
                    {

                        Type genericType = erpStep.GenericObjectType.MakeGenericType(erpStep.ReturnType);
                        MethodInfo method = typeof(ServiceConnector).GetMethod("CallOdataEndpoint");
                        MethodInfo generic = method.MakeGenericMethod(genericType, erpStep.ReturnType);

                        Object[] parameters = new Object[5];
                        parameters = new object[] { erpStep.EndPoint, erpStep.EndpointFilter, erpStep.DbTable, actionId, Authenticator.GetAuthData(erpStep.AuthenitcationType) };
                        generic.Invoke(null, parameters);
                    }
                }
                else if (erpStep.TaskType == ErpTaskStep.ErpTaskType.CUSTOM_SERVICE)
                {
                    MethodInfo method = typeof(ServiceConnector).GetMethod("CallService");
                    MethodInfo generic = method.MakeGenericMethod(erpStep.ReturnType);
                    generic.Invoke(null, new Object[6] { actionId, erpStep.ServiceMethod, erpStep.ServiceName, erpStep.DbTable, erpStep.MaxPageSize, Authenticator.GetAuthData(erpStep.AuthenitcationType) });
                }
                else if (erpStep.TaskType == ErpTaskStep.ErpTaskType.CUSTOM_SERVICE_BY_DATE)
                {
                    MethodInfo method = typeof(ServiceConnector).GetMethod("CallServiceByDate");
                    MethodInfo generic = method.MakeGenericMethod(erpStep.ReturnType);
                    Func<DateTime, DateTime> action = null;
                    switch (erpStep.PeriodIncrement)
                    {
                        case ErpTaskStep.PeriodIncrementType.HOURS:
                            {
                                action = delegate (DateTime d) { return d.AddHours(1); };
                                break;
                            }
                        case ErpTaskStep.PeriodIncrementType.DAYS:
                            {
                                action = delegate (DateTime d) { return d.AddDays(1); };
                                break;
                            }
                        case ErpTaskStep.PeriodIncrementType.MONTHS:
                            {
                                action = delegate (DateTime d) { return d.AddMonths(1); };
                                break;
                            }
                        default:
                            {
                                action = null;
                                break;
                            }

                    }
                    Object[] parameters = new Object[7] { date, actionId, erpStep.ServiceMethod, erpStep.ServiceName, erpStep.DbTable, Authenticator.GetAuthData(erpStep.AuthenitcationType), action };
                    generic.Invoke(null, parameters);
                }
                else if ( erpStep.TaskType == ErpTaskStep.ErpTaskType.ITERATIVE_ENDPOINT)
                {
                    var list = DataWriter.GetIdsFromEntities(erpStep.BaseTypeProcedure);
                    foreach (var id in list)
                    {
                        string endpoint = erpStep.EndPoint.Replace("{key}", id);
                        Type genericType = erpStep.GenericObjectType.MakeGenericType(erpStep.ReturnType);
                        MethodInfo method = typeof(ServiceConnector).GetMethod("CallOdataEndpoint");
                        MethodInfo generic = method.MakeGenericMethod(genericType, erpStep.ReturnType);

                        Object[] parameters = new Object[5];
                        var authData = Authenticator.GetAuthData(erpStep.AuthenitcationType);
                        authData.InjectionPropertyName = erpStep.InjectionPropertyName;
                        authData.InjectionPropertyValue = id;
                        parameters = new object[] { endpoint, erpStep.EndpointFilter, erpStep.DbTable, actionId, authData };
                        generic.Invoke(null, parameters);
                    }
                }
                return null;
            }
        }

    }
}
