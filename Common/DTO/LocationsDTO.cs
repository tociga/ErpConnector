using ErpConnector.Common.Constants;
using System.Runtime.Serialization;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class LocationsDTO
    {
        [DataMember]
        public string location_no { get; set; }
        [DataMember]
        public string location_name { get; set; }
        [DataMember]
        public AGRConstants.AGR_LOCATION_TYPE location_type { get; set; }
    }
}
