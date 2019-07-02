using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    [DataContract]
    public class SAPLocationsDTO
    {
        [DataMember]
        public string mandt { get; set; }
        [DataMember]
        public string werks { get; set; }
        [DataMember]
        public string lgort { get; set; }
        [DataMember]
        public string lgobe { get; set; }
    }
}
