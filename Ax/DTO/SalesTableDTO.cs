using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class SalesTableDTO
    {
        public long RecId { get; set; }
        public string DataAreaId { get; set; }
        public long Partition { get; set; }
        public int RecVersion { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string LineDisc { get; set; }
        public string SalesId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int DeliveryDateControlType { get; set; }
        public string DeliveryName { get; set; }
        public int DocumentStatus { get; set; }
        public string InventLocationId { get; set; }
        public string InventSiteId { get; set; }
        public string PurchId { get; set; }
        public DateTime ReceiptDateConfirmed { get; set; }
        public DateTime ReceiptDateRequested { get; set; }
        public int ReleaseStatus { get; set; }
        public int Reservation { get; set; }
        public long RetailChannelTable { get; set; }
        public int SalesStatus { get; set; }
        public int SalesType { get; set; }
        public long SourceDocumentHeader { get; set; }


    }
}
