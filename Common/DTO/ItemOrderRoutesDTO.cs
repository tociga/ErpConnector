using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class ItemOrderRoutesDTO
    {
        [DataMember]
        public string item_no { get; set; }
        [DataMember]
        public string location_no { get; set; }
        [DataMember]
        public string order_from_location_no { get; set; }
        [DataMember]
        public bool primary_route { get; set; }
        [DataMember]
        public decimal? min_order { get; set; }
        [DataMember]
        public decimal? cost_price { get; set; }
        [DataMember]
        public decimal order_multiple { get; set; }
        [DataMember]
        public int lead_time_days { get; set; }
        [DataMember]
        public int? qty_pallet { get; set; }
        [DataMember]
        public int? qty_layer { get; set; }
    }
}
