using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class AGREcoResValueDTO
    {
        public long RecId { get; set; }
        public long Partition { get; set; }
        public int RecVersion { get; set; }
        public string TextValue { get; set; }
        public int IntValue { get; set; }
        public decimal FloatValue { get; set; }
        public decimal CurrencyValue { get; set; }
        public int BooleanValue { get; set; }
        public DateTime DateTimeValue { get; set; }
    }
}
