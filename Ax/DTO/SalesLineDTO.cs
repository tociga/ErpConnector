using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class SalesLineDTO
    {
        public long RecId { get; set; }
        public string DataAreaId { get; set; }
        public long Partition { get; set; }
        public int RecVersion { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public decimal CostPrice { get; set; }
        public string CurrencyCode { get; set; }
        public int CustomerLineNum { get; set; }
        public decimal ExpectedRetQty { get; set; }
        public string InventDimId { get; set; }
        public string InventTransId { get; set; }
        public string ItemBOMId { get; set; }
        public string ItemId { get; set; }
        public decimal LineAmount { get; set; }
        public string LineHeader { get; set; }
        public decimal LineNum { get; set; }
        public string Name { get; set; }
        public decimal PriceUnit { get; set; }
        public decimal QtyOrdered { get; set; }
        public DateTime ReceiptDateConfirmed { get; set; }
        public DateTime ReceiptDateRequested { get; set; }
        public string RetailVariantId { get; set; }
        public string SalesId { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal SalesQty { get; set; }
        public int SalesStatus { get; set; }
        public int SalesType { get; set; }
        public string SalesUnit { get; set; }
        public DateTime ShippingDateConfirmed { get; set; }
        public DateTime ShippingDateRequested { get; set; }


    }
}
