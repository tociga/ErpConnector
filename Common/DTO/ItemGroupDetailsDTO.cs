using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class ItemGroupDetailsDTO
    {
        [DataMember]
        public string group_no { get; set; }
        [DataMember]
        public string group_name { get; set; }
        [DataMember]
        public long level_id { get; set; }
        [DataMember]
        public string parent_no { get; set; }
    }
}
