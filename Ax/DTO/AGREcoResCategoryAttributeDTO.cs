using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    class AGREcoResCategoryAttributeDTO
    {
        public long RecId { get; set; }
        public long Partition { get; set; }
        public int RecVersion { get; set; }
        public long Attribute { get; set; }
        public int Modifier { get; set; }
        public long Category { get; set; }
    }
}
