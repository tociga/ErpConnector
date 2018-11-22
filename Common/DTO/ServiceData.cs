using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    public class ServiceData
    {
        public string AuthHeader { get; set; }
        public string AuthToken { get; set; }
        public string BaseUrl { get; set; }
        public string OdataUrlPostFix { get; set; }
    }
}
