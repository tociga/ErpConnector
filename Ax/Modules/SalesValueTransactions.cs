using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;
using ErpConnector.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ErpConnector.Ax.Modules
{
    public class SalesValueTransactions
    {
        public static AxBaseException WriteSalesValueTrans(int actionId)
        {
            return ServiceConnector.CallService<RetailTransactionTableDTO>(actionId, "GetRetailTrans", "AGRRetailTransService", "[ax]", "[RetailTransactionTable]", 5000);
        }

        public static AxBaseException WriteSalesValueTransRefresh(DateTime minDate, int actiond)
        {
            return ServiceConnector.CallServiceByDate<RetailTransactionTableDTO>(minDate, actiond, "GetRetailTransByDate",
                        "AGRRetailTransService", "[ax]", "[RetailTransactionTable_Increment]", delegate (DateTime d) { return d.AddHours(1); });
        }
        public static AxBaseException WriteSalesValueTransLines(int actionId)
        {
            return ServiceConnector.CallService<RetailTransactionSalesLinesDTO>(actionId, "GetRetailTransLines",
                        "AGRRetailTransService", "[ax]", "[RetailTransactionSalesLines]", 5000);
        }

        public static AxBaseException WriteSalesValueTransLinesRefresh(DateTime minDate, int actiond)
        {
            Func<DateTime, DateTime> nextPeriod = delegate (DateTime d) { return d.AddHours(1); };
            return ServiceConnector.CallServiceByDate<RetailTransactionSalesLinesDTO>(minDate, actiond, "GetRetailTransLinesByDate",
                        "AGRRetailTransService", "[ax]", "[RetailTransactionSalesLines_Increment]", nextPeriod);
        }

    }
}
