using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO.CustomData
{
    [DataContract]
    public class VendorCustomDataDTO
    {
        [DataMember]
        public string vendor_no { get; set; }
        [DataMember]
        public List<CustomAttributeValuesDTO> attributes { get; set; }
    }
}
