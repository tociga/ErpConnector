using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.AGREntities
{
    public class ItemToCreate
    {
        public string product_no { get; set; }
        public string product_name { get; set; }
        public string description { get; set; }
        public string division_no { get; set; }
        public string division { get; set; }
        public string department_no { get; set; }
        public string department { get; set; }
        public string sup_department_no { get; set; }
        public string sup_department { get; set; }
        public string option_name_no { get; set; }
        public string option_name { get; set; }
        public string size_no { get; set; }
        public string size { get; set; }
        public string color_no { get; set; }
        public string color { get; set; }
        public string color_group_no { get; set; }
        public string color_group { get; set; }
        public string size_group { get; set; }
        public string size_group_no { get; set; }
        public int temp_id { get; set; }
        public int master_status { get; set; }
        public decimal? min_order_qty { get; set; }
        public decimal? pack_size { get; set; }
        public decimal? display_stock { get; set; }
        public int option_id { get; set; }
        public string primar_vendor_no { get; set; }
        public decimal? sale_price { get; set; }
        public decimal? cost_price { get; set; }
        public string life_cycle_state_id { get; set; }
    }
}
