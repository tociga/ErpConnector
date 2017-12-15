using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class AGREcoResAttributeTypeDTO
    {
        public long RecId { get; set; }
        public long Partition { get; set; }
        public int RecVersion { get; set; }
        public int DataType { get; set; }
        public int IsEnumeration { get; set; }
        public int IsHidden { get; set; }
        public string Name { get; set; }
    }
}
