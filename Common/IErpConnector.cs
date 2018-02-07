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
        AxBaseException FullTransfer(int actionId);
        //void GetPoTo();
        //string GetDBScript(string entity);
        //void GetBom();
        ErpConnectorException DailyRefresh(DateTime date, int actionId);
        ErpConnectorException PimFull(int actionId);
        ErpConnectorException TransactionRefresh(DateTime date, int actionId);
        ErpConnectorException TransactionFull(int actionId);
        ErpConnectorException CreatePoTo(List<POTOCreate> po_to_create, int actionId);
        ErpConnectorException CreateItems(List<ItemToCreate> itemsToCreate, int actionId);
        ErpConnectorException UpdateProduct(int actionId);
    }
}
