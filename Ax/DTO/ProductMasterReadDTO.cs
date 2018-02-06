using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;

namespace ErpConnector.Ax.DTO
{
    public class ProductMasterReadDTO
    {
        NoYes _areIdenticalConfigurationsAllowed;
        NoYes _isAutomaticVariantGenerationEnabled;
        NoYes _isCatchWeightProduct;
        NoYes _isProductKit;
        NoYes _isProductVariantUnitConversionEnabled;
        NoYes _kRFUseRatioCurves;
        EcoResVariantConfigurationTechnologyType _variantConfigurationTechnology;
        EcoResProductType _productType;

        public object AreIdenticalConfigurationsAllowed
        {
            get { return (int)_areIdenticalConfigurationsAllowed; }
            set
            {
                _areIdenticalConfigurationsAllowed = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public string HarmonizedSystemCode { get; set; }
        public object IsAutomaticVariantGenerationEnabled
        {
            get { return (int)_isAutomaticVariantGenerationEnabled; }
            set
            {
                _isAutomaticVariantGenerationEnabled = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public object IsCatchWeightProduct
        {
            get { return (int)_isCatchWeightProduct; }
            set
            {
                _isCatchWeightProduct = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public object IsProductKit
        {
            get { return (int)_isProductKit; }
            set
            {
                _isProductKit = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public object IsProductVariantUnitConversionEnabled
        {
            get { return (int)_isProductVariantUnitConversionEnabled; }
            set
            {
                _isProductVariantUnitConversionEnabled = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        // comes with Ax|is fashion
        #region AXis fashion properties
        public string KPMInstructionGroupId { get; set; }
        public string KRFColorRatioCurve { get; set; }
        public string KRFSizeRatioCurve { get; set; }
        public string KRFStyleRatioCurve { get; set; }
        public object KRFUseRatioCurves
        {
            get { return (int)_kRFUseRatioCurves; }
            set
            {
                _kRFUseRatioCurves = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        #endregion
        public string NMFCCode { get; set; }
        public string ProductColorGroupId { get; set; }
        public string ProductDescription { get; set; }
        public string ProductDimensionGroupName { get; set; }
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
        public string ProductSearchName { get; set; }
        public string ProductSizeGroupId { get; set; }
        public string ProductStyleGroupId { get; set; }
        public object VariantConfigurationTechnology
        {
            get { return (int)_variantConfigurationTechnology; }
            set
            {
                _variantConfigurationTechnology = DTOUtil.GetEnumFromObj<EcoResVariantConfigurationTechnologyType>(value, EcoResVariantConfigurationTechnologyType.PredefinedVariants);
            }
        }
        public string RetailProductCategoryName { get; set; }
        public object ProductType
        {
            get { return (int)_productType; }
            set
            {
                _productType = DTOUtil.GetEnumFromObj<EcoResProductType>(value, EcoResProductType.Item);
            }
        }
        public string STCCCode { get; set; }
        public string TrackingDimensionGroupName { get; set; }
        public string StorageDimensionGroupName { get; set; }

    }
}
