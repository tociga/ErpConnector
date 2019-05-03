using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Jira.DTO
{
    public class JiraProjectDTO
    {
        public string expand { get; set; }
        public string self { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string projectTypeKey { get; set; }
        public bool simplified { get; set; }
        public string style { get; set; }
        public bool isPrivate { get; set; }
    }
}
