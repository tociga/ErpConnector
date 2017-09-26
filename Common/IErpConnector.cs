using ErpConnector.Common.AGREntities;
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
        AxBaseException FullTransfer();
        //void GetPoTo();
        //string GetDBScript(string entity);
        //void GetBom();
        AxBaseException DailyRefresh(DateTime date);
        AxBaseException PimFull();
        AxBaseException TransactionRefresh(DateTime date);
        AxBaseException TransactionFull();
        AxBaseException CreatePoTo(List<POTOCreate> po_to_create);
        AxBaseException CreateItems(List<ItemToCreate> itemsToCreate);
    }
}
