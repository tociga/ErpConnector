using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.Exceptions
{
    public class AxInnerException
    {
        public string message { get; set; }
        public string type { get; set; }
        public string stacktrace { get; set; }
    }
}
