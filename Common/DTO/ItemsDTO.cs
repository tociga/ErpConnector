using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class ItemsDTO
    {
        [DataMember]
        public string item_no { get; set; }
        [DataMember]
        public string location_no { get; set; }
        [DataMember]
        public string primary_vendor_no { get; set; }
        [DataMember]
        public string original_item_no { get; set; }
        [DataMember]
        public string item_name { get; set; }
        [DataMember]
        public string article_no { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string size { get; set; }
        [DataMember]
        public string color { get; set; }
        [DataMember]
        public string style { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public bool closed { get; set; }
        [DataMember]
        public decimal? weight { get; set; }
        [DataMember]
        public decimal? volume{ get; set; }
        [DataMember]
        public decimal? sale_price { get; set; }
        [DataMember]
        public int? wastage_days { get; set; }
        [DataMember]
        public string abc_grouping { get; set; }
        [DataMember]
        public string responsible { get; set; }
    }
}
