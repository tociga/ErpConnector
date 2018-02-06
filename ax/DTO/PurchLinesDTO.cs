using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class PurchLinesDTO
    {
        public long RecId { get; set; }
        public string DataAreaId { get; set; }
        public long Partition { get; set; }
        public string ItemId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string PurchId { get; set; }
        public decimal QtyOrdered { get; set; }
        public decimal RemainPurchPhysical { get; set; }
        public int PurchStatus { get; set; }
        public string InventDimId { get; set; }
        public string InventTransId { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
