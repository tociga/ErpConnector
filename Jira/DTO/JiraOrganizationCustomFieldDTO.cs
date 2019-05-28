using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Jira.DTO
{
    public class JiraOrganizationCustomFieldDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        [JsonProperty("_links")]
        public Links links { get; set; }

        public class Links
        {
            public string self { get; set; }
        }
    }
}
