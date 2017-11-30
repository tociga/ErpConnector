using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class RetailTransactionTableDTO
    {
        public long RecId { get; set; }
        public string DataAreaId { get; set; }
        public long Partition { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public int RecVersion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ModifiedBy { get; set; }
        public string Terminal { get; set; }
        public decimal AmountToAccount { get; set; }
        public long BatchId { get; set; }
        public string BatchTerminalId { get; set; }
        public DateTime BuisnessDate { get; set; }
        public long Channel { get; set; }
        public string ChannelReferenceId { get; set; }
        public string Comment { get; set; }
        public long CostAmount { get; set; }
        public int Counter { get; set; }
        public int CreatedOffline { get; set; }
        public string CreatedOnPosTerminal { get; set; }
        public string Currency { get; set; }
        public string CustAccount { get; set; }
        public decimal CustDiscAmount { get; set; }
        public string CustPurchaseOrder { get; set; }
        public long DefaultDimension { get; set; }
        public string Description { get; set; }
        public long DiscAmount { get; set; }
        public string DlvMode { get; set; }
        public int EntryStatus { get; set; }
        public decimal ExchRate { get; set; }
        public string FiscalDocumentId { get; set; }
        public string FiscalSerialId { get; set; }
        public decimal GrossAmount { get; set; }
        public int IncludedInStatistics { get; set; }
        public decimal IncomeExpenseAmount { get; set; }
        public string InfocodeDiscGroup { get; set; }
        public string InventLocationId { get; set; }
        public string InventSiteId { get; set; }
        public string InvoiceComment { get; set; }
        public string InvoiceId { get; set; }
        public int ItemsPosted { get; set; }
        public long LogisticsPostalAddress { get; set; }
        public string LoyaltyCardId { get; set; }
        public decimal LoyaltyDiscAmount_RU { get; set; }
        public decimal NetAmount { get; set; }
        public int NumberOfInvoices { get; set; }
        public decimal NumberOfItemLines { get; set; }
        public decimal NumberOfItems { get; set; }
        public int NumberOfPaymentLines { get; set; }
        public int OpenDrawer { get; set; }
        public string Origin { get; set; }
        public decimal PaymentAmoun { get; set; }
        public int PostAsShipment { get; set; }
        public DateTime ReceiptDateRequested { get; set; }
        public string ReceiptEmail { get; set; }
        public int ReceiptEmailSent { get; set; }
        public string ReceiptId { get; set; }
        public string RefundReceipt { get; set; }
        public int Replicated { get; set; }
        public int ReplicationCounterFromOrigin { get; set; }
        public string RetrievedFromReceiptId { get; set; }
        public decimal RoundedAmount { get; set; }
        public int SalesReturnSale { get; set; }
        public decimal SalesInvoiceAmount { get; set; }
        public decimal SalesOrderAmount { get; set; }
        public string SalesOrderId { get; set; }
        public decimal SalesPaymentDifference { get; set; }
        public string SellToContactId { get; set; }
        public string Shift { get; set; }
        public DateTime ShiftDate { get; set; }
        public DateTime ShippingDateRequested { get; set; }
        public int SkipAggregation { get; set; }
        public string Staff { get; set; }
        public string StatementCode { get; set; }
        public string StatementId { get; set; }
        public string Store { get; set; }
        public int TimeWhenTotalPressed { get; set; }
        public int TimeWhenTransClosed { get; set; }
        public int ToAccount { get; set; }
        public decimal TotalDiscAmount { get; set; }
        public decimal TotalManualDiscountAmount { get; set; }
        public decimal TotalManualDiscountPercentage { get; set; }
        public string TransactionId { get; set; }
        public int TransCode { get; set; }
        public DateTime TransDate { get; set; }
        public int TransTableId { get; set; }
        public int TransTime { get; set; }
        public int Type { get; set; }
        public int WrongShift { get; set; }
        public string CustAccountAsync { get; set; }
        public string SourceId { get; set; }
        public int PriceOverride { get; set; }
        public int FTCExempt { get; set; }
        public int CatalogUpSellShown { get; set; }
        public int ContinuityOrder { get; set; }
        public int ContinuityChild { get; set; }
        public int ContinuityLineEval { get; set; }
        public int InstallmentOrderSubmitted { get; set; }
        public int OutOfBalanceReleaseType { get; set; }
        public int PaymOutOfBalanceType { get; set; }
        public int InstallmentBillingPrompt { get; set; }
        public int AllocationPriority { get; set; }
        public string SalesGroup { get; set; }
    }
}
