﻿using ErpConnector.Common.AGREntities;
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
    }
}
