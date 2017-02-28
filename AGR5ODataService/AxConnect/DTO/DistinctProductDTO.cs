using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxConnect.Microsoft.Dynamics.DataEntities;

namespace AxConnect.DTO
{
    public class DistinctProductDTO
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
                //var prodType = value is EcoResProductType;
                if (value is EcoResProductType)
                {
                    _productType = (EcoResProductType)value;
                }
                else
                {
                    _productType = EcoResProductType.Item;
                }
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
                if (value is ABC)
                {
                    _revenueAbc = (ABC)value;
                }
                else
                {
                    _revenueAbc = ABC.None;
                }
            }
        }
        public object ValueABCCode
        {
            get { return _valueAbc; }
            set
            {
                if (value is ABC)
                {
                    _valueAbc = (ABC)value;
                }
                else
                {
                    _valueAbc = ABC.None;
                }
            }
        }
        public object MarginABCCode
        {
            get { return _marginAbc; }
            set
            {
                if (value is ABC)
                {
                    _marginAbc = (ABC)value;
                }
                else
                {
                    _marginAbc = ABC.None;
                }
            }
        }
        public string SearchName { get; set; }
        public string ProductGroupId { get; set; }
        public string ProjectCategoryId { get; set; }
        public decimal StandardPalletQty { get; set; }
        public decimal QtyPerLayer { get; set; }
        public string BuyerGroupId { get; set; }
        public long Product { get; set; }
        public decimal FixedSalesPriceCharges { get; set; }
    }
}
