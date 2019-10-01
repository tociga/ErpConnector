using System;
using System.Runtime.Serialization;
using ErpConnector.Common.Constants;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class AGROrderHeaderDTO
    {
        // internal order id in the agr system
        [DataMember]
        public int agr_order_id { get; set; }
        [DataMember]
        public string order_from_location_no { get; set; }
        //The location receiving the goods
        [DataMember]
        public string order_to_location_no { get; set; }
        //The estimated delivery date
        [DataMember]
        public DateTime est_deliv_date { get; set; }
        [DataMember]
        public AGRConstants.AGR_ORDER_TYPE order_type { get; set; }
    }
}
