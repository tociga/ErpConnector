using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class TStocksDTO
    {
        public string matnr { get; set; }
        public string werks { get; set; }
        public string lgort { get; set; }
        public decimal labst { get; set; }
    }
}
