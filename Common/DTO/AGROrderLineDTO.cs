using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class AGROrderLineDTO
    {
        [DataMember]
        public int agr_order_id { get; set; }
        [DataMember]
        public string color { get; set; }
        [DataMember]
        public string item_no { get; set; }
        [DataMember]
        public decimal qty { get; set; }
        [DataMember]
        public string size { get; set; }
        [DataMember]
        public string style { get; set; }
        //In some instances the customer wants to have different order to location no within the PO. If this value is blank or null use the
        //value from AGROrderHeaderDTO if it has a value use this one instead for the line. This property should be ignored if you are dealing with
        //a TO.
        [DataMember]
        public string order_to_location_no { get; set; }

    }
}
