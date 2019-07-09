using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class SAPReservationWriteDTO
    {
        public SAPResHeaderDTO Header { get; set; }
        public List<SAPResLineDTO> Lines { get; set; }
    }
}
