using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;

namespace ErpConnector.Ax.DTO
{
    public class ProductMasterWriteDTO
    {
 

        public NoYes AreIdenticalConfigurationsAllowed{ get; set; }
        public string HarmonizedSystemCode { get; set; }
        public NoYes IsAutomaticVariantGenerationEnabled{ get; set; }
        public NoYes IsCatchWeightProduct{ get; set; }
        public NoYes IsProductKit{ get; set; }
        public NoYes IsProductVariantUnitConversionEnabled{ get; set; }
        // comes with Ax|is fashion
        #region AXis fashion properties
        //public string KPMInstructionGroupId { get; set; }
        //public string KRFColorRatioCurve { get; set; }
        //public string KRFSizeRatioCurve { get; set; }
        //public string KRFStyleRatioCurve { get; set; }
        //public NoYes KRFUseRatioCurves { get; set; }
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
        public EcoResVariantConfigurationTechnologyType VariantConfigurationTechnology { get; set; }
        public string RetailProductCategoryName { get; set; }
        public EcoResProductType ProductType { get; set; }
        public string STCCCode { get; set; }
        public string TrackingDimensionGroupName { get; set; }
        public string StorageDimensionGroupName { get; set; }

        public ProductMasterWriteDTO()
        {       
            AreIdenticalConfigurationsAllowed = NoYes.No;
            HarmonizedSystemCode = "";
            IsAutomaticVariantGenerationEnabled = NoYes.Yes;
            IsCatchWeightProduct = NoYes.No;
            IsProductKit = NoYes.No;
            IsProductVariantUnitConversionEnabled = NoYes.No;
            NMFCCode = "";
            //ProductColorGroupId = "Basic";
            //ProductDescription = "";
            //ProductDimensionGroupName = "SizeCol";
            //ProductName = dp.ProductName;
            //ProductNumber = dp.ProductNumber;
            //ProductSearchName = dp.ProductSearchName;
            //ProductSizeGroupId = "10-18";
            //ProductStyleGroupId = "";
            VariantConfigurationTechnology = EcoResVariantConfigurationTechnologyType.PredefinedVariants;
            //RetailProductCategoryName = "";
            ProductType = EcoResProductType.Item;
            STCCCode = "";
            TrackingDimensionGroupName = "None";
            StorageDimensionGroupName = "SiteWhsCha";
        }

    }
}
