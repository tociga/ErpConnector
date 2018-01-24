using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class OpenSalesPriceJournalLinesDTO
    {
            public string dataAreaId { get; set; }
            public string TradeAgreementJournalNumber { get; set; }
            public long LineNumber { get; set; }
            public string ProductConfigurationId { get; set; }
            public DateTime PriceApplicableFromDate { get; set; }
            public NoYes WillSearchContinue { get; set; }
            public string QuantityUnitSymbol { get; set; }
            public decimal SalesPriceQuantity { get; set; }
            public string AttributeBasedPricingId { get; set; }
            public string ProductSizeId { get; set; }
            public string ItemNumber { get; set; }
            public string PriceCurrencyCode { get; set; }
            public decimal ToQuantity { get; set; }
            public decimal FixedPriceCharges { get; set; }
            public NoYes WillDeliveryDateControlDisregardLeadTime { get; set; }
            public DateTime PriceApplicableToDate { get; set; }
            public string PriceWarehouseId { get; set; }
            public int SalesLeadTimeDays { get; set; }
            public decimal FromQuantity { get; set; }
            public string CustomerAccountNumber { get; set; }
            public string PriceCustomerGroupCode { get; set; }
            public decimal Price { get; set; }
            public string PriceSiteId { get; set; }
            public NoYes IsGenericCurrencySearchEnabled { get; set; }
            public string ProductColorId { get; set; }
            public string ProcessingLog { get; set; }
            public string ProductStyleId { get; set; }
            public string KRFMarkdownModel_Name { get; set; }
            public string KRFSalesOrderCategory { get; set; }
            public NoYes KRFIsMarkdown { get; set; }
            public string KRFPurchOrderCategory { get; set; }
            public string KRFMarkdownReason { get; set; }
            public long KRFMarkdownModel { get; set; }
    }
}
