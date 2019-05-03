using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    public class GenericJiraLinks
    {
        public string self { get; set; }
        [JsonProperty("base")]
        public string base_url { get; set; }
        public string context { get; set; }
        public string next { get; set; }
    }
}
