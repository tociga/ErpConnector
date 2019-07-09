using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class SAPResHeaderDTO
    {
        public string Plant { get; set; }
        public string ResDate { get; set; }
        public string MoveType { get; set; }
        public string CreatedBy { get; set; }
        public string ProfitCtr { get; set; }
        public string MovePlant { get; set; }
        public string MoveStloc { get; set; }

    }
}
