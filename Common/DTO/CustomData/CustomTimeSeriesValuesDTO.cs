using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO.CustomData
{
    [DataContract]
    public class CustomTimeSeriesValuesDTO
    {
        [DataMember]
        public DateTime date { get; set; }
        [DataMember]
        public decimal value { get; set; }
    }
}
