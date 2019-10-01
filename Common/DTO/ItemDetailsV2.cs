using ErpConnector.Common.DTO.CustomData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class ItemDetailsV2DTO
    {
        public ItemDetailsV2DTO()
        {
            sku_attribute_values = new List<SKUCustomDataDTO>();
            item_order_routes = new List<ItemOrderRoutesV2DTO>();
        }
        public string item_no { get; set; }
        [DataMember]
        public string location_no { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public decimal? minimum_stock { get; set; }
        [DataMember]
        public decimal? maximum_stock { get; set; }
        [DataMember]
        public int? order_frequency_days { get; set; }
        [DataMember]
        public List<SKUCustomDataDTO> sku_attribute_values { get; set; }
        [DataMember]
        public List<ItemOrderRoutesV2DTO> item_order_routes { get; set;  }


    }
}
