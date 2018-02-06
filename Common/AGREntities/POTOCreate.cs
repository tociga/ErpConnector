using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.AGREntities
{
    public class POTOCreate
    {
        public int order_id { get; set; }
        public string item_no { get; set; }
        public string location_no { get; set; }
        public string order_from_location_no { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public string style { get; set; }
        public int user_id { get; set; }
        public decimal unit_qty_chg { get; set; }
        public DateTime est_delivery_date { get; set; }
        public string site_id { get; set; }
        public string channel_id { get; set; }
        public string warehouse { get; set; }
        public string vendor_location_type { get; set; }
    }
}
