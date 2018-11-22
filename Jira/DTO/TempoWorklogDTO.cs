using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Jira.DTO
{
    public class TempoWorklogDTO
    {
        public string self { get; set; }
        public string tempoWorklogId { get; set; }
        public string jiraWorklogId { get; set; }
        public string issueKey
        {
            get
            {
                if (issue != null)
                {
                    return issue.key;
                }
                return null;
            }
        }
        public int timeSpentSeconds { get; set; }
        public int billableSeconds { get; set; }
        public DateTime startDate { get; set; }
        public TimeSpan startTime { get; set; }
        public string description { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string username
        {
            get
            {
                if (author != null)
                {
                    return author.username;
                }
                return null;
            }
        }
        public JiraIssue issue { get; set; } 
        public JiraUser author { get; set; }
        public class JiraIssue
        {
            public string self { get; set; }
            public string key { get; set; }
        } 
        
        public class JiraUser
        {
            public string self { get; set; }
            public string username { get; set; }
            public string displayname { get; set; }
        }      
    }
}
