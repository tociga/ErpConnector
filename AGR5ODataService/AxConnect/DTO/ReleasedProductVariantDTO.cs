using AxConnect.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.DTO
{
    public class ReleasedProductVariantDTO
    {
        public string ProductSizeId { get; set; }
        public string ProductConfigurationId { get; set; }
        public string ProductSearchName { get; set; }
        public string ItemNumber { get; set; }
        //"ProductVariantNumber": "0140 :  : Large : Black :",
        public string ProductStyleId { get; set; }
        //"dataAreaId": "usrt",
        public string ProductDescription { get; set; }
        public string ProductMasterNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductColorId { get; set; }
        //"PublicRecId": 22565421845
        //public List<ProductTranslation> ProductTranslation {get;set;}
    }
}
