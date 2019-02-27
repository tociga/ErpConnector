using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Jira.DTO
{
    public class JiraIssueDTO
    {
        public string expand { get; set; }
        public string id { get; set; }
        public string self { get; set; }
        public string key { get; set; }
        public JiraIssueFieldsDTO fields { get; set; }
        public long? timespent
        {
            get
            {
                if (fields != null)
                {
                    return fields.timespent;
                }
                return null;
            }
        }
        public DateTime? created
        {
            get
            {
                if (fields != null)
                {
                    return fields.created;
                }
                return null;
            }
        }

        public DateTime? updated
        {
            get
            {
                if (fields != null)
                {
                    return fields.updated;
                }
                return null;
            }
        }

        public string description
        {
            get
            {
                if (fields != null)
                {
                    return fields.description;
                }
                return null;
            }
        }

        public string summary
        {
            get
            {
                if (fields != null)
                {
                    return fields.summary;
                }
                return null;
            }
        }

        public string projectKey
        {
            get
            {
                if (fields != null && fields.project != null)
                {
                    return fields.project.key;
                }
                return null;
            }
        }

        public int? account_id
        {
            get
            {
                if (fields !=null && fields.account_custom_field != null)
                {
                    return fields.account_custom_field.id;
                }
                return null;
            }
        }

        public string component_id
        {
            get
            {
                if (fields != null && fields.components != null && fields.components.Any())
                {
                    return fields.components[0].id;
                }
                return null;
            }
        }

        public int? organization_id
        {
            get
            {
                if (fields != null && fields.organization_custom_field != null && fields.organization_custom_field.Any())
                {
                    return fields.organization_custom_field[0].id;
                }
                return null;
            }
        }

    }
}
