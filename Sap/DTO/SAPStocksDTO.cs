using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class SAPStocksDTO
    {
        public List<TStocksDTO> tStocks { get; set; }
        public List<TBapiretDTO> tBapiret { get; set; }
    }
}
