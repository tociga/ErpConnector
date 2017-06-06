using System;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;

namespace ErpConnector.Ax.DTO
{ 
    public class ReleasedDistinctProductsReadDTO
    {
        EcoResProductType _productType;
        ABC _marginAbc;
        ABC _valueAbc;
        ABC _revenueAbc;
        ABC _carryingCostABCCode;
        NoYes _isSalesWithholdingTaxCalculated;
        NoYes _isPOSRegistrationQuantityNegative;
        NoYes _isPurchaseWithholdingTaxCalculated;
        NoYes _isPhantom;
        NoYes _isPurchasePriceIncludingCharges;
        NoYes _isICMSTaxAppliedOnService;
        NoYes _isExemptFromAutomaticNotificationAndCancellation;
        NoYes _willPickingWorkbenchApplyBoxingLogic;
        NoYes _isDeliveredDirectly;
        NoYes _isScaleProduct;
        NoYes _isUnitCostIncludingCharges;
        NoYes _areTransportationManagementProcessesEnabled;
        NoYes _isShipAloneEnabled;
        NoYes _isSalesPriceIncludingCharges;
        NoYes _willTotalPurchaseDiscountCalculationIncludeProduct;
        NoYes _isIntercompanyPurchaseUsageBlocked;
        NoYes _isDiscountPOSRegistrationProhibited;

        WHSAllowMaterialOverPick _rawMaterialPick;
        PmfProductType _productionType;
        PurchMatchingPolicyWithNotSetOption _vendorInvoiceLineMatchingPolicy;
        SalesPriceModel _salesPriceModel;
        InventBatchMergeDateCalculationMethod _batchMergeDateCalculationMethod;
        PdsVendorCheckItem _approvedVendorCheckMethod;
        ProdFlushingPrincipItem _flushingPrinciple;
        NoYes _willInventoryIssueAutomaticallyReportAsFinished;
        NoYes _mustKeyInCommentAtPOSRegister;
        PDSPotencyAttribRecordingEnum _potencyBaseAttributeValueEntryEvent;
        RetailPriceKeyingRequirement _keyInPriceRequirementsAtPOSRegister;
        NoYes _isIntercompanySalesUsageBlocked;
        NoYes _isZeroPricePOSRegistrationAllowed;
        NoYes _isUnitCostAutomaticallyUpdated;
        FITaxationOrigin_BR _productTaxationOrigin;
        SalesPriceModelBasic _baseSalesPriceSource;
        NoYes _isInstallmentEligible;
        NoYes _willTotalSalesDiscountCalculationIncludeProduct;
        ItemNumAlternative _alternativeProductUsageCondition;
        NoYes _isVariantShelfLabelsPrintingEnabled;
        RetailQtyKeyingRequirement _keyInQuantityRequirementsAtPOSRegister;
        ReqPOType _defaultOrderType;
        NoYes _isPOSRegistrationBlocked;
        NoYes _willWorkCenterPickingAllowNegativeInventory;
        NoYes _isRestrictedForCoupons;
        NoYes _isSalesPriceAdjustmentAllowed;
        NoYes _isPurchasePriceAutomaticallyUpdated;
        NoYes _willInventoryReceiptIgnoreFlushingPrinciple;

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
        public int StandardPalletQty { get; set; }
        public string BuyerGroupId { get; set; }
        public long ProductRecId { get; set; }
        public decimal FixedSalesPriceCharges { get; set; }
        public object IsPhantom
        {
            get { return (int)_isPhantom; }
            set { _isPhantom = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }

        public object IsPurchasePriceIncludingCharges
        {
            get { return (int)_isPurchasePriceIncludingCharges; }
            set { _isPurchasePriceIncludingCharges = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }

        public string ItemFiscalClassificationCode
        {
            get;
            set;
        }
        public object IsICMSTaxAppliedOnService
        {
            get { return (int)_isICMSTaxAppliedOnService; }
            set { _isICMSTaxAppliedOnService = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }


