using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class HistoriesSalesStockDTO
    {
        [DataMember]
        public string item_no { get; set; }
        [DataMember]
        public string location_no { get; set; }
        [DataMember]
        public DateTime history_date { get; set; }
        [DataMember]
        public decimal sales_value { get; set; }
        [DataMember]
        public decimal stock_move { get; set; }
    }
}
