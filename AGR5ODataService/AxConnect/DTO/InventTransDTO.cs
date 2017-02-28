using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.DTO
{
    public class InventTransDTO
    {
        public long RecId { get; set; }
        public string DataAreaId { get; set; }
        public long Partition { get; set; }
        public string ItemId { get; set; }
        public int StatusIssue { get; set; }
        public DateTime DatePhysical { get; set; }
        public decimal Qty { get; set; }
        public DateTime DateFinancial { get; set; }
        public string InventDimId { get; set; }
        public string InvoiceId { get; set; }
        public long InventTransOrigin { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
   
}
