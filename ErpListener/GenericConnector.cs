using ErpConnector.Common;
using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using ErpConnector.Common.AGREntities;
using System.Collections.Generic;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common.ErpTasks;
using ErpConnector.Ax;

namespace ErpConnector.Listener
{
    public class GenericConnector
    {
        IErpConnector factory;
        BlockingCollection<Task> readConnectorTasks;
        BlockingCollection<Task> writeConnectorTasks;
        public GenericConnector(string typeOfErp)
        {
            ErpType erpType;
            Enum.TryParse(typeOfErp, out erpType);
            switch (erpType)
            {
                case ErpType.ax:
                    factory = new AxODataConnector();
                    break;
                case ErpType.jira:
                    factory = new ErpGenericConnector();
                    break;
                case ErpType.sap:
                    break;
                default:
                    throw new NotImplementedException("ErpListener has not been implemented for ERP type: " + typeOfErp);
            }
            readConnectorTasks = new BlockingCollection<Task>();
            writeConnectorTasks = new BlockingCollection<Task>();
            Task.Factory.StartNew(() => StartReadConnector());
            Task.Factory.StartNew(() => StartWriteConnector());
        }

        private void StartReadConnector()
        {
            foreach(Task task in readConnectorTasks.GetConsumingEnumerable())
            {
                task.Start();
                task.Wait();
            }
        }

        private void StartWriteConnector()
        {
            foreach (Task task in writeConnectorTasks.GetConsumingEnumerable())
            {
                task.Start();
                task.Wait();
            }
        }

        public Task<AxBaseException> CreatePoTo(List<POTOCreate> po_to_create, int actionId)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.CreatePoTo(po_to_create, actionId));
            writeConnectorTasks.Add(task);
            return task;            
        }

        public Task<AxBaseException> CreateItem(int itemCreateBatchId, int actionId)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.CreateItems(itemCreateBatchId, actionId));
            writeConnectorTasks.Add(task);
            return task;
        }

        public Task<AxBaseException> ExecuteTask(ErpTask erpTask, int actionId, DateTime date, int? noParallelProcesses)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.TaskList(actionId, erpTask, date, noParallelProcesses));
            readConnectorTasks.Add(task);
            return task;
        }
        public Task<AxBaseException> GetSingleTable(ErpTaskStep step, int actionId, DateTime date)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.GetSingleTable(step, actionId, date));
            readConnectorTasks.Add(task);
            return task;
        }

        public Task<AxBaseException> UpdateProductLifecycleStatus(int actionId, int plcUdpdateId)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.UpdateProductLifecycleState(plcUdpdateId, actionId));
            writeConnectorTasks.Add(task);
            return task;
        }
        //public string GetDBScript(string entity)
        //{
        //    return factory.GetDBScript(entity);
        //}

        //public void GetBom()
        //{
        //    factory.GetBom();
        //}
    }
}
