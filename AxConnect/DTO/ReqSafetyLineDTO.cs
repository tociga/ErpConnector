using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.DTO
{
    public class ReqSafetyLineDTO
    {
        public decimal SafetyFactor { get; set; }
        public string SafetyKeyId { get; set; }
        public int FreqCode { get; set; }
        public int Freq { get; set; }
        public DateTime Sort1980 { get; set; }
        public string DataAreaId { get; set; }
        public int RecVersion { get; set; }
        public long Partition { get; set; }
        public long RecId { get; set; }

    }
}
