﻿using ErpConnector.Common;
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
                case ErpType.jira:
                    factory = new ErpGenericConnector();
                    break;
                case ErpType.sap:
                    factory = new SAPDataConnector();
                    break;
                case ErpType.nav:
                    factory = new NavConnector();
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
                // hvað gerist ef villa gerist hér.
            }
        }

        public Task<AxBaseException> CreatePoTo(List<POTOCreate> po_to_create, int actionId)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.CreatePoTo(po_to_create, actionId));
            connectorTasks.Add(task);
            return task;            
        }

        public Task<AxBaseException> CreateItem(int itemCreateBatchId, int actionId)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.CreateItems(itemCreateBatchId, actionId));
            connectorTasks.Add(task);
            return task;
        }

        public Task<AxBaseException> ExecuteTask(ErpTask erpTask, int actionId, DateTime date, int? noParallelProcesses)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.TaskList(actionId, erpTask, date, noParallelProcesses));
            connectorTasks.Add(task);
            return task;
        }
        public AxBaseException GetSingleTable(ErpTaskStep step, int actionId, DateTime date)
        {
            AxBaseException task = factory.GetSingleTable(step, actionId, date);
            return task;
        }

        public Task<AxBaseException> UpdateProductLifecycleStatus(int actionId, int plcUdpdateId)
        {
            Task<AxBaseException> task = new Task<AxBaseException>(() => factory.UpdateProductLifecycleState(plcUdpdateId, actionId));
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
