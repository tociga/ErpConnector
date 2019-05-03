using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    public class JiraObjectMetaData
    {
        public int count { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
    }
}
