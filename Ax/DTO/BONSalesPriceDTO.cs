using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class BONSalesPriceDTO
    {
        public string dataAreaId { get; set; }
        public string AccountCode { get; set; }
        public string AccountRelation { get; set; }
        public string Currency { get; set; }
        public DateTime FromDate { get; set; }
        public string ItemRelation { get; set; }
        public decimal PriceUnit { get; set; }
        public decimal QuantityAmountFrom { get; set; }
        public decimal QuantityAmountTo { get; set; }
        public DateTime ToDate { get; set; }
        public string UnitId { get; set; }
        public string ProductConfigurationId { get; set; }
        public string ProductColorId { get; set; }
        public string ProductSizeId { get; set; }
        public string ProductStyleId { get; set; }
        public string ItemCode { get; set; }
        public decimal Markup { get; set; }
        public string KRFSalesOrderCategory { get; set; }
        public NoYes DisregardLeadTime { get; set; }
        public int DeliveryTime { get; set; }
        public decimal Amount { get; set; }
        public NoYes KRFIsMarkdown { get; set; }
        public string KRFMarkdownReason { get; set; }
        public string KRFMarkdownModel { get; set; }
        public NoYes GenericCurrency { get; set; }
        public NoYes SearchAgain { get; set; }
    }
}
