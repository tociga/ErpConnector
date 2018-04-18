using System;

namespace ErpConnector.Ax.DTO
{
    public class InventTransDTO
    {
        public long RecId { get; set; }
        public string DataAreaId { get; set; }
        public long Partition { get; set; }
        public string ItemId { get; set; }
        public int StatusIssue { get; set; }
        public DateTime DatePhysical { get; set; }
        public decimal Qty { get; set; }
        public DateTime DateFinancial { get; set; }
        public string InventDimId { get; set; }
        public string InvoiceId { get; set; }
        public long InventTransOrigin { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int StatusReceipt { get; set; }
        public int RecVersion { get; set; }
        public string ActivityNumber { get; set; }
        public decimal CostAmountAdjustment { get; set; }
        public decimal CostAmountOperations { get; set; }
        public decimal CostAmountPhysical { get; set; }
        public decimal CostAmountPosted { get; set; }
        public decimal CostAmountSecCurAdjustment_RU { get; set; }
        public decimal CostAmountSecCurPhysical_RU { get; set; }
        public decimal CostAmountSecCurPosted_RU { get; set; }
        public decimal CostAmountSettled { get; set; }
        public decimal CostAmountSettledSecCur_RU { get; set; }
        public decimal CostAmountStd { get; set; }
        public decimal CostAmountStdSecCur_RU { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime DateClosed { get; set; }
        public DateTime DateClosedSecCur_RU { get; set; }
        public DateTime DateExpected { get; set; }
        public DateTime DateInvent { get; set; }
        public DateTime DateStatus { get; set; }
        public string GroupRefId_RU { get; set; }
        public int GroupRefType_RU { get; set; }
        public int InterCompanyInventDimTransferred { get; set; }
        public int InventDimFixed { get; set; }
        public string InventDimIdSales_RU { get; set; }
        public long InventTransOriginDelivery_RU { get; set; }
        public long InventTransOriginSales_RU { get; set; }
        public long InventTransOriginTransit_RU { get; set; }
        public int InvoiceReturned { get; set; }
        public long MarkingRefInventTransOrigin { get; set; }
        public string PackingSlipId { get; set; }
        public int PackingSlipReturned { get; set; }
        public decimal PdsCWQty { get; set; }
        public decimal PdsCWSettled { get; set; }
        public string PickingRouteID { get; set; }
        public string ProjAdjustRefId { get; set; }
        public string ProjCategoryId { get; set; }
        public string ProjId { get; set; }
        public decimal QtySettled { get; set; }
        public decimal QtySettledSecCur_RU { get; set; }
        public long ReturnInventTransOrigin { get; set; }
        public decimal RevenueAmountPhysical { get; set; }
        public DateTime ShippingDateConfirmed { get; set; }
        public DateTime ShippingDateRequested { get; set; }
        public int Storno_RU { get; set; }
        public int StornoPhysical_RU { get; set; }
        public decimal TaxAmountPhysical { get; set; }
        public int TimeExpected { get; set; }
        public string TransChildRefId { get; set; }
        public int TransChildType { get; set; }
        public int ValueOpen { get; set; }
        public int ValueOpenSecCur_RU { get; set; }
        public string Voucher { get; set; }
        public string VoucherPhysical { get; set; }
        public long NonFinancialTransferInventClosin { get; set; }
    }
   
}
