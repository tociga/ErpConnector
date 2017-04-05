using AxConnect.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.DTO
{
    public class ReleasedProductMasterWriteDTO
    {
        public int TransferOrderOverdeliveryPercentage { get; set; }
        public string SalesUnitSymbol { get; set; }
        public int ProductionConsumptionWidthConversionFactor { get; set; }        
        public NoYes IsPurchasePriceAutomaticallyUpdated { get; set; }
        public NoYes IsPurchaseWithholdingTaxCalculated { get; set; }
        public int TransferOrderUnderdeliveryPercentage { get; set; }
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
        public int PurchaseUnderdeliveryPercentage { get; set; }
        public string DefaultProductStyleId { get; set; }
        public string AlternativeProductSizeId { get; set; }
        public NoYes WillInventoryIssueAutomaticallyReportAsFinished { get; set; }
        public int ProductVolume { get; set; }
        public int TareProductWeight { get; set; }
        public string ItemNumber { get; set; }
        public NoYes IsPhantom { get; set; }
        public string DefaultProductConfigurationId { get; set; }
        public ProdFlushingPrincipItem FlushingPrinciple { get; set; }
        public int VariableScrapPercentage { get; set; }
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
        public int IntrastatChargePercentage { get; set; }
        public NoYes IsScaleProduct { get; set; }
        public string PlanningFormulaItemNumber { get; set; }
        public string ProductFiscalInformationType { get; set; }
        public string StorageDimensionGroupName { get; set; }
        public int ShelfAdvicePeriodDays { get; set; }
        public string ContinuityScheduleId { get; set; }
        public PurchMatchingPolicyWithNotSetOption VendorInvoiceLineMatchingPolicy { get; set; }
        
        public NoYes MustKeyInCommentAtPOSRegister { get; set; }
        public int SalesPriceQuantity { get; set; }
        public string ServiceFiscalInformationCode { get; set; }
        public string BarcodeSetupId { get; set; }
        public NoYes IsUnitCostAutomaticallyUpdated { get; set; }
        public int PotencyBaseAttibuteTargetValue { get; set; }
        public int DefaultReceivingQuantity { get; set; }
        public int ProductionConsumptionDensityConversionFactor { get; set; }
        public string PurchaseItemWithholdingTaxGroupCode { get; set; }
        public int PurchasePricingPrecision { get; set; }
        public int UnitCostQuantity { get; set; }
        public NoYes IsRestrictedForCoupons { get; set; }
        public DateTimeOffset SellStartDate{ get; set; }
        public DateTimeOffset SellEndDate{ get; set; }
        public int ConstantScrapQuantity { get; set; }
        public string BatchNumberGroupCode { get; set; }
        public string CostCalculationGroupId { get; set; }
        public int PackingDutyQuantity { get; set; }
        public string AlternativeProductConfigurationId { get; set; }
        public decimal SalesPrice { get; set; }
        public string DefaultProductColorId { get; set; }
        public NoYes IsSalesPriceIncludingCharges { get; set; }
        public PmfProductType ProductionType { get; set; }
        public NoYes WillTotalSalesDiscountCalculationIncludeProduct { get; set; }
        public PDSPotencyAttribRecordingEnum PotencyBaseAttributeValueEntryEvent { get; set; }
        public string ItemModelGroupId { get; set; }
        public string PurchaseMultilineDiscountProductGroupCode { get; set; }
        
        public int SalesChargesQuantity { get; set; }
        public int SalesPriceCalculationContributionRatio { get; set; }
        public string ProductGroupId { get; set; }
        public NoYes IsSalesPriceAdjustmentAllowed { get; set; }
        public int SalesPriceCalculationChargesPercentage { get; set; }
        public int ShippingAndReceivingSortOrderCode { get; set; }
        public int GrossProductHeight { get; set; }
        public DateTimeOffset ProductLifeCycleValidFromDate{ get; set; }
        
        public DateTimeOffset POSRegistrationPlannedBlockedDate{ get; set; }
        public string SalesItemWithholdingTaxGroupCode { get; set; }
        public string PurchaseLineDiscountProductGroupCode { get; set; }
        public DateTimeOffset PurchasePriceDate { get; set; }
        public NoYes IsIntercompanyPurchaseUsageBlocked { get; set; }
        public int NGPCode { get; set; }
        public int ShelfLifePeriodDays { get; set; }
        public string UnitConversionSequenceGroupId { get; set; }
        public int ProductionConsumptionHeightConversionFactor { get; set; }
        public string BOMUnitSymbol { get; set; }
        public int MaximumCatchWeightQuantity { get; set; }
        public int PurchasePriceQuantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public string ProductLifeCycleSeasonCode { get; set; }
        public NoYes IsDiscountPOSRegistrationProhibited { get; set; }
        public DateTimeOffset ProductLifeCycleValidToDate{ get; set; }
        public int PurchaseOverdeliveryPercentage { get; set; }
        public string TrackingDimensionGroupName { get; set; }
        public int FixedSalesPriceCharges { get; set; }
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
        public int FixedPurchasePriceCharges { get; set; }
        public string FreightAllocationGroupId { get; set; }
        public int ContinuityEventDuration { get; set; }
        public string PurchaseSalesTaxItemGroupCode { get; set; }
        public string DefaultDirectDeliveryWarehouse { get; set; }
        
        public DateTimeOffset SalesPriceDate { get; set; }
        public string OriginCountryRegionId { get; set; }
        public ReqPOType DefaultOrderType { get; set; }
        public NoYes IsPOSRegistrationQuantityNegative { get; set; }
        public int PurchaseChargesQuantity { get; set; }
        public string PrimaryVendorAccountNumber { get; set; }
        public int MaximumPickQuantity { get; set; }
        public int SalesUnderdeliveryPercentage { get; set; }
        public NoYes IsInstallmentEligible { get; set; }
        public RetailQtyKeyingRequirement KeyInQuantityRequirementsAtPOSRegister { get; set; }
        public string CommissionProductGroupId { get; set; }
        public NoYes IsIntercompanySalesUsageBlocked { get; set; }
        public int YieldPercentage { get; set; }
        public SalesPriceModelBasic BaseSalesPriceSource { get; set; }
        public NoYes IsSalesWithholdingTaxCalculated { get; set; }
        public PdsVendorCheckItem ApprovedVendorCheckMethod { get; set; }
        public int BestBeforePeriodDays { get; set; }
        public int GrossDepth { get; set; }
        public string PurchaseRebateProductGroupId { get; set; }
        public string PackSizeCategoryId { get; set; }
        public string PackageClassId { get; set; }
        public int FixedCostCharges { get; set; }
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
        public int ApproximateSalesTaxPercentage { get; set; }
        public DateTimeOffset POSRegistrationActivationDate{ get; set; }
        public NoYes WillInventoryReceiptIgnoreFlushingPrinciple { get; set; }
        public int NetProductWeight { get; set; }
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
        public int ProductionConsumptionDepthConversionFactor { get; set; }
        public string ItemFiscalClassificationCode { get; set; }
        public int PackageHandlingTime { get; set; }
        public NoYes IsUnitCostProductVariantSpecific { get; set; }
        public string BuyerGroupId { get; set; }
        public int GrossProductWidth { get; set; }
        public DateTimeOffset ShipStartDate{ get; set; }

    }
}
