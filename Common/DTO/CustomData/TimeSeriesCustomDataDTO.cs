using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO.CustomData
{
    [DataContract]
    public class TimeSeriesCustomDataDTO
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int series_type { get; set; }
        [DataMember]
        public List<CustomTimeSeriesValuesDTO> values { get; set; }
    }
}
