using System.Runtime.Serialization;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class ItemOrderRoutesV2DTO
    {
        public ItemOrderRoutesV2DTO()
        {
        }
        public string item_no { get; set; }
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