        public int ShippingAndReceivingSortOrderCode { get; set; }
        public int ProductionConsumptionWidthConversionFactor { get; set; }
        public string AlternativeProductSizeId { get; set; }
        public object RawMaterialPickingPrinciple
        {
            get { return (int)_rawMaterialPick; }
            set { _rawMaterialPick = DTOUtil.GetEnumFromObj<WHSAllowMaterialOverPick>(value, WHSAllowMaterialOverPick.Staging); }
        }
        public int ProductionConsumptionDepthConversionFactor { get; set; }
        public string ItemModelGroupId { get; set; }
        public object IsSalesWithholdingTaxCalculated
        {
            get { return (int)_isSalesWithholdingTaxCalculated; }
            set { _isSalesWithholdingTaxCalculated = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }

        public string TrackingDimensionGroupName { get; set; }
        public string PurchaseSalesTaxItemGroupCode { get; set; }
        public string PlanningFormulaItemNumber { get; set; }
        public string WarehouseMobileDeviceDescriptionLine1 { get; set; }
        public string WarehouseMobileDeviceDescriptionLine2 { get; set; }
        public string SalesItemWithholdingTaxGroupCode { get; set; }
        public object IsPOSRegistrationQuantityNegative
        {
            get { return (int)_isPOSRegistrationQuantityNegative; }
            set { _isPOSRegistrationQuantityNegative = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }
        public DateTime POSRegistrationPlannedBlockedDate { get; set; }
        public DateTime SellEndDate { get; set; }
        public object IsPurchaseWithholdingTaxCalculated
        {
            get { return (int)_isPurchaseWithholdingTaxCalculated; }
            set { _isPurchaseWithholdingTaxCalculated = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }
        public string DefaultLedgerDimensionDisplayValue { get; set; }
        public string CommissionProductGroupId { get; set; }
        public object IsExemptFromAutomaticNotificationAndCancellation
        {
            get { return (int)_isExemptFromAutomaticNotificationAndCancellation; }
            set { _isExemptFromAutomaticNotificationAndCancellation = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }
        public object ProductionType
        {
            get { return (int)_productionType; }
            set { _productionType = DTOUtil.GetEnumFromObj<PmfProductType>(value, PmfProductType.None); }
        }
        public string ProductionPoolId { get; set; }
        public string SalesSupplementaryProductProductGroupId { get; set; }
        public string StorageDimensionGroupName { get; set; }
        public int PurchasePricingPrecision { get; set; }
        public string BOMUnitSymbol { get; set; }
        public int SalesPriceCalculationContributionRatio { get; set; }
        public string CatchWeightUnitSymbol { get; set; }

        public object VendorInvoiceLineMatchingPolicy
        {
            get { return (int)_vendorInvoiceLineMatchingPolicy; }
            set { _vendorInvoiceLineMatchingPolicy = DTOUtil.GetEnumFromObj<PurchMatchingPolicyWithNotSetOption>(value, PurchMatchingPolicyWithNotSetOption.NotSet); }
        }
        public DateTime SellStartDate { get; set; }
        public string PhysicalDimensionGroupId { get; set; }
        public object CarryingCostABCCode
        {
            get { return (int)_carryingCostABCCode; }
            set { _carryingCostABCCode = DTOUtil.GetEnumFromObj<ABC>(value, ABC.None); }
        }
        public int TransferOrderOverdeliveryPercentage { get; set; }
        public string UnitConversionSequenceGroupId { get; set; }
        public object WillPickingWorkbenchApplyBoxingLogic
        {
            get { return (int)_willPickingWorkbenchApplyBoxingLogic; }
            set { _willPickingWorkbenchApplyBoxingLogic = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }
        public object IsDeliveredDirectly
        {
            get { return (int)_isDeliveredDirectly; }
            set { _isDeliveredDirectly = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }

        public string SalesGSTReliefCategoryCode { get; set; }
        public object IsScaleProduct
        {
            get { return (int)_isScaleProduct; }
            set { _isScaleProduct = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }

        public string AlternativeProductColorId { get; set; }
        public int FixedPurchasePriceCharges { get; set; }
        public object IsUnitCostIncludingCharges
        {
            get { return (int)_isUnitCostIncludingCharges; }
            set { _isUnitCostIncludingCharges = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }

