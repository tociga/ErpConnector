using ErpConnector.Ax.DTO;
using ErpConnector.Common.Util;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common;
using System;
using ErpConnector.Common.ErpTasks;

namespace ErpConnector.Ax.Modules
{
    public class SalesValueTransactions
    {
        public static AxBaseException WriteSalesValueTrans(int actionId)
        {
            return ServiceConnector.CallService<RetailTransactionTableDTO>(actionId, "GetRetailTrans", "AGRRetailTransService", "[ax].[RetailTransactionTable]", 5000, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365));
        }

        public static AxBaseException WriteSalesValueTransRefresh(DateTime minDate, int actiond)
        {
            return ServiceConnector.CallServiceByDate<RetailTransactionTableDTO>(minDate, actiond, "GetRetailTransByDate",
                        "AGRRetailTransService", "[ax].[RetailTransactionTable_Increment]",  Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365), delegate (DateTime d) { return d.AddHours(1); });
        }
        public static AxBaseException WriteSalesValueTransLines(int actionId)
        {
            return ServiceConnector.CallService<RetailTransactionSalesLinesDTO>(actionId, "GetRetailTransLines",
                        "AGRRetailTransService", "[ax].[RetailTransactionSalesLines]", 5000,  Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365));
        }

        public static AxBaseException WriteSalesValueTransLinesRefresh(DateTime minDate, int actiond)
        {
            Func<DateTime, DateTime> nextPeriod = delegate (DateTime d) { return d.AddHours(1); };
            return ServiceConnector.CallServiceByDate<RetailTransactionSalesLinesDTO>(minDate, actiond, "GetRetailTransLinesByDate",
                        "AGRRetailTransService", "[ax].[RetailTransactionSalesLines_Increment]",  Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365), nextPeriod);
        }

    }
}
