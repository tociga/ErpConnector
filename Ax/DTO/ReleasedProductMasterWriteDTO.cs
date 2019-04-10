using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using System;

namespace ErpConnector.Ax.DTO
{
    public class ReleasedProductMasterWriteDTO
    {
        public ReleasedProductMasterWriteDTO()
        {

        }

        public ReleasedProductMasterWriteDTO(string itemNumber, string searchName, string primarVendorNo, decimal? salePrice, decimal? costPrice)
        {
            PrimaryVendorAccountNumber = primarVendorNo;
            SalesPrice = salePrice.HasValue ? salePrice.Value : 0.0m;
            UnitCost = costPrice.HasValue ? costPrice.Value : 0.0m;
            ItemNumber = itemNumber;
            ProductNumber = itemNumber;
            TransferOrderOverdeliveryPercentage = 0;
            SalesUnitSymbol = "UNIT";
            ProductionConsumptionWidthConversionFactor = 0;
            IsPurchasePriceAutomaticallyUpdated = NoYes.No;
            IsPurchaseWithholdingTaxCalculated = NoYes.No;
            TransferOrderUnderdeliveryPercentage = 0;
            IsDeliveredDirectly = NoYes.No;
            SalesSupplementaryProductProductGroupId = "";
            SalesMultilineDiscountProductGroupCode = "";
            WillTotalPurchaseDiscountCalculationIncludeProduct = NoYes.Yes;
            IsVariantShelfLabelsPrintingEnabled = NoYes.No;
            ProductionGroupId = "";
            OriginStateId = "";
            RevenueABCCode = ABC.None;
            NecessaryProductionWorkingTimeSchedulingPropertyId = "";
            IntrastatCommodityCode = "";
            SalesRebateProductGroupId = "";
            UnitCostDate = new DateTimeOffset(new DateTime(2012, 10, 6, 12, 0, 0, 0));
            DefaultProductSizeId = "";
            AlternativeProductStyleId = "";
            PotencyBaseAttributeId = "";
            AlternativeProductUsageCondition = ItemNumAlternative.Never;
            PurchaseUnderdeliveryPercentage = 0;
            DefaultProductStyleId = "";
            AlternativeProductSizeId = "";
            WillInventoryIssueAutomaticallyReportAsFinished = NoYes.No;
            ProductVolume = 0;
            TareProductWeight = 0;
            IsPhantom = NoYes.No;
            DefaultProductConfigurationId = "";
            FlushingPrinciple = ProdFlushingPrincipItem.Start;
            VariableScrapPercentage = 0;
            ArrivalHandlingTime = 0;
            SearchName = searchName;
            IsUnitCostIncludingCharges = NoYes.No;
            AlternativeProductColorId = "";
            PhysicalDimensionGroupId = "";
            MinimumCatchWeightQuantity = 0;
            SalesChargeProductGroupId = "";
            WillPickingWorkbenchApplyBoxingLogic = NoYes.No;
            ItemFiscalClassificationExceptionCode = "";
            InventoryReservationHierarchyName = "";
            IsZeroPricePOSRegistrationAllowed = NoYes.No;
            IntrastatChargePercentage = 0;
            IsScaleProduct = NoYes.No;
            PlanningFormulaItemNumber = "";
            ProductFiscalInformationType = "";
            StorageDimensionGroupName = "SiteWhsCha";
            ShelfAdvicePeriodDays = 0;
            ContinuityScheduleId = "";
            VendorInvoiceLineMatchingPolicy = PurchMatchingPolicyWithNotSetOption.NotSet;
            MustKeyInCommentAtPOSRegister = NoYes.No;
            SalesPriceQuantity = 1;
            ServiceFiscalInformationCode = "";
            BarcodeSetupId = "";
            IsUnitCostAutomaticallyUpdated = NoYes.No;
            PotencyBaseAttibuteTargetValue = 0;
            DefaultReceivingQuantity = 0;
            ProductionConsumptionDensityConversionFactor = 0;
            PurchaseItemWithholdingTaxGroupCode = "";
            PurchasePricingPrecision = 0;
            UnitCostQuantity = 1;
            IsRestrictedForCoupons = NoYes.No;
            SellStartDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0));
            SellEndDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0));
            ConstantScrapQuantity = 0;
            BatchNumberGroupCode = "";
            CostCalculationGroupId = "";
            PackingDutyQuantity = 0;
            AlternativeProductConfigurationId = "";            
            DefaultProductColorId = "";
            IsSalesPriceIncludingCharges = NoYes.No;
            ProductionType = PmfProductType.None;
            WillTotalSalesDiscountCalculationIncludeProduct = NoYes.Yes;
            PotencyBaseAttributeValueEntryEvent = PDSPotencyAttribRecordingEnum.PurchProdReceipt;
            ItemModelGroupId = "DEFAULT";
            PurchaseMultilineDiscountProductGroupCode = "";
            SalesChargesQuantity = 0;
            SalesPriceCalculationContributionRatio = 0;
            IsSalesPriceAdjustmentAllowed = NoYes.No;
            SalesPriceCalculationChargesPercentage = 0;
            ShippingAndReceivingSortOrderCode = 0;
            GrossProductHeight = 0;
            ProductLifeCycleValidFromDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0));
            POSRegistrationPlannedBlockedDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0));
            SalesItemWithholdingTaxGroupCode = "";
            PurchaseLineDiscountProductGroupCode = "";
            PurchasePriceDate = new DateTimeOffset(new DateTime(2012, 10, 6, 12, 0, 0, 0));
            IsIntercompanyPurchaseUsageBlocked = NoYes.No;
            NGPCode = 0;
            ShelfLifePeriodDays = 0;
            UnitConversionSequenceGroupId = "";
            ProductionConsumptionHeightConversionFactor = 0;
            BOMUnitSymbol = "";
            MaximumCatchWeightQuantity = 0;
            PurchasePriceQuantity = 1;
            PurchasePrice = 0m;
            ProductLifeCycleSeasonCode = "";
            IsDiscountPOSRegistrationProhibited = NoYes.No;
            ProductLifeCycleValidToDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0));
            PurchaseOverdeliveryPercentage = 0;
            TrackingDimensionGroupName = "None";
            FixedSalesPriceCharges = 0;
            ProductTaxationOrigin = FITaxationOrigin_BR.National;
            ProductionPoolId = "";
            ValueABCCode = ABC.None;
            PurchaseUnitSymbol = "UNIT";
            PurchaseSupplementaryProductProductGroupId = "";
            AlternativeItemNumber = "";
            ProductCoverageGroupId = "";
            CostGroupId = "";
            IsPurchasePriceIncludingCharges = NoYes.No;
            IsShipAloneEnabled = NoYes.No;
            RawMaterialPickingPrinciple = WHSAllowMaterialOverPick.Staging;
            FixedPurchasePriceCharges = 0;
            FreightAllocationGroupId = "";
            ContinuityEventDuration = 0;
            DefaultDirectDeliveryWarehouse = "";
            SalesPriceDate = new DateTimeOffset(new DateTime(2012, 10, 6, 12, 0, 0, 0));
            OriginCountryRegionId = "";
            DefaultOrderType = ReqPOType.Purch;
            IsPOSRegistrationQuantityNegative = NoYes.No;
            PurchaseChargesQuantity = 0;
            MaximumPickQuantity = 0;
            SalesUnderdeliveryPercentage = 0;
            IsInstallmentEligible = NoYes.No;
            KeyInQuantityRequirementsAtPOSRegister = RetailQtyKeyingRequirement.NotMandatory;
            CommissionProductGroupId = "";
            IsIntercompanySalesUsageBlocked = NoYes.No;
            YieldPercentage = 0;
            BaseSalesPriceSource = SalesPriceModelBasic.PurchPrice;
            IsSalesWithholdingTaxCalculated = NoYes.No;
            ApprovedVendorCheckMethod = PdsVendorCheckItem.NoCheck;
            BestBeforePeriodDays = 0;
            GrossDepth = 0;
            PurchaseRebateProductGroupId = "";
            PackSizeCategoryId = "";
            PackageClassId = "";
            FixedCostCharges = 0;            
            SerialNumberGroupCode = "";
            CarryingCostABCCode = ABC.None;
            SalesLineDiscountProductGroupCode = "";
            IsPOSRegistrationBlocked = NoYes.No;
            POSRegistrationBlockedDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0)); ;
            ProjectCategoryId = "";
            PurchasePriceToleranceGroupId = "";
            AreTransportationManagementProcessesEnabled = NoYes.Yes;
            IsExemptFromAutomaticNotificationAndCancellation = NoYes.No;
            PackingMaterialGroupId = "";
            InventoryUnitSymbol = "UNIT";
            ComparisonPriceBaseUnitSymbol = "";
            WillWorkCenterPickingAllowNegativeInventory = NoYes.No;
            IsICMSTaxAppliedOnService = NoYes.No;
            KeyInPriceRequirementsAtPOSRegister = RetailPriceKeyingRequirement.NotMandatory;
            ApproximateSalesTaxPercentage = 0;
            POSRegistrationActivationDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0)); ;
            WillInventoryReceiptIgnoreFlushingPrinciple = NoYes.No;
            NetProductWeight = 0;
            CostChargesQuantity = 0;
            BatchMergeDateCalculationMethod = InventBatchMergeDateCalculationMethod.Manual;
            SalesPriceCalculationModel = SalesPriceModel.None;
            PurchaseChargeProductGroupId = "";
            SalesOverdeliveryPercentage = 0;
            DefaultLedgerDimensionDisplayValue = "";
            SalesPricingPrecision = 0;
            MarginABCCode = ABC.None;
            CatchWeightUnitSymbol = "";
            WarehouseMobileDeviceDescriptionLine1 = "";
            WarehouseMobileDeviceDescriptionLine2 = "";
            ProductionConsumptionDepthConversionFactor = 0;
            ItemFiscalClassificationCode = "";
            PackageHandlingTime = 0;
            IsUnitCostProductVariantSpecific = NoYes.No;
            BuyerGroupId = "";
            GrossProductWidth = 0;
            ShipStartDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0));

        }
        public decimal TransferOrderOverdeliveryPercentage { get; set; }
        public string SalesUnitSymbol { get; set; }
        public int ProductionConsumptionWidthConversionFactor { get; set; }        
        public NoYes IsPurchasePriceAutomaticallyUpdated { get; set; }
        public NoYes IsPurchaseWithholdingTaxCalculated { get; set; }
        public decimal TransferOrderUnderdeliveryPercentage { get; set; }
        public NoYes IsDeliveredDirectly { get; set; }
        public string SalesSupplementaryProductProductGroupId { get; set; }
        public string SalesMultilineDiscountProductGroupCode { get; set; }
        public NoYes WillTotalPurchaseDiscountCalculationIncludeProduct { get; set; }
        public NoYes IsVariantShelfLabelsPrintingEnabled { get; set; }
        public string ProductionGroupId { get; set; }
        public string OriginStateId { get; set; }
        public ABC RevenueABCCode { get; set; }
        public string NecessaryProductionWorkingTimeSchedulingPropertyId { get; set; }
        public string IntrastatCommodityCode { get; set; }
        public string SalesRebateProductGroupId { get; set; }
        public DateTimeOffset UnitCostDate { get; set; }
        public string DefaultProductSizeId { get; set; }
        public string AlternativeProductStyleId { get; set; }
        public string PotencyBaseAttributeId { get; set; }
        public ItemNumAlternative AlternativeProductUsageCondition { get; set; }
        public decimal PurchaseUnderdeliveryPercentage { get; set; }
        public string DefaultProductStyleId { get; set; }
        public string AlternativeProductSizeId { get; set; }
        public NoYes WillInventoryIssueAutomaticallyReportAsFinished { get; set; }
        public decimal ProductVolume { get; set; }
        public decimal TareProductWeight { get; set; }
        public string ItemNumber { get; set; }
        public NoYes IsPhantom { get; set; }
        public string DefaultProductConfigurationId { get; set; }
        public ProdFlushingPrincipItem FlushingPrinciple { get; set; }
        public decimal VariableScrapPercentage { get; set; }
        public int ArrivalHandlingTime { get; set; }
        public string SearchName { get; set; }
        public NoYes IsUnitCostIncludingCharges { get; set; }
        public string AlternativeProductColorId { get; set; }
        public string PhysicalDimensionGroupId { get; set; }
        public int MinimumCatchWeightQuantity { get; set; }
        public string SalesChargeProductGroupId { get; set; }
        
        public NoYes WillPickingWorkbenchApplyBoxingLogic { get; set; }
        public string ItemFiscalClassificationExceptionCode { get; set; }
        public string InventoryReservationHierarchyName { get; set; }
        public NoYes IsZeroPricePOSRegistrationAllowed { get; set; }
        public decimal IntrastatChargePercentage { get; set; }
        public NoYes IsScaleProduct { get; set; }
        public string PlanningFormulaItemNumber { get; set; }
        public string ProductFiscalInformationType { get; set; }
        public string StorageDimensionGroupName { get; set; }
        public int ShelfAdvicePeriodDays { get; set; }
        public string ContinuityScheduleId { get; set; }
        public PurchMatchingPolicyWithNotSetOption VendorInvoiceLineMatchingPolicy { get; set; }
        
        public NoYes MustKeyInCommentAtPOSRegister { get; set; }
        public decimal SalesPriceQuantity { get; set; }
        public string ServiceFiscalInformationCode { get; set; }
        public string BarcodeSetupId { get; set; }
        public NoYes IsUnitCostAutomaticallyUpdated { get; set; }
        public decimal PotencyBaseAttibuteTargetValue { get; set; }
        public decimal DefaultReceivingQuantity { get; set; }
        public decimal ProductionConsumptionDensityConversionFactor { get; set; }
        public string PurchaseItemWithholdingTaxGroupCode { get; set; }
        public int PurchasePricingPrecision { get; set; }
        public decimal UnitCostQuantity { get; set; }
        public NoYes IsRestrictedForCoupons { get; set; }
        public DateTimeOffset SellStartDate{ get; set; }
        public DateTimeOffset SellEndDate{ get; set; }
        public decimal ConstantScrapQuantity { get; set; }
        public string BatchNumberGroupCode { get; set; }
        public string CostCalculationGroupId { get; set; }
        public decimal PackingDutyQuantity { get; set; }
        public string AlternativeProductConfigurationId { get; set; }
        public decimal SalesPrice { get; set; }
        public string DefaultProductColorId { get; set; }
        public NoYes IsSalesPriceIncludingCharges { get; set; }
        public PmfProductType ProductionType { get; set; }
        public NoYes WillTotalSalesDiscountCalculationIncludeProduct { get; set; }
        public PDSPotencyAttribRecordingEnum PotencyBaseAttributeValueEntryEvent { get; set; }
        public string ItemModelGroupId { get; set; }
        public string PurchaseMultilineDiscountProductGroupCode { get; set; }
        
        public decimal SalesChargesQuantity { get; set; }
        public decimal SalesPriceCalculationContributionRatio { get; set; }
        public string ProductGroupId { get; set; }
        public NoYes IsSalesPriceAdjustmentAllowed { get; set; }
        public decimal SalesPriceCalculationChargesPercentage { get; set; }
        public int ShippingAndReceivingSortOrderCode { get; set; }
        public decimal GrossProductHeight { get; set; }
        public DateTimeOffset ProductLifeCycleValidFromDate{ get; set; }
        
        public DateTimeOffset POSRegistrationPlannedBlockedDate{ get; set; }
        public string SalesItemWithholdingTaxGroupCode { get; set; }
        public string PurchaseLineDiscountProductGroupCode { get; set; }
        public DateTimeOffset PurchasePriceDate { get; set; }
        public NoYes IsIntercompanyPurchaseUsageBlocked { get; set; }
        public int NGPCode { get; set; }
        public int ShelfLifePeriodDays { get; set; }
        public string UnitConversionSequenceGroupId { get; set; }
        public decimal ProductionConsumptionHeightConversionFactor { get; set; }
        public string BOMUnitSymbol { get; set; }
        public decimal MaximumCatchWeightQuantity { get; set; }
        public decimal PurchasePriceQuantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public string ProductLifeCycleSeasonCode { get; set; }
        public NoYes IsDiscountPOSRegistrationProhibited { get; set; }
        public DateTimeOffset ProductLifeCycleValidToDate{ get; set; }
        public decimal PurchaseOverdeliveryPercentage { get; set; }
        public string TrackingDimensionGroupName { get; set; }
        public decimal FixedSalesPriceCharges { get; set; }
        public FITaxationOrigin_BR ProductTaxationOrigin { get; set; }
        public string ProductionPoolId { get; set; }
        public ABC ValueABCCode { get; set; }
        public string PurchaseUnitSymbol { get; set; }
        public string PurchaseSupplementaryProductProductGroupId { get; set; }
        public string AlternativeItemNumber { get; set; }
        public string SalesSalesTaxItemGroupCode { get; set; }
        public string ProductCoverageGroupId { get; set; }
        public string CostGroupId { get; set; }
        public NoYes IsPurchasePriceIncludingCharges { get; set; }
        public NoYes IsShipAloneEnabled { get; set; }
        public string ProductNumber { get; set; }
        public WHSAllowMaterialOverPick RawMaterialPickingPrinciple { get; set; }
        public decimal FixedPurchasePriceCharges { get; set; }
        public string FreightAllocationGroupId { get; set; }
        public int ContinuityEventDuration { get; set; }
        public string PurchaseSalesTaxItemGroupCode { get; set; }
        public string DefaultDirectDeliveryWarehouse { get; set; }
        
        public DateTimeOffset SalesPriceDate { get; set; }
        public string OriginCountryRegionId { get; set; }
        public ReqPOType DefaultOrderType { get; set; }
        public NoYes IsPOSRegistrationQuantityNegative { get; set; }
        public decimal PurchaseChargesQuantity { get; set; }
        public string PrimaryVendorAccountNumber { get; set; }
        public int MaximumPickQuantity { get; set; }
        public int SalesUnderdeliveryPercentage { get; set; }
        public NoYes IsInstallmentEligible { get; set; }
        public RetailQtyKeyingRequirement KeyInQuantityRequirementsAtPOSRegister { get; set; }
        public string CommissionProductGroupId { get; set; }
        public NoYes IsIntercompanySalesUsageBlocked { get; set; }
        public decimal YieldPercentage { get; set; }
        public SalesPriceModelBasic BaseSalesPriceSource { get; set; }
        public NoYes IsSalesWithholdingTaxCalculated { get; set; }
        public PdsVendorCheckItem ApprovedVendorCheckMethod { get; set; }
        public int BestBeforePeriodDays { get; set; }
        public decimal GrossDepth { get; set; }
        public string PurchaseRebateProductGroupId { get; set; }
        public string PackSizeCategoryId { get; set; }
        public string PackageClassId { get; set; }
        public decimal FixedCostCharges { get; set; }
        public decimal UnitCost { get; set; }
        public string SerialNumberGroupCode { get; set; }
        public ABC CarryingCostABCCode { get; set; }
        public string SalesLineDiscountProductGroupCode { get; set; }
        public NoYes IsPOSRegistrationBlocked { get; set; }
        public DateTimeOffset POSRegistrationBlockedDate{ get; set; }
        public string ProjectCategoryId { get; set; }
        public string PurchasePriceToleranceGroupId { get; set; }
        public NoYes AreTransportationManagementProcessesEnabled { get; set; }
        public NoYes IsExemptFromAutomaticNotificationAndCancellation { get; set; }
        public string PackingMaterialGroupId { get; set; }
        public string InventoryUnitSymbol { get; set; }
        public string ComparisonPriceBaseUnitSymbol { get; set; }
        public NoYes WillWorkCenterPickingAllowNegativeInventory { get; set; }
        public NoYes IsICMSTaxAppliedOnService { get; set; }
        
        public RetailPriceKeyingRequirement KeyInPriceRequirementsAtPOSRegister { get; set; }
        public decimal ApproximateSalesTaxPercentage { get; set; }
        public DateTimeOffset POSRegistrationActivationDate{ get; set; }
        public NoYes WillInventoryReceiptIgnoreFlushingPrinciple { get; set; }
        public decimal NetProductWeight { get; set; }
        public int CostChargesQuantity { get; set; }
        public InventBatchMergeDateCalculationMethod BatchMergeDateCalculationMethod { get; set; }
        public SalesPriceModel SalesPriceCalculationModel { get; set; }
        public string PurchaseChargeProductGroupId { get; set; }
        public int SalesOverdeliveryPercentage { get; set; }
        public string DefaultLedgerDimensionDisplayValue { get; set; }
        public int SalesPricingPrecision { get; set; }
        public ABC MarginABCCode { get; set; }
        public string CatchWeightUnitSymbol { get; set; }
        public string WarehouseMobileDeviceDescriptionLine1 { get; set; }
        public string WarehouseMobileDeviceDescriptionLine2 { get; set; }
        public decimal ProductionConsumptionDepthConversionFactor { get; set; }
        public string ItemFiscalClassificationCode { get; set; }
        public int PackageHandlingTime { get; set; }
        public NoYes IsUnitCostProductVariantSpecific { get; set; }
        public string BuyerGroupId { get; set; }
        public int GrossProductWidth { get; set; }
        public DateTimeOffset ShipStartDate{ get; set; }

    }
}
