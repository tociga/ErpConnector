using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class UndeliveredDTO
    {
        [DataMember]
        public string item_no { get; set; }
        [DataMember]
        public string location_no { get; set; }
        [DataMember]
        public DateTime est_deliv_date { get; set; }
        [DataMember]
        public string reference_no { get; set; }
        [DataMember]
        public decimal units { get; set; }
        [DataMember]
        public DateTime expire_date { get; set; }
    }
}
