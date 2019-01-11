using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Jira.DTO
{
    public class ServiceDeskOrgUserDTO
    {
        [JsonProperty("accountId")]
        public string account_id { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        [JsonProperty("emailAddress")]
        public string email_address { get; set; }
        [JsonProperty("displayName")]
        public string display_name { get; set; }
        public bool active { get; set; }
        [JsonProperty("timeZone")]
        public string time_zone { get; set; }
        public string organization_id { get; set; }
    }
}
