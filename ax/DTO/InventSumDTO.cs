using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class InventSumDTO
    {
        public long RecId { get; set; }
        public string DataAreaId { get; set; }
        public long Partition { get; set; }
        public string ItemId { get; set; }
        public decimal PostedQty { get; set; }
        public decimal PostedValue { get; set; }
        public decimal Deducted { get; set; }
        public decimal Received { get; set; }
        public decimal ReservPhysical { get; set; }
        public decimal ReservOrdered { get; set; }
        public decimal OnOrder { get; set; }
        public decimal Ordered { get; set; }
        public string InventDimId { get; set; }
        public int Closed { get; set; }
        public decimal AvailPhysical { get; set; }
        public decimal PhysicalValue { get; set; }
        public decimal PhysicalInvent { get; set; }
        public int ClosedQty { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
