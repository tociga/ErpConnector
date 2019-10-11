using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class SAPProductsDTO
    {
        public List<TProductsDTO> tProducts { get; set; }
        public List<TProductGroupsDTO> tVt179 { get; set; }
        public List<TBapiretDTO> tBapiret { get; set; }
    }
}
