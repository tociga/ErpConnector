using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO.CustomData
{
    [DataContract]
    public class SKUCustomDataDTO
    {
        public string item_no { get; set; }
        public string location_no { get; set; }
        [DataMember]
        public string attribute_name { get; set; }
        [DataMember]
        public string attribute_value { get; set; }
    }
}
