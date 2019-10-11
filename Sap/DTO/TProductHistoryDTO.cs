using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class TProductHistoryDTO
    {
        public string mblnr { get; set; }
        public string mjahr { get; set; }
        public string matnr { get; set; }
        public string werks { get; set; }
        public string lgort { get; set; }
        public string wempf { get; set; }
        public string budat { get; set; }
        public decimal menge { get; set; }
        public string shkzg { get; set; }
        public string bwart { get; set; }
        public decimal labst { get; set; }

    }
}
