using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.DTO
{
    public class InventTransOriginDTO
    {
        public long RecId { get; set; }
        public string DataAreaId { get; set; }
        public long Partition { get; set; }
        public string InventTransId { get; set; }
        public int ReferenceCategory { get; set; }
        public string ReferenceId { get; set; }
        public string ItemId { get; set; }
        public string ItemInventDimId { get; set; }
        public long Party { get; set; }
        public int RecVersion { get; set; }

    }
}
