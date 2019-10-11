using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    [DataContract]
    public class SAPVendorDTO 
    {
        [DataMember]
        public string lifnr { get; set; }
        [DataMember]
        public string name1 { get; set; }
        [DataMember]
        public int plifz { get; set; }

    }
}
