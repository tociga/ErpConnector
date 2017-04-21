using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxConnect.Microsoft.Dynamics.DataEntities;

namespace AxConnect.DTO
{
    public class ReleasedDistinctProductsReadDTO
    {
        EcoResProductType _productType;
        ABC _marginAbc;
        ABC _valueAbc;
        ABC _revenueAbc;
        public string DataAreaId { get; set; }
        public string ItemNumber { get; set; }
        public object ProductType
        {
            get
            {
                return (int)_productType;
            }
            set
            {
                _productType = DTOUtil.GetEnumFromObj<EcoResProductType>(value, EcoResProductType.Item);
            }
            
        }
        public decimal GrossProductHeight { get; set; }
        public decimal GrossProductWidth { get; set; }
        public string PrimaryVendorAccountNumber { get; set; }
        public decimal NetProductWeight { get; set; }
        public decimal GrossDepth { get; set; }
        public decimal ProductVolume { get; set; }
        public object RevenueABCCode
        {
            get { return (int)_revenueAbc; }
            set
            {
                _revenueAbc = DTOUtil.GetEnumFromObj<ABC>(value, ABC.None);
            }
        }
        public object ValueABCCode
        {
            get { return _valueAbc; }
            set
            {
                _valueAbc = DTOUtil.GetEnumFromObj<ABC>(value, ABC.None);
            }
        }
        public object MarginABCCode
        {
            get { return _marginAbc; }
            set
            {
                _marginAbc = DTOUtil.GetEnumFromObj<ABC>(value, ABC.None);
            }
        }
        public string SearchName { get; set; }
        public string ProductGroupId { get; set; }
        public string ProjectCategoryId { get; set; }
        public decimal StandardPalletQty { get; set; }
        public decimal QtyPerLayer { get; set; }
        public string BuyerGroupId { get; set; }
        public long ProductRecId { get; set; }
        public decimal FixedSalesPriceCharges { get; set; }
    }
}
