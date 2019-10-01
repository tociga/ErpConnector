using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class VendorsDTO
    {
        [DataMember]
        public string vendor_no { get; set; }
        [DataMember]
        public string vendor_name { get; set; }
        [DataMember]
        public string vendor_group { get; set; }
    }
}
