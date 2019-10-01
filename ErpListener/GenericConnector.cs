using ErpConnector.Common;
using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using ErpConnector.Common.AGREntities;
using System.Collections.Generic;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common.ErpTasks;
using ErpConnector.Ax;
using ErpConnector.Sap;
using ErpConnector.Nav;
using ErpConnector.Jira;

namespace ErpConnector.Listener
{
    public class GenericConnector
    {
        private ErpGenericConnector factory;
        public GenericConnector(string typeOfErp, ErpGenericConnector.ErpTaskCompleted taskCompleted)
        {
            factory = GetConnector(typeOfErp);
            factory.ErpTaskCompletedEvent += taskCompleted;
        }


        private ErpGenericConnector GetConnector(string typeOfErp)
        {
            Enum.TryParse(typeOfErp, out ErpType erpType);
            switch (erpType)
            {
                case ErpType.ax:
                    return new AxODataConnector();
                case ErpType.jira:
                    return new JiraConnector();
                case ErpType.sap:
                    return new SAPDataConnector();
                case ErpType.nav:
                    return new NavConnector();
                default:
                    throw new NotImplementedException("ErpListener has not been implemented for ERP type: " + typeOfErp);
            }
        }

        public Task<int> CreatePoTo(List<POTOCreate> po_to_create, int actionId)
        {            
            Task<int> task = new Task<int>(() => factory.CreatePoTo(po_to_create, actionId));
            task.Start();
            return task;            
        }

        public Task<int> CreateItem(int itemCreateBatchId, int actionId)
        {
            Task<int> task = new Task<int>(() => factory.CreateItems(itemCreateBatchId, actionId));
            return task;
        }

        public Task<int> ExecuteTask(ErpTask erpTask, int actionId, DateTime date, int? noParallelProcesses)
        {
            Task<int> task = new Task<int>(() => factory.TaskList(actionId, erpTask, date, noParallelProcesses));
            return task;
        }
        public Task<int> GetSingleTable(ErpTaskStep step, int actionId, DateTime date)
        {
            Task<int> task = new Task<int>(()=> factory.GetSingleTable(step, actionId, date));
            return task;
        }

        public Task<int> UpdateProductLifecycleStatus(int actionId, int plcUdpdateId)
        {
            Task<int> task = new Task<int>(() => factory.UpdateProductLifecycleState(plcUdpdateId, actionId));
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
