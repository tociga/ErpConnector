using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class TSuppliersProductsDTO
    {
        public string matnr { get; set; }
        public string lgort { get; set; }
        public decimal minbm { get; set; }
        public decimal aplfz { get; set; }
        public decimal umrez { get; set; }
        public decimal norbm { get; set; }
        public string lifnr { get; set; }
    }
}
