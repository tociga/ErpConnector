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
        jira
    }
    public interface IErpConnector
    {
        AxBaseException CreatePoTo(List<POTOCreate> po_to_create,int actionId);
        AxBaseException CreateItems(int itemToCreateId, int actionId);
        AxBaseException TaskList(int actionId, ErpTask erpTask, DateTime date, int? noParallelProcesses);
        AxBaseException GetSingleTable(ErpTaskStep step, int actionId, DateTime date);
        AxBaseException UpdateProductLifecycleState(int plcUdateId, int actionId);
    }
}
