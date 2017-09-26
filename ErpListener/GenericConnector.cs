using ErpConnector.Ax;
using ErpConnector.Common;
using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using ErpConnector.Common.AGREntities;
using System.Collections.Generic;
using ErpConnector.Common.Exceptions;

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

        public Task<AxBaseException> DailyRefresh(DateTime date)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(()=>factory.DailyRefresh(date));
            connectorTasks.Add(task);
            return task;
        }

        public Task<AxBaseException> CreatePoTo(List<POTOCreate> po_to_create)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.CreatePoTo(po_to_create));
            connectorTasks.Add(task);
            return task;            
        }

        public Task<AxBaseException> PimFull()
        {
            Task<AxBaseException> task = new Task<AxBaseException>(()=>factory.PimFull());
            connectorTasks.Add(task);
            return task;
        }

        public Task<AxBaseException> FullTransfer()
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.FullTransfer());
            connectorTasks.Add(task);
            return task;
        }

        public Task<AxBaseException> TransactionFull()
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.TransactionFull());
            connectorTasks.Add(task);
            return task;
        }

        public Task<AxBaseException> TransfactionRefresh(DateTime date)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.TransactionRefresh(date));
            connectorTasks.Add(task);
            return task;
        }

        public Task<AxBaseException> CreateItem(List<ItemToCreate> itemsToCreate)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.CreateItems(itemsToCreate));
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
