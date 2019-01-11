using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Jira.DTO
{
    public class TempoAccountDTO
    {
        public string self { get; set; }
        public string key { get; set; }
        public long id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public bool global { get; set; }
        public TempoLeadDTO lead { get; set; }
        public TempoAccountCategoryDTO category { get; set; }
        public TempoCustomerDTO customer { get; set; }
        public string lead_name
        {
            get
            {
                if (lead != null)
                {
                    return lead.displayName;
                }
                return null;
            }
        }
        public string category_type
        {
            get
            {
                if (category != null)
                {
                    return category.name;
                }
                return null;
            }
        }

        public string customer_name
        {
            get
            {
                if (customer != null)
                {
                    return customer.name;
                }
                return null;
            }
        }

        public class TempoLeadDTO
        {
            public string self { get; set; }
            public string username { get; set; }
            public string displayName { get; set; }
        }

        public class TempoAccountCategoryDTO
        {
            public string self { get; set; }
            public string key { get; set; }
            public string name { get; set; }
        }
    
    }
}
