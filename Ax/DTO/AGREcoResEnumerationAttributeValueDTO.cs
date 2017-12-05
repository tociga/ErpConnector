using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class AGREcoResEnumerationAttributeValueDTO
    {
        public long RecId { get; set; }
        public long Partition { get; set; }
        public int RecVersion { get; set; }
        public long AttributeType { get; set; }
        public int OrdinalNumber { get; set; }
        public long Value { get; set; }
    }
}
