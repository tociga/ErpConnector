using ErpConnector.Common.DTO.CustomData;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class ItemsV2DTO
    {
        public ItemsV2DTO()
        {            
            product_attributes = new List<ProductCustomDataDTO>();
            item_detials = new List<ItemDetailsV2DTO>();
        }
        public string location_no { get; set; }
        [DataMember]
        public string item_no { get; set; }
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
        public decimal? volume { get; set; }
        [DataMember]
        public decimal? sale_price { get; set; }
        [DataMember]
        public int? wastage_days { get; set; }
        [DataMember]
        public string abc_grouping { get; set; }
        [DataMember]
        public string responsible { get; set; }
        [DataMember]
        public List<ProductCustomDataDTO> product_attributes { get; set; }
        [DataMember]
        public List<ItemDetailsV2DTO> item_detials { get; set; }

        public ItemsV2DTO ShallowCopy()
        {
            return (ItemsV2DTO)MemberwiseClone();
        }

    }
}
