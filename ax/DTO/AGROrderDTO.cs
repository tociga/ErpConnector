using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class AGROrderDTO
    {
        public string ARGId { get; set; }
        public string OrderFrom { get; set; }
        public string OrderTo { get; set; }
        public AGROrderType OrderType { get; set; }
        public DateTime ReceiveDate { get; set; }
        public AGROrderStatus OrderStatus { get; set; }
        public ArrayList ArgOrderLine { get; set; }
    }
}
