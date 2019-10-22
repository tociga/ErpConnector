using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.AGREntities
{
    public class AGRProductLifeCycleState
    {
        public int product_lifecycle_state_update_id { get; set; }
        public int product_id { get; set; }
        public string product_no { get; set; }
        public string product_size_id { get; set; }
        public string product_color_id { get; set; }
        public string product_style_id { get; set; }
        public string product_config_id { get; set; }
        public string lifecycle_status { get; set; }
    }
}
