using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class ItemDetailsDTO
    {
        [DataMember]
        public string item_no { get; set; }
        [DataMember]
        public string location_no { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public decimal? minimum_stock { get; set; }
        [DataMember]
        public decimal? maximum_stock { get; set; }
        [DataMember]
        public int? order_frequency_days { get; set; }
        [DataMember]
        public decimal? confidence_factor { get; set; }
    }
}
