using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.AGREntities
{
    public class ErpActions
    {
        public int id { get; set; }
        public string action_type { get; set; }
        public int reference_id { get; set; }
        public int user_id { get; set; }
        public int status { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
        public int? date_reference_id { get; set; }
        public int? no_parallel_process { get; set; }
        public int? on_failure_retry_attempts { get; set; }

    }
}
