using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class SAPSuppliersDTO
    {
        public List<SAPVendorDTO> tLfa1 { get; set; }
        public List<SAPLocationsDTO> tT001l { get; set; }
    }
}
