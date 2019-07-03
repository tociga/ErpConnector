using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class SAPProductsDTO
    {
        public string matnr { get; set; }
        public string lifnr { get; set; }
        public string idnlf { get; set; }
        public string maktx { get; set; }
        public string prodh { get; set; }
        public string mmsta { get; set; }
        public decimal volum { get; set; }
        public decimal verpr { get; set; }
        public decimal bstmi { get; set; }
        public decimal plifz { get; set; }
        public decimal eislo { get; set; }
        public string maabc { get; set; }
        public string lfrhy { get; set; }
        public decimal price { get; set; }
        public string dispo { get; set; }
        public decimal eisbe { get; set; }
        public decimal mabst { get; set; }
        public string matkl { get; set; }
        public string wgbez { get; set; }
        public string vrbmt { get; set; }
        public string meins { get; set; }
        public string bdatu { get; set; }
        public string flifn { get; set; }
    }
}
