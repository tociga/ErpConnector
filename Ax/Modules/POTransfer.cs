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
        public static void GetPosAndTos( Resources context)
        {
            //var pol = AXServiceConnector.CallOdataEndpoint<PurchaseOrderHeader>("PurchaseOrderHeaders", null, header).Result;
            //var poh = context.PurchaseOrderHeaders.ToList();
            //DataWriter.WriteToTable<PurchaseOrderHeader>(poh.GetDataReader(), "[ax].[PurchaseOrderHeaders]");
            //PullPoHeaders(context, 5000);

            //PullPoLines(context, 5000);
            PullPurchLines(DateTime.MinValue, false);
            PullTOTable();
            PullTOLines();
        }

        public static void RefershPosAndTos(Resources context, DateTime date)
        {
            PullPoHeaders(context, 5000);
            PullPoLines(context, 5000);

            PullTOLines();
            PullTOTable();

            PullPurchLines(date, true);
        }

        private static void PullPurchLines(DateTime date, bool useDate)
        {
            long nextRecId = DataWriter.GetMaxRecId("[ax]", "[PurchLine]");
            bool hasData = true;
            while (hasData)
            {
                hasData = WriteData<PurchLinesDTO>(nextRecId, 10000, "GetPurchLine", "AGRInventTransService", "[PurchLine]", date, useDate);
                nextRecId = DataWriter.GetMaxRecId("[ax]", "[PurchLine]");
            }
        }
        public static void RefreshPurchLines(DateTime date)
        {            
            for (DateTime d = date.Date; d <= DateTime.Now.Date; d = d.AddDays(1))
            {
                WriteData<PurchLinesDTO>(0, 10000, "GetPurchLineByDate", "AGRInventTransService", "[PurchLine_Increment]", d, true);
            }
        }
        private static void PullPoLines(Resources context, int pageSize)
        {
            var pol = context.PurchaseOrderLines.Take(pageSize).ToList();
            bool foundData = pol.Any();
            for(int i = 1; foundData; i++)
            {
                DataWriter.WriteToTable<PurchaseOrderLine>(pol.GetDataReader(), "[ax].[PurchaseOrderLines]");
                pol.Clear();
                pol = context.PurchaseOrderLines.Skip(i * pageSize).Take(pageSize).ToList();
                foundData = pol.Any();
                System.GC.Collect();
                // Allow GC to work magic
            }
        }

        private static void PullPoHeaders(Resources context, int pageSize)
        {
            var poh = context.PurchaseOrderHeaders.Take(pageSize).ToList();
            bool foundData = poh.Any();
            for (int i = 1; foundData; i++)
            {
                DataWriter.WriteToTable<PurchaseOrderHeader>(poh.GetDataReader(), "[ax].[PurchaseOrderHeaders]");
                poh.Clear();
                poh = context.PurchaseOrderHeaders.Skip(i * pageSize).Take(pageSize).ToList();
                foundData = poh.Any();
            }
        }
        private static void PullTOLines()
        {
            long nextRecId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSFERLINE]");
            bool hasData = WriteData<InventTransferLineDTO>(nextRecId, 10000, "GetInventTransferLines", "AGRItemCustomService", "[INVENTTRANSFERLINE]", DateTime.MinValue, false);
            while(hasData)
            {
                nextRecId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSFERLINE]");
                hasData = WriteData<InventTransferLineDTO>(nextRecId, 10000, "GetInventTransferLines", "AGRItemCustomService", "[INVENTTRANSFERLINE]", DateTime.MinValue, false);
            }
        }
        private static void PullTOTable()
        {
            long nextRecId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSFERTABLE]");
            bool hasData = WriteData<InventTransferTableDTO>(nextRecId, 10000, "GetInventTransferTable", "AGRItemCustomService", "[INVENTTRANSFERTABLE]", DateTime.MinValue, false);
            while (hasData)
            {
                nextRecId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSFERTABLE]");
                hasData = WriteData<InventTransferTableDTO>(nextRecId, 10000, "GetInventTransferTable", "AGRItemCustomService", "[INVENTTRANSFERTABLE]", DateTime.MinValue, false);
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
