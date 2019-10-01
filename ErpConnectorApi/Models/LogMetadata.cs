using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnectorApi.Models
{
    public class LogMetadata
    {
        public string RequestUri { get; set; }
        public string RequestMethod { get; set; }
        public string RequestBody { get; set; }
        public string ReferrerFullUri { get; set; }
    }
}
