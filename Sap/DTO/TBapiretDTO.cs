using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class TBapiretDTO
    {
        public string type { get; set; }      
        public string id { get; set; }
        public string number { get; set; }
        public string message { get; set; }
        public string logNo { get; set; }
        public string logMsgNo { get; set; }
        public string messageV1 { get; set; }
        public string messageV2 { get; set; }
        public string messageV3 { get; set; }
        public string messageV4 { get; set; }
        public string parameter { get; set; }
        public int row { get; set; }
        public string field { get; set; }
        public string system { get; set; }
    }
}
