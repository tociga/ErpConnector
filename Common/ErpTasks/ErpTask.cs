using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.ErpTasks
{
    public class ErpTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ErpTaskStep> Steps { get; set; }
        public bool truncate_items { get; set; }
        public bool truncate_sales_trans_dump { get; set; }
        public bool truncate_locations_and_vendors { get; set; }
        public bool truncate_sales_trans_refresh { get; set; }
        public bool truncate_lookup_info { get; set; }
        public bool truncate_bom { get; set; }
        public bool truncate_po_to { get; set; }
        public bool truncate_price { get; set; }
        public bool truncate_attribute_refresh { get; set; }
        public int no_of_parallel_processes { get; set; }
    }
}
