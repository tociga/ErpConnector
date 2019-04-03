using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Jira.DTO
{
    public class JiraUserDTO
    {
        public string key { get; set; }
        public string accountId { get; set; }
        public string accountType { get; set; }
        public string name { get; set; }
        public string emailAddress { get; set; }
    }
}
