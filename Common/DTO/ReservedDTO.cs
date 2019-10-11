using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class ReservedDTO
    {
        [DataMember]
        public string item_no { get; set; }
        [DataMember]
        public string location_no { get; set; }
        [DataMember]
        public DateTime reserved_date { get; set; }
        [DataMember]
        public decimal reserved_qty { get; set; }
        [DataMember]
        public string description { get; set; }
    }
}
