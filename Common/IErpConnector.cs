using ErpConnector.Common.AGREntities;
using ErpConnector.Common.ErpTasks;
using ErpConnector.Common.Exceptions;
using System;
using System.Collections.Generic;

namespace ErpConnector.Common
{
    public enum ErpType
    {
        ax,
        sap,
        jira,
        nav,
        ax_lss,
        ax_fm
    }
    public interface IErpConnector
    {
        int CreatePoTo(List<POTOCreate> po_to_create,int actionId);
        int CreateItems(int itemToCreateId, int actionId);
        int TaskList(int actionId, ErpTask erpTask, DateTime date, int? noParallelProcesses);
        int GetSingleTable(ErpTaskStep step, int actionId, DateTime date);
        int UpdateProductLifecycleState(int plcUdateId, int actionId);        
        void OnTaskCompleted(object sender, ErpTaskCompletedArgs args);
    }
}
