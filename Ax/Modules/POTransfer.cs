﻿using System.Linq;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;
using ErpConnector.Ax.DTO;
using System;
using System.Text;
using ErpConnector.Common.Exceptions;

namespace ErpConnector.Ax.Modules
{
    public class POTransfer
    {
        public static AxBaseException GetPosAndTos( Resources context, int actionId)
        {
            //var pol = AXServiceConnector.CallOdataEndpoint<PurchaseOrderHeader>("PurchaseOrderHeaders", null, header).Result;
            //var poh = context.PurchaseOrderHeaders.ToList();
            //DataWriter.WriteToTable<PurchaseOrderHeader>(poh.GetDataReader(), "[ax].[PurchaseOrderHeaders]");
            //PullPoHeaders(context, 5000);

            //PullPoLines(context, 5000);
            var poLines = PullPurchLines(actionId);
            var poTable = PullPurchTable(actionId);
            var agrOrder = PullAGROrders(actionId);
            var agrOrderLine = PullPurchLines(actionId);
            //if (poLines != null)
            //{
            //    return poLines;
            //}
            var toTable = PullTOTable(actionId);
            //if (toTable != null)
            //{
            //    return toTable;
            //}
            var toLines = PullTOLines(actionId);
            //if (toLines != null)
            //{
            //    return toLines;
            //}
            return null;
        }

        private static AxBaseException PullPurchLines(int actionId)
        {
            return ServiceConnector.CallService<PurchLinesDTO>(actionId, "GetPurchLine", "AGRInventTransService", "[ax].[PurchLine]", 10000).Result;
        }
        public static AxBaseException RefreshPurchLines(DateTime date, int actionId)
        {
            return ServiceConnector.CallServiceByDate<PurchLinesDTO>(date, actionId, "GetPurchLineByDate", "AGRInventTransService", "[ax].[PurchLine_Increment]").Result;

        }
        private static AxBaseException PullTOLines(int actionId)
        {
            return ServiceConnector.CallService<InventTransferLineDTO>(actionId, "GetInventTransferLines", "AGRItemCustomService", "[ax].[INVENTTRANSFERLINE]", 5000).Result;
        }
        private static AxBaseException PullTOTable(int actionId)
        {
            return  ServiceConnector.CallService<InventTransferTableDTO>(actionId, "GetInventTransferTable", "AGRItemCustomService", "[ax].[INVENTTRANSFERTABLE]", 10000).Result;
        }

        public static AxBaseException PullPurchTable(int actionId)
        {
            return ServiceConnector.CallService<PurchTableDTO>(actionId, "GetPurchTable", "AGRInventTransService", "[ax].[PurchTable]", 10000).Result;
        }

        public static AxBaseException PullAGROrders(int actionId)
        {
            return ServiceConnector.CallOdataEndpoint<AGROrder>("AGROrders", null, "[ax].[AGROrderTable]", actionId).Result;
        }

        public static AxBaseException PullAGROrderLines(int actionId)
        {
            return ServiceConnector.CallOdataEndpoint<AGROrderLine>("AGROrderLines", null, "[ax].[AGROrderLine]", actionId).Result;
        }
    }
}
