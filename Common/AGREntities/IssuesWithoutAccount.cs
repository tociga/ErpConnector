using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.AGREntities
{
    public class IssuesWithoutAccount
    {
        public string issue_key { get; set; }
        public int organization_id { get; set; }
        public int account_id { get; set; }
    }
}
