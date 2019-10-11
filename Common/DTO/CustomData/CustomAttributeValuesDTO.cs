using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO.CustomData
{
    [DataContract]
    public class CustomAttributeValuesDTO
    {
        [DataMember]
        public string attribute_name { get; set; }
        [DataMember]
        public string attribute_value { get; set; }
    }
}
