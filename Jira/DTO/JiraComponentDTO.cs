using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Jira.DTO
{
    public class JiraComponentDTO
    {
        public string self { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string assigneeType { get; set; }
        public string realAssigneeType { get; set; }
        public bool isAssigneeTypeValid { get; set; }
        public string project { get; set; }
        public int projectId { get; set; }
    }
}
