using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class TReservationDTO
    {
        public string matnr { get; set; }
        public string werks { get; set; }
        public string lgort { get; set; }
        public string bdter { get; set; }
        public decimal bdmng { get; set; }
        public string maktx { get; set; }
    }
}
