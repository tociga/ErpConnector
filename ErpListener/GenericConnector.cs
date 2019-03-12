using ErpConnector.Ax;
using ErpConnector.Common;
using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using ErpConnector.Common.AGREntities;
using System.Collections.Generic;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common.ErpTasks;

namespace ErpConnector.Listener
{
    public class GenericConnector
    {
        IErpConnector factory;
        BlockingCollection<Task> connectorTasks;
        public GenericConnector(string typeOfErp)
        {
            ErpType erpType;
            Enum.TryParse(typeOfErp, out erpType);
            switch (erpType)
            {
                case ErpType.ax:
                    factory = new AxODataConnector();
                    break;
                case ErpType.sap:
                    break;
                default:
                    throw new NotImplementedException("ErpListener has not been implemented for ERP type: " + typeOfErp);
            }
            connectorTasks = new BlockingCollection<Task>();
            Task.Factory.StartNew(() => StartConnector());
        }

        private void StartConnector()
        {
            foreach(Task task in connectorTasks.GetConsumingEnumerable())
            {
                task.Start();
                task.Wait();
            }
        }

        public Task<AxBaseException> DailyRefresh(DateTime date, int actionId)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(()=>factory.DailyRefresh(date, actionId));
            connectorTasks.Add(task);
            return task;
        }

        public Task<AxBaseException> CreatePoTo(List<POTOCreate> po_to_create, int actionId)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.CreatePoTo(po_to_create, actionId));
            connectorTasks.Add(task);
            return task;            
        }

        public Task<AxBaseException> PimFull(int actionId)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(()=>factory.PimFull(actionId));
            connectorTasks.Add(task);
            return task;
        }

        public Task<AxBaseException> FullTransfer(int actionId)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.FullTransfer(actionId));
            connectorTasks.Add(task);
            return task;
        }

        public Task<AxBaseException> TransactionFull(int actionId)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.TransactionFull(actionId));
            connectorTasks.Add(task);
            return task;
        }

        public Task<AxBaseException> TransfactionRefresh(DateTime date, int actionId)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.TransactionRefresh(date, actionId));
            connectorTasks.Add(task);
            return task;
        }

        public Task<AxBaseException> CreateItem(List<ItemToCreate> itemsToCreate, int actionId)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.CreateItems(itemsToCreate, actionId));
            connectorTasks.Add(task);
            return task;
        }
        public Task<AxBaseException> UpdateProductAttributes(int actionId)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.UpdateProduct(actionId));
            connectorTasks.Add(task);
            return task;
        }

        public Task<AxBaseException> ExecuteTask(ErpTask erpTask, int actionId, DateTime date, int? noParallelProcesses)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.TaskList(actionId, erpTask, date, noParallelProcesses));
            connectorTasks.Add(task);
            return task;
        }
        public Task<AxBaseException> GetSingleTable(ErpTaskStep step, int actionId, DateTime date)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.GetSingleTable(step, actionId, date));
            connectorTasks.Add(task);
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
