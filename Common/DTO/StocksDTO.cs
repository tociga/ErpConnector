using System;
using System.Runtime.Serialization;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class StocksDTO
    {
        [DataMember]
        public string item_no { get; set; }
        [DataMember]
        public string location_no { get; set; }
        [DataMember]
        public decimal stock_units { get; set; }
        [DataMember]
        public bool closed { get; set; }
        [DataMember]
        public DateTime expire_date { get; set; }
    }
}
