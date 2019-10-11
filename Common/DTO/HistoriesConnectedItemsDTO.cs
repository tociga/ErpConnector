using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class HistoriesConnectedItemsDTO
    {
        [DataMember]
        public string item_no_old { get; set; }
        [DataMember]
        public string item_no_new { get; set; }
        [DataMember]
        public string location_no { get; set; }
        [DataMember]
        public DateTime date_from { get; set; }
        [DataMember]
        public DateTime date_to { get; set; }
        [DataMember]
        public decimal scale { get; set; }
        [DataMember]
        public int connection_duration { get; set; }
        [DataMember]
        public bool connect_sale { get; set; }
        [DataMember]
        public bool connect_stock { get; set; }
        [DataMember]
        public int overlap_action { get; set; }
    }
}
