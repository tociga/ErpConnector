using System.Linq;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;
using ErpConnector.Ax.DTO;
using System;
using System.Text;

namespace ErpConnector.Ax.Modules
{
    public class POTransfer
    {
        public static void GetPosAndTos( Resources context, int actionId)
        {
            //var pol = AXServiceConnector.CallOdataEndpoint<PurchaseOrderHeader>("PurchaseOrderHeaders", null, header).Result;
            //var poh = context.PurchaseOrderHeaders.ToList();
            //DataWriter.WriteToTable<PurchaseOrderHeader>(poh.GetDataReader(), "[ax].[PurchaseOrderHeaders]");
            //PullPoHeaders(context, 5000);

            //PullPoLines(context, 5000);
            PullPurchLines(DateTime.MinValue, false, actionId);
            PullTOTable(actionId);
            PullTOLines(actionId);
        }

        private static void PullPurchLines(DateTime date, bool useDate, int actionId)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                long nextRecId = DataWriter.GetMaxRecId("[ax]", "[PurchLine]");
                bool hasData = true;
                while (hasData)
                {
                    hasData = WriteData<PurchLinesDTO>(nextRecId, 10000, "GetPurchLine", "AGRInventTransService", "[PurchLine]", date, useDate);
                    nextRecId = DataWriter.GetMaxRecId("[ax]", "[PurchLine]");
                }
                DataWriter.LogErpActionStep(actionId, "[ax].[PurchLine]", startTime, true);
            }
            catch(Exception)
            {
                DataWriter.LogErpActionStep(actionId, "[ax].[PurchLine]", startTime, false);
                throw;
            }
        }
        public static void RefreshPurchLines(DateTime date, int actionId)
        {            
            DateTime startTime = DateTime.Now;
            try
            {
                for (DateTime d = date.Date; d <= DateTime.Now.Date; d = d.AddDays(1))
                {
                    WriteData<PurchLinesDTO>(0, 10000, "GetPurchLineByDate", "AGRInventTransService", "[PurchLine_Increment]", d, true);
                }
                DataWriter.LogErpActionStep(actionId, "[ax].[PurchLine_Increment]", startTime, true);
            }
            catch (Exception)
            {
                DataWriter.LogErpActionStep(actionId, "[ax].[PurchLine_Increment]", startTime, false);
                throw;
            }

        }
        private static void PullTOLines(int actionId)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                long nextRecId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSFERLINE]");
                bool hasData = WriteData<InventTransferLineDTO>(nextRecId, 10000, "GetInventTransferLines", "AGRItemCustomService", "[INVENTTRANSFERLINE]", DateTime.MinValue, false);
                while (hasData)
                {
                    nextRecId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSFERLINE]");
                    hasData = WriteData<InventTransferLineDTO>(nextRecId, 10000, "GetInventTransferLines", "AGRItemCustomService", "[INVENTTRANSFERLINE]", DateTime.MinValue, false);
                }
                DataWriter.LogErpActionStep(actionId, "[ax].[INVENTTRANSFERLINE]", startTime, true);
            }
            catch(Exception)
            {
                DataWriter.LogErpActionStep(actionId, "[ax].[INVENTTRANSFERLINE]", startTime, false);
            }
        }
        private static void PullTOTable(int actionId)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                long nextRecId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSFERTABLE]");
                bool hasData = WriteData<InventTransferTableDTO>(nextRecId, 10000, "GetInventTransferTable", "AGRItemCustomService", "[INVENTTRANSFERTABLE]", DateTime.MinValue, false);
                while (hasData)
                {
                    nextRecId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSFERTABLE]");
                    hasData = WriteData<InventTransferTableDTO>(nextRecId, 10000, "GetInventTransferTable", "AGRItemCustomService", "[INVENTTRANSFERTABLE]", DateTime.MinValue, false);
                }
            }
            catch(Exception)
            {
                DataWriter.LogErpActionStep(actionId, "[ax].[INVENTTRANSFERTALBE]", startTime, true);
                throw;
            }
        }
        public static bool WriteData<T>(long recId, long pageSize, string webMethod, string service, string destTable, DateTime minDate, bool useDate = false)
        {

            StringBuilder sb = new StringBuilder();
            if (useDate)
            {
                sb.Append("{\"firstDate\" : \"" + minDate.ToString("yyyy-MM-dd HH:mm:ss") + "\"");
                sb.Append(", \"lastDate\" : \"" + minDate.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "\"}");
            }
            else
            {
                sb.Append("{ \"lastRecId\": " + recId.ToString() + ", \"pageSize\" : " + (pageSize).ToString() + " }");
            }
            var result = ServiceConnector.CallAGRServiceArray<T>(service, webMethod, sb.ToString(), null);

            var reader = result.Result.value.GetDataReader();

            DataWriter.WriteToTable<T>(reader, "[ax]." + destTable);

            return result.Result.value.Any();
        }

    }
}
