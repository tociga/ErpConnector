using AxConCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpDTO.Microsoft.Dynamics.DataEntities;
using ErpDTO.DTO;

namespace AxConnect.Modules
{
    public class POTransfer
    {
        public static void GetPosAndTos( Resources context)
        {
            //var pol = AXServiceConnector.CallOdataEndpoint<PurchaseOrderHeader>("PurchaseOrderHeaders", null, header).Result;
            //var poh = context.PurchaseOrderHeaders.ToList();
            //DataAccess.DataWriter.WriteToTable<PurchaseOrderHeader>(poh.GetDataReader(), "[ax].[PurchaseOrderHeaders]");
            //PullPoHeaders(context, 1000);

            //PullPoLines(context, 1000);

            PullPurchLines();

            PullTOTable();

            PullTOLines();

        }

        private static void PullPurchLines()
        {
            long nextRecId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[PurchLine]");
            bool hasData = WriteData<PurchLinesDTO>(nextRecId, 10000, "GetPurchLine", "AGRInventTransService", "[PurchLine]");
            while (hasData)
            {
                nextRecId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[PurchLine]");
                hasData = WriteData<PurchLinesDTO>(nextRecId, 10000, "GetPurchLine", "AGRInventTransService", "[PurchLine]");
            }

        }
        private static void PullPoLines(Resources context, int pageSize)
        {
            var pol = context.PurchaseOrderLines.Take(pageSize).ToList();
            bool foundData = pol.Any();
            for(int i = 1; foundData; i++)
            {
                DataAccess.DataWriter.WriteToTable<PurchaseOrderLine>(pol.GetDataReader(), "[ax].[PurchaseOrderLines]");
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
                DataAccess.DataWriter.WriteToTable<PurchaseOrderHeader>(poh.GetDataReader(), "[ax].[PurchaseOrderHeaders]");
                poh.Clear();
                poh = context.PurchaseOrderHeaders.Skip(i * pageSize).Take(pageSize).ToList();
                foundData = poh.Any();
            }
        }

        private static void PullTOLines()
        {
            long nextRecId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSFERLINE]");
            bool hasData = WriteData<InventTransferLineDTO>(nextRecId, 10000, "GetInventTransferLines", "AGRItemCustomService", "[INVENTTRANSFERLINE]");
            while(hasData)
            {
                nextRecId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSFERLINE]");
                hasData = WriteData<InventTransferLineDTO>(nextRecId, 10000, "GetInventTransferLines", "AGRItemCustomService", "[INVENTTRANSFERLINE]");
            }

        }

        private static void PullTOTable()
        {
            long nextRecId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSFERTABLE]");
            bool hasData = WriteData<InventTransferTableDTO>(nextRecId, 10000, "GetInventTransferTable", "AGRItemCustomService", "[INVENTTRANSFERTABLE]");
            while (hasData)
            {
                nextRecId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSFERTABLE]");
                hasData = WriteData<InventTransferTableDTO>(nextRecId, 10000, "GetInventTransferTable", "AGRItemCustomService", "[INVENTTRANSFERTABLE]");
            }
        }

        public static bool WriteData<T>(long recId, long pageSize, string webMethod, string service, string destTable)
        {
            AXServiceConnector connector = new AXServiceConnector();
            string postData = "{ \"lastRecId\": " + recId.ToString() + ", \"pageSize\" : " + (pageSize).ToString() + "}";
            var result = AXServiceConnector.CallAGRServiceArray<T>(service, webMethod, postData, null);

            var reader = result.Result.GetDataReader();

            DataAccess.DataWriter.WriteToTable<T>(reader, "[ax]." + destTable);

            return result.Result.Any();

        }
    }
}
