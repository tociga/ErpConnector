using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.AGREntities
{
    public class ErpActionResult
    {
        public string ErrorMessage { get; set; }
        public int reference_id { get; set; }
        public int option_id { get; set; }
    }
}
