using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    public class ErpActionStep
    {
        public string StepName { get; set; }
        public string DBTable { get; set; }
        public string D365Endpoint { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