        public DateTime ShipStartDate { get; set; }
        public decimal SalesPrice { get; set; }
        public object SalesPriceCalculationModel
        {
            get { return (int)_salesPriceModel; }
            set { _salesPriceModel = DTOUtil.GetEnumFromObj<SalesPriceModel>(value, SalesPriceModel.None); }
        }
        public int ArrivalHandlingTime { get; set; }
        public string IntrastatCommodityCode { get; set; }
        public object AreTransportationManagementProcessesEnabled
        {
            get { return (int)_areTransportationManagementProcessesEnabled; }
            set { _areTransportationManagementProcessesEnabled = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }
        public object IsShipAloneEnabled
        {
            get { return (int)_isShipAloneEnabled; }
            set { _isShipAloneEnabled = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }

        public int ProductionConsumptionDensityConversionFactor { get; set; }
        public DateTime PurchasePriceDate { get; set; }
        public int SalesPricingPrecision { get; set; }
        public int PurchaseChargesQuantity { get; set; }
        public string ProductSearchName { get; set; }
        public DateTime UnitCostDate { get; set; }
        public int VariableScrapPercentage { get; set; }
        public int MaximumPickQuantity { get; set; }
        public string AlternativeProductStyleId { get; set; }
        public string BarcodeSetupId { get; set; }
        public object IsSalesPriceIncludingCharges
        {
            get { return (int)_isSalesPriceIncludingCharges; }
            set { _isSalesPriceIncludingCharges = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }
        public int PurchasePriceQuantity { get; set; }
        public string PurchaseChargeProductGroupId { get; set; }
        public string ContinuityScheduleId { get; set; }
        public int FixedCostCharges { get; set; }
        public string CostGroupId { get; set; }
        public string SalesLineDiscountProductGroupCode { get; set; }
        public DateTime POSRegistrationActivationDate { get; set; }
        public int MaximumCatchWeightQuantity { get; set; }
        public string ServiceFiscalInformationCode { get; set; }
        public DateTime ProductLifeCycleValidToDate { get; set; }
        public string PurchaseSupplementaryProductProductGroupId { get; set; }
        public string InventoryUnitSymbol { get; set; }
        public object WillTotalPurchaseDiscountCalculationIncludeProduct
        {
            get { return (int)_willTotalPurchaseDiscountCalculationIncludeProduct; }
            set { _willTotalPurchaseDiscountCalculationIncludeProduct = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }

        public string PackSizeCategoryId { get; set; }
        public int SalesChargesQuantity { get; set; }
        public object BatchMergeDateCalculationMethod
        {
            get { return (int)_batchMergeDateCalculationMethod; }
            set { _batchMergeDateCalculationMethod = DTOUtil.GetEnumFromObj<InventBatchMergeDateCalculationMethod>(value, InventBatchMergeDateCalculationMethod.Manual); }
        }
        public string SalesMultilineDiscountProductGroupCode { get; set; }
        public decimal PurchasePrice { get; set; }
        public string SalesChargeProductGroupId { get; set; }
        public object IsIntercompanyPurchaseUsageBlocked
        {
            get { return (int)_isIntercompanyPurchaseUsageBlocked; }
            set { _isIntercompanyPurchaseUsageBlocked = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }

        public string AlternativeProductConfigurationId { get; set; }
        public int SalesOverdeliveryPercentage { get; set; }
        public object IsDiscountPOSRegistrationProhibited
        {
            get { return (int)_isDiscountPOSRegistrationProhibited; }
            set { _isDiscountPOSRegistrationProhibited = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }

