using AxConnect.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.DTO
{
    public class DistinctProductsDTO
    {
        EcoResProductType _productType;
        NoYes _isCatchWeightProduct;
        public string NMFCCode { get; set; }
        public object ProductType
        {
            get { return (int)_productType; }
            set
            {
                _productType = DTOUtil.GetEnumFromObj<EcoResProductType>(value, EcoResProductType.Item);
            }
        }
        public string STCCCode { get; set; }
        public string StorageDimensionGroupName { get; set; }
        public string ProductNumber { get; set; }
        public object IsCatchWeightProduct
        {
            get { return (int)_isCatchWeightProduct; }
            set
            {
                _isCatchWeightProduct = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public string ProductDescription { get; set; }
        public string RetailProductCategoryName { get; set; }
        public string TrackingDimensionGroupName { get; set; }
        public string ProductSearchName { get; set; }
        public string ProductName { get; set; }
        public string HarmonizedSystemCode { get; set; }
    }
}
