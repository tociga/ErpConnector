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

        public Task<ErpConnectorException> DailyRefresh(DateTime date, int actionId)
        {
            Task<ErpConnectorException> task = new Task<ErpConnectorException>(()=>factory.DailyRefresh(date, actionId));
            connectorTasks.Add(task);
            return task;
        }

        public Task<ErpConnectorException> CreatePoTo(List<POTOCreate> po_to_create, int actionId)
        {
            Task<ErpConnectorException> task = new Task<ErpConnectorException>(() => factory.CreatePoTo(po_to_create, actionId));
            connectorTasks.Add(task);
            return task;            
        }

        public Task<ErpConnectorException> PimFull(int actionId)
        {
            Task<ErpConnectorException> task = new Task<ErpConnectorException>(()=>factory.PimFull(actionId));
            connectorTasks.Add(task);
            return task;
        }

        public Task<ErpConnectorException> FullTransfer(int actionId)
        {
            Task<ErpConnectorException> task = new Task<ErpConnectorException>(() => factory.FullTransfer(actionId));
            connectorTasks.Add(task);
            return task;
        }

        public Task<ErpConnectorException> TransactionFull(int actionId)
        {
            Task<ErpConnectorException> task = new Task<ErpConnectorException>(() => factory.TransactionFull(actionId));
            connectorTasks.Add(task);
            return task;
        }

        public Task<ErpConnectorException> TransfactionRefresh(DateTime date, int actionId)
        {
            Task<ErpConnectorException> task = new Task<ErpConnectorException>(() => factory.TransactionRefresh(date, actionId));
            connectorTasks.Add(task);
            return task;
        }

        public Task<ErpConnectorException> CreateItem(List<ItemToCreate> itemsToCreate, int actionId)
        {
            Task<ErpConnectorException> task = new Task<ErpConnectorException>(() => factory.CreateItems(itemsToCreate, actionId));
            connectorTasks.Add(task);
            return task;
        }

        public Task<ErpConnectorException> UpdateProductAttributes(int actionId)
        {
            Task<ErpConnectorException> task = new Task<ErpConnectorException>(() => factory.UpdateProduct(actionId));
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
