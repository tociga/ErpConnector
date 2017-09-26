using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class AGROrderLineDTO
    {
        public string ARGId { get; set; }
        public string Color { get; set; }
        public string Config { get; set; }
        public string ItemId { get; set; }
        public int LineNum { get; set; }
        public decimal Qty { get; set; }
        public string Size { get; set; }
        public string Style { get; set; }

    }
}