        public int BestBeforePeriodDays { get; set; }
        public int PurchaseOverdeliveryPercentage { get; set; }
        public string PurchaseUnitSymbol { get; set; }
        public int SalesUnderdeliveryPercentage { get; set; }
        public string NecessaryProductionWorkingTimeSchedulingPropertyId { get; set; }
        public string InventoryGSTReliefCategoryCode { get; set; }
        public object ApprovedVendorCheckMethod
        {
            get { return (int)_approvedVendorCheckMethod; }
            set
            {
                _approvedVendorCheckMethod = DTOUtil.GetEnumFromObj<PdsVendorCheckItem>(value, PdsVendorCheckItem.NoCheck);
            }
        }
        public string SalesRebateProductGroupId { get; set; }
        public string InventoryReservationHierarchyName { get; set; }
        public object FlushingPrinciple
        {
            get { return (int)_flushingPrinciple; }
            set
            {
                _flushingPrinciple = DTOUtil.GetEnumFromObj<ProdFlushingPrincipItem>(value, ProdFlushingPrincipItem.Start);
            }
        }
        public int SalesPriceQuantity { get; set; }
        public int YieldPercentage { get; set; }
        public int TareProductWeight { get; set; }
        public int ApproximateSalesTaxPercentage { get; set; }
        public int PackingDutyQuantity { get; set; }
        public string PurchaseLineDiscountProductGroupCode { get; set; }
        public object WillInventoryIssueAutomaticallyReportAsFinished
        {
            get { return (int)_willInventoryIssueAutomaticallyReportAsFinished; }
            set
            {
                _willInventoryIssueAutomaticallyReportAsFinished = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public string ProductFiscalInformationType { get; set; }
        public int PackageHandlingTime { get; set; }
        public Guid DynamicsConnectorIntegrationKey { get; set; }
        public int ShelfLifePeriodDays { get; set; }
        public int TransferOrderUnderdeliveryPercentage { get; set; }
        public int DefaultReceivingQuantity { get; set; }
        public DateTime POSRegistrationBlockedDate { get; set; }
        public object MustKeyInCommentAtPOSRegister
        {
            get { return (int)_mustKeyInCommentAtPOSRegister; }
            set
            {
                _mustKeyInCommentAtPOSRegister = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public int ConstantScrapQuantity { get; set; }
        public object PotencyBaseAttributeValueEntryEvent
        {
            get { return (int)_potencyBaseAttributeValueEntryEvent; }
            set
            {
                _potencyBaseAttributeValueEntryEvent = DTOUtil.GetEnumFromObj<PDSPotencyAttribRecordingEnum>(value, PDSPotencyAttribRecordingEnum.Quality);
            }
        }
        public object KeyInPriceRequirementsAtPOSRegister
        {
            get { return (int)_keyInPriceRequirementsAtPOSRegister; }
            set
            {
                _keyInPriceRequirementsAtPOSRegister = DTOUtil.GetEnumFromObj<RetailPriceKeyingRequirement>(value, RetailPriceKeyingRequirement.NotMandatory);
            }
        }
        public int IntrastatChargePercentage { get; set; }
        public string ProductCoverageGroupId { get; set; }
        public int PotencyBaseAttibuteTargetValue { get; set; }
        public object IsIntercompanySalesUsageBlocked
        {
            get { return (int)_isIntercompanySalesUsageBlocked; }
            set
            {
                _isIntercompanySalesUsageBlocked = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public string PackingMaterialGroupId { get; set; }
        public string PurchaseRebateProductGroupId { get; set; }
        public string OriginCountryRegionId { get; set; }
        public string AlternativeItemNumber { get; set; }
        public int BOMLevel { get; set; }
        public string PurchaseItemWithholdingTaxGroupCode { get; set; }
        public object IsZeroPricePOSRegistrationAllowed
        {
            get { return (int)_isZeroPricePOSRegistrationAllowed; }
            set
            {
                _isZeroPricePOSRegistrationAllowed = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public int CostChargesQuantity { get; set; }
        public object IsUnitCostAutomaticallyUpdated
        {
            get { return (int)_isUnitCostAutomaticallyUpdated; }
            set
            {
                _isUnitCostAutomaticallyUpdated = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public string DefaultDirectDeliveryWarehouse { get; set; }
        public object ProductTaxationOrigin
        {
            get { return (int)_productTaxationOrigin; }
            set
            {
                _productTaxationOrigin = DTOUtil.GetEnumFromObj<FITaxationOrigin_BR>(value, FITaxationOrigin_BR.National);
            }
        }
        public object IsVariantShelfLabelsPrintingEnabled
        {
            get { return (int)_isVariantShelfLabelsPrintingEnabled; }
            set
            {
                _isVariantShelfLabelsPrintingEnabled = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public decimal UnitCost { get; set; }
        public DateTime SalesPriceDate { get; set; }
        public object AlternativeProductUsageCondition
        {
            get { return (int)_alternativeProductUsageCondition; }
            set
            {
                _alternativeProductUsageCondition = DTOUtil.GetEnumFromObj<ItemNumAlternative>(value, ItemNumAlternative.Never);
            }
        }
        public object WillTotalSalesDiscountCalculationIncludeProduct
        {
            get { return (int)_willTotalSalesDiscountCalculationIncludeProduct; }
            set
            {
                _willTotalSalesDiscountCalculationIncludeProduct = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public object IsInstallmentEligible
        {
            get { return (int)_isInstallmentEligible; }
            set
            {
                _isInstallmentEligible = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public string PurchasePriceToleranceGroupId { get; set; }
        public object BaseSalesPriceSource
        {
            get { return (int)_baseSalesPriceSource; }
            set
            {
                _baseSalesPriceSource = DTOUtil.GetEnumFromObj<SalesPriceModelBasic>(value, SalesPriceModelBasic.PurchPrice);
            }
        }
        public string SerialNumberGroupCode { get; set; }
        public DateTime ProductLifeCycleValidFromDate { get; set; }
        public string ItemFiscalClassificationExceptionCode { get; set; }
        public int NGPCode { get; set; }
        public string SalesUnitSymbol { get; set; }
        public string ProductionGroupId { get; set; }
        public object KeyInQuantityRequirementsAtPOSRegister
        {
            get { return (int)_keyInQuantityRequirementsAtPOSRegister; }
            set
            {
                _keyInQuantityRequirementsAtPOSRegister = DTOUtil.GetEnumFromObj<RetailQtyKeyingRequirement>(value, RetailQtyKeyingRequirement.NotMandatory);
            }
        }
        public object DefaultOrderType
        {
            get { return (int)_defaultOrderType; }
            set { _defaultOrderType = DTOUtil.GetEnumFromObj<ReqPOType>(value, ReqPOType.Purch); }
        }
        public int ProductionConsumptionHeightConversionFactor { get; set; }
        public int ContinuityEventDuration { get; set; }
        public object IsPOSRegistrationBlocked
        {
            get { return (int)_isPOSRegistrationBlocked; }
            set
            {
                _isPOSRegistrationBlocked = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public string BatchNumberGroupCode { get; set; }
        public string PotencyBaseAttributeId { get; set; }
        public int PurchaseUnderdeliveryPercentage { get; set; }
        public string PackageClassId { get; set; }
        public string PurchaseGSTReliefCategoryCode { get; set; }
        public int SalesPriceCalculationChargesPercentage { get; set; }
        public string PurchaseMultilineDiscountProductGroupCode { get; set; }
        public object WillWorkCenterPickingAllowNegativeInventory
        {
            get { return (int)_willWorkCenterPickingAllowNegativeInventory; }
            set
            {
                _willWorkCenterPickingAllowNegativeInventory = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public string ProductLifeCycleSeasonCode { get; set; }
        public string SalesSalesTaxItemGroupCode { get; set; }
        public object IsRestrictedForCoupons
        {
            get { return (int)_isRestrictedForCoupons; }
            set
            {
                _isRestrictedForCoupons = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public object IsSalesPriceAdjustmentAllowed
        {
            get { return (int)_isSalesPriceAdjustmentAllowed; }
            set { _isSalesPriceAdjustmentAllowed = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }

        public object IsPurchasePriceAutomaticallyUpdated
        {
            get { return (int)_isPurchasePriceAutomaticallyUpdated; }
            set
            {
                _isPurchasePriceAutomaticallyUpdated = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public int MinimumCatchWeightQuantity { get; set; }
        public object WillInventoryReceiptIgnoreFlushingPrinciple
        {
            get { return (int)_willInventoryReceiptIgnoreFlushingPrinciple; }
            set
            {
                _willInventoryReceiptIgnoreFlushingPrinciple = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }

        public string ProductNumber { get; set; }
        public int UnitCostQuantity { get; set; }
        public string FreightAllocationGroupId { get; set; }
        public string ComparisonPriceBaseUnitSymbol { get; set; }
        public string CostCalculationGroupId { get; set; }
        public int ShelfAdvicePeriodDays { get; set; }
        public string OriginStateId { get; set; }
        public int qtyPerLayer { get; set; }

    }
}