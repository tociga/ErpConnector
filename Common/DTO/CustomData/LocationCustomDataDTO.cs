using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ErpConnector.Common.DTO.CustomData
{
    [DataContract]
    public class LocationCustomDataDTO
    {
        [DataMember]
        public string location_no { get; set; }
        [DataMember]
        public List<CustomAttributeValuesDTO> attributes { get; set; }

    }
}
