using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ErpConnector.Ax.Modules
{
    public class SalesValueTransactions
    {
        public static void WriteSalesValueTrans(int actionId)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                Int64 recId = DataWriter.GetMaxRecId("[ax]", "[RetailTransactionTable]");
                bool foundData = true;
                while (foundData)
                {
                    foundData = ServiceConnector.WriteFromService<RetailTransactionTableDTO>(recId, 5000, "GetRetailTrans", "AGRRetailTransService", "[RetailTransactionTable]", DateTime.MinValue, false);
                    recId = DataWriter.GetMaxRecId("[ax]", "[RetailTransactionTable]");
                }
                DataWriter.LogErpActionStep(actionId, "[ax].[RetailTransactionTable]", startTime, true);
            }
            catch (Exception)
            {
                DataWriter.LogErpActionStep(actionId, "[ax].[RetailTransactionTable]", startTime, false);
                throw;
            }

        }

        public static void WriteSalesValueTransRefresh(DateTime minDate, int actiond)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                for (DateTime d = minDate.Date; d <= DateTime.Now.Date; d = d.AddDays(1))
                {
                    ServiceConnector.WriteFromService<InventSumDTO>(0, 5000, "GetRetailTransByDate", "AGRRetailTransService", "[RetailTransactionTable_Increment]", d, true);
                }
                DataWriter.LogErpActionStep(actiond, "[ax].[RetailTransactionTable_Increment]", startTime, true);
            }
            catch (Exception)
            {
                DataWriter.LogErpActionStep(actiond, "[ax].[RetailTransactionTable_Increment]", startTime, false);
                throw;
            }
        }
        public static void WriteSalesValueTransLines(int actionId)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                Int64 recId = DataWriter.GetMaxRecId("[ax]", "[RetailTransactionSalesLines]");
                bool foundData = true;
                while (foundData)
                {
                    foundData = ServiceConnector.WriteFromService<RetailTransactionTableDTO>(recId, 5000, "GetRetailTransLines", "AGRRetailTransService", "[RetailTransactionSalesLines]", DateTime.MinValue, false);
                    recId = DataWriter.GetMaxRecId("[ax]", "[RetailTransactionSalesLines]");
                }
                DataWriter.LogErpActionStep(actionId, "[ax].[RetailTransactionTable]", startTime, true);
            }
            catch (Exception)
            {
                DataWriter.LogErpActionStep(actionId, "[ax].[RetailTransactionSalesLines]", startTime, false);
                throw;
            }

        }

        public static void WriteSalesValueTransLinesRefresh(DateTime minDate, int actiond)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                for (DateTime d = minDate.Date; d <= DateTime.Now.Date; d = d.AddDays(1))
                {
                    ServiceConnector.WriteFromService<InventSumDTO>(0, 5000, "GetRetailTransLinesByDate", "AGRRetailTransService", "[RetailTransactionSalesLines_Increment]", d, true);
                }
                DataWriter.LogErpActionStep(actiond, "[ax].[RetailTransactionSalesLines_Increment]", startTime, true);
            }
            catch (Exception)
            {
                DataWriter.LogErpActionStep(actiond, "[ax].[RetailTransactionTable_Increment]", startTime, false);
                throw;
            }

        }

    }
}
