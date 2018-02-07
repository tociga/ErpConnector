using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.Exceptions
{
    public class AxWebExceptions : ErpConnectorException
    {
        public string code { get; set; }
        public string message { get; set; }
        public AxInnerException innererror { get; set; }
    }
}
