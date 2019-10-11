using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class SAPResLineDTO
    {
        public string Plant { get; set; }
        public string StoreLoc { get; set; }
        public string Unit { get; set; }
        public string ShortText { get; set; }
        public decimal Quantity { get; set; }
        public string Material { get; set; }

    }
}
