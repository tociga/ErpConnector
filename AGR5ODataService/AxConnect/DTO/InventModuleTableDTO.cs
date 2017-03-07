using AxConnect.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.DTO
{
    public class InventModuleTableDTO
    {
        private NoYes _allocateMarkup;
        private NoYes _taxwithold_th;
        private NoYes _endDisc;
        private ModuleInventPurchSales _moduleType;
        private NoYes _interCompanyBlocked;
        public object AllocateMarkup
        {
            get
            {
                return (int)_allocateMarkup;
            }
            set
            {
                if (value is NoYes)
                {
                    _allocateMarkup = (NoYes)value;
                }
                else
                {
                    _allocateMarkup = NoYes.No;
                }
            }
        }
        public string MultiLineDisc { get; set; }
        public object TaxWithholdCalculate_TH
        {
            get
            {
                return (int)_taxwithold_th;
            }
            set
            {
                if (value is NoYes)
                {
                    _taxwithold_th = (NoYes)value;
                }
                else
                {
                    _taxwithold_th = NoYes.No;
                }
            }
        }
        public string TaxGSTReliefCategory_MY_ReliefCategoryId { get; set; }
        public int PDSPricingPrecision { get; set; }
        public DateTime PriceDate { get; set; }
        public string UnitId { get; set; }
        public string MarkupGroupId { get; set; }
        public string SuppItemGroupId { get; set; }
        public decimal PriceUnit { get; set; }
        public string LineDisc { get; set; }
        public decimal Markup { get; set; }
        public decimal MaximumRetailPrice_IN { get; set; }
        public decimal PriceSecCur_RU { get; set; }
        public decimal MarkupSecCur_RU { get; set; }
        public string TaxItemGroupId { get; set; }
        public object EndDisc
        {
            get
            {
                return (NoYes)_endDisc;
            }
            set
            {
                if (value is NoYes)
                {
                    _endDisc = (NoYes)value;
                }
                else
                {
                    _endDisc = NoYes.No;
                }
            }
        }
        public decimal PriceQty { get; set; }
        public decimal Price { get; set; }
        public decimal UnderDeliveryPct { get; set; }
        public string TaxWithholdItemGroupHeading_TH_TaxWithholdItemGroup { get; set; }
        public string ItemId { get; set; }
        public decimal OverDeliveryPct { get; set; }
        public string dataAreaId { get; set; }
        public object ModuleType
        {
            get
            {
                return (ModuleInventPurchSales)_moduleType;
            }
            set
            {
                if (value is ModuleInventPurchSales)
                {
                    _moduleType = (ModuleInventPurchSales)value;
                }
                else
                {
                    _moduleType = ModuleInventPurchSales.Invent;
                }
            }
        }
        public object InterCompanyBlocked
        {
            get
            {
                return (NoYes)_interCompanyBlocked;
            }
            set
            {
                if (value is NoYes)
                {
                    _interCompanyBlocked = (NoYes)value;
                }
                else
                {
                    _interCompanyBlocked = NoYes.No;
                }
            }
        }
    }
}
