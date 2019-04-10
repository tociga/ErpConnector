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
        sap
    }
    public interface IErpConnector
    {
        AxBaseException FullTransfer(int actionId);
        //void GetPoTo();
        //string GetDBScript(string entity);
        //void GetBom();
        AxBaseException DailyRefresh(DateTime date, int actionId);
        AxBaseException PimFull(int actionId);
        AxBaseException TransactionRefresh(DateTime date, int actionId);
        AxBaseException TransactionFull(int actionId);
        AxBaseException CreatePoTo(List<POTOCreate> po_to_create, int actionId);
        AxBaseException CreateItems(List<ItemToCreate> itemsToCreate, int actionId);
        AxBaseException UpdateProduct(int actionId);
<<<<<<< HEAD
        AxBaseException TaskList(int actionId, ErpTask erpTask, DateTime date);
        AxBaseException GetSingleTable(ErpTaskStep step, int actionId, DateTime date);
=======
        AxBaseException TaskList(int actionId, ErpTask erpTask, DateTime date, int? noParallelProcesses);
        AxBaseException GetSingleTable(ErpTaskStep step, int actionId, DateTime date);
        AxBaseException UpdateProductLifecycleState(List<AGRProductLifeCycleState> plcs, int actionId);
>>>>>>> erp_listener_ax_lss
    }
}
