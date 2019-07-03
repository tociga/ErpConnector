using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class TUndeliveredDTO
    {
        public string ebeln { get; set; }
        public string ebelp { get; set; }
        public string webre { get; set; }
        public string matnr { get; set; }
        public string lifnr { get; set; }
        public string lgort { get; set; }
        public string eindt { get; set; }
        public decimal menge { get; set; }
        public decimal mglief { get; set; }
    }
}
