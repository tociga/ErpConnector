using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ErpConnector.Jira.DTO
{
    public class JiraIssueFieldsDTO
    {
        public long? timespent { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public string description { get; set; }
        public string summary { get; set; }
        public JiraProjectDTO project { get; set; }
        [JsonProperty("customfield_11330")]
        public JiraCustomFieldDTO account_custom_field { get; set; }
        public List<JiraComponentDTO> components { get; set; }


    }
}