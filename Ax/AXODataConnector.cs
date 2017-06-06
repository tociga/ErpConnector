using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ErpConnector.Ax.Modules;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.DTO;
using System.Diagnostics;
using System.Configuration;

namespace ErpConnector.Ax
{
    public class AXODataConnector
    {
        string header = "";
        private Resources context;
        public AXODataConnector()
        {            
            context = new Resources(new Uri("https://agrax7u2devaos.cloudax.dynamics.com/data"));
            context.SendingRequest2 += Context_SendingRequest2;                      
        }

        public static string GetDBScript(string entity)
        {
            return ScriptGeneratorModule.GenerateScript(entity);
        }
        public ItemDTO CreateItem(ItemDTO item)
        {
            header = AdalAuthenticate();
            return ItemTransfer.CreateItem(item, header);
        }

        public void GetBom()
        {
            header = AdalAuthenticate();
            DataWriter.TruncateTables(false, false, false, false, false, true);
            BomTransfer.GetBom(header);
        }

        public void GetPoTo()
        {
            header = AdalAuthenticate();
            POTransfer.GetPosAndTos(header, context);
        }
        public void RunTransfer()
        {
            header = AdalAuthenticate();
            var start = DateTime.Now;
            DataWriter.TruncateTables(true, false, false,true, true, false);
            var truncate = DateTime.Now;
            Debug.WriteLine("Truncate = " + truncate.Subtract(start).TotalSeconds);
            ItemCategoryTransfer.WriteCategories(header);
            DateTime cat = DateTime.Now;
            Debug.WriteLine("Category = " + cat.Subtract(truncate).TotalSeconds);
            LocationsAndVendorsTransfer.WriteLocationsAndVendors(context, header);
            DateTime loc = DateTime.Now;
            Debug.WriteLine("Locations = " + loc.Subtract(cat).TotalSeconds);
            ItemTransfer.WriteItems(context, header);
            DateTime items = DateTime.Now;
            Debug.WriteLine("Items = " + items.Subtract(loc).TotalSeconds);
            ItemAttributeLookup.ReadItemAttributes(context, header);
            DateTime lookup = DateTime.Now;
            Debug.WriteLine("Lookup = " + lookup.Subtract(items).TotalSeconds);
        }

        /// <summary>
        ///
        /// </summary>
        public void CreateItemTest()
        {
            var rand = new Random();
            var dp = new DistinctProduct();
            dp.NMFCCode = "";
            dp.ProductType = EcoResProductType.Item;
            dp.STCCCode = "";
            dp.StorageDimensionGroupName = "";
            dp.ProductNumber = "AGRPOC11";
            dp.IsCatchWeightProduct = NoYes.No;
            dp.ProductDescription = "";
            dp.RetailProductCategoryName = "";
            dp.TrackingDimensionGroupName = "None";
            dp.ProductSearchName = "AGRProf11";
            dp.ProductName = dp.ProductSearchName;
            dp.HarmonizedSystemCode = "";

            #region Released Distinct Product
            ReleasedDistinctProductsWriteDTO rdp = new ReleasedDistinctProductsWriteDTO();
            rdp.AlternativeItemNumber = "";
            rdp.AlternativeProductColorId = "";
            rdp.AlternativeProductConfigurationId = "";
            rdp.AlternativeProductSizeId = "";
            rdp.AlternativeProductStyleId = "";
            rdp.AlternativeProductUsageCondition = ItemNumAlternative.Never;
            rdp.ApprovedVendorCheckMethod = PdsVendorCheckItem.NoCheck;
            rdp.ApproximateSalesTaxPercentage = 0m;
            rdp.AreTransportationManagementProcessesEnabled = NoYes.No;
            rdp.ArrivalHandlingTime = 0;
            rdp.BarcodeSetupId = "";
            rdp.BaseSalesPriceSource = SalesPriceModelBasic.PurchPrice;
            rdp.BatchMergeDateCalculationMethod = InventBatchMergeDateCalculationMethod.Manual;
            rdp.BatchNumberGroupCode = "";
            rdp.BestBeforePeriodDays = 0;
            rdp.BOMUnitSymbol = "";
            rdp.BuyerGroupId = "";
            rdp.CarryingCostABCCode = ABC.None;
            rdp.CatchWeightUnitSymbol = "";
            rdp.CommissionProductGroupId = "";
            rdp.ComparisonPriceBaseUnitSymbol = "";
            rdp.ConstantScrapQuantity = 0m;
            rdp.ContinuityEventDuration = 0;
            rdp.ContinuityScheduleId = "";
            rdp.CostCalculationGroupId = "";
            rdp.CostChargesQuantity = 0m;
            rdp.CostGroupId = "";
            //rdp.DataAreaId = "usrt";
            rdp.DefaultDirectDeliveryWarehouse = "";
            rdp.DefaultLedgerDimensionDisplayValue = "006--";
            rdp.DefaultOrderType = ReqPOType.Purch;
            rdp.DefaultReceivingQuantity = 0m;
            rdp.FixedCostCharges = 0m;
            rdp.FixedPurchasePriceCharges = 0m;
            rdp.FixedSalesPriceCharges = 0m;
            rdp.FlushingPrinciple = ProdFlushingPrincipItem.Start;
            rdp.FreightAllocationGroupId = "";
            rdp.GrossDepth = 0m;
            rdp.GrossProductHeight = 0m;
            rdp.GrossProductWidth = 0m;
            rdp.IntrastatChargePercentage = 0m;
            rdp.IntrastatCommodityCode = "";
            //rdp.InventoryGSTReliefCategoryCode = "";
            rdp.InventoryReservationHierarchyName = "";
            rdp.InventoryUnitSymbol = "EA";
            rdp.IsDeliveredDirectly = NoYes.No;
            rdp.IsDiscountPOSRegistrationProhibited = NoYes.No;
            rdp.IsExemptFromAutomaticNotificationAndCancellation = NoYes.No;
            rdp.IsICMSTaxAppliedOnService = NoYes.No;
            rdp.IsInstallmentEligible = NoYes.No;
            rdp.IsIntercompanyPurchaseUsageBlocked = NoYes.No;
            rdp.IsIntercompanySalesUsageBlocked = NoYes.No;
            rdp.IsPhantom = NoYes.No;
            rdp.IsPOSRegistrationBlocked = NoYes.No;
            rdp.IsPOSRegistrationQuantityNegative = NoYes.No;
            rdp.IsPurchasePriceAutomaticallyUpdated = NoYes.No;
            rdp.IsPurchasePriceIncludingCharges = NoYes.No;
            rdp.IsPurchaseWithholdingTaxCalculated = NoYes.No;
            rdp.IsRestrictedForCoupons = NoYes.No;
            rdp.IsSalesPriceAdjustmentAllowed = NoYes.No;
            rdp.IsSalesPriceIncludingCharges = NoYes.No;
            rdp.IsSalesWithholdingTaxCalculated = NoYes.No;
            rdp.IsScaleProduct = NoYes.No;
            rdp.IsShipAloneEnabled = NoYes.No;
            rdp.IsUnitCostAutomaticallyUpdated = NoYes.No;
            rdp.IsUnitCostIncludingCharges = NoYes.No;
            rdp.IsVariantShelfLabelsPrintingEnabled = NoYes.No;
            rdp.IsZeroPricePOSRegistrationAllowed = NoYes.No;
            rdp.ItemFiscalClassificationCode = "";
            rdp.ItemFiscalClassificationExceptionCode = "";
            rdp.ItemModelGroupId = "MOV_AVG";
            rdp.ItemNumber = dp.ProductNumber;
            rdp.KeyInPriceRequirementsAtPOSRegister = RetailPriceKeyingRequirement.NotMandatory;
            rdp.KeyInQuantityRequirementsAtPOSRegister = RetailQtyKeyingRequirement.NotMandatory;
            rdp.MarginABCCode = ABC.None;
            rdp.MaximumCatchWeightQuantity = 0m;
            rdp.MaximumPickQuantity = 0m;
            rdp.MinimumCatchWeightQuantity = 0m;
            rdp.MustKeyInCommentAtPOSRegister = NoYes.No;
            rdp.NecessaryProductionWorkingTimeSchedulingPropertyId = "";
            rdp.NetProductWeight = 0m;
            rdp.NGPCode = 0;
            rdp.OriginCountryRegionId = "";
            rdp.OriginStateId = "";
            rdp.PackageClassId = "";
            rdp.PackageHandlingTime = 0;
            rdp.PackingDutyQuantity = 0m;
            rdp.PackingMaterialGroupId = "";
            rdp.PackSizeCategoryId = "";
            rdp.PhysicalDimensionGroupId = "";
            rdp.PlanningFormulaItemNumber = "";
            rdp.POSRegistrationActivationDate = new DateTimeOffset(new DateTime(1900,1,1,0,0,0));
            rdp.POSRegistrationBlockedDate = new DateTimeOffset(new DateTime(1900, 1, 1, 0, 0, 0));
            rdp.POSRegistrationPlannedBlockedDate = new DateTimeOffset(new DateTime(1900, 1, 1, 0, 0, 0));
            rdp.PotencyBaseAttibuteTargetValue = 0m;
            rdp.PotencyBaseAttributeId = "";
            rdp.PotencyBaseAttributeValueEntryEvent = PDSPotencyAttribRecordingEnum.PurchProdReceipt;
            rdp.PrimaryVendorAccountNumber = "1004";
            rdp.ProductCoverageGroupId = "";
            rdp.ProductFiscalInformationType = "";
            rdp.ProductGroupId = "ActionSpor";
            rdp.ProductionConsumptionDensityConversionFactor = 0m;
            rdp.ProductionConsumptionDepthConversionFactor = 0m;
            rdp.ProductionConsumptionHeightConversionFactor = 0m;
            rdp.ProductionConsumptionWidthConversionFactor = 0m;
            rdp.ProductionGroupId = "";
            rdp.ProductionPoolId = "";
            rdp.ProductionType = PmfProductType.None;
            rdp.ProductLifeCycleSeasonCode = "";
            rdp.ProductLifeCycleValidFromDate = new DateTimeOffset(new DateTime(1900, 1, 1, 0, 0, 0));
            rdp.ProductLifeCycleValidToDate = new DateTimeOffset(new DateTime(1900, 1, 1, 0, 0, 0));
            rdp.ProductNumber = dp.ProductNumber;
            rdp.ProductTaxationOrigin = FITaxationOrigin_BR.National;
            rdp.ProductVolume = 0m;
            rdp.ProjectCategoryId = "";
            rdp.PurchaseChargeProductGroupId = "";
            rdp.PurchaseChargesQuantity = 0m;
            //rdp.PurchaseGSTReliefCategoryCode = "";
            rdp.PurchaseItemWithholdingTaxGroupCode = "";
            rdp.PurchaseLineDiscountProductGroupCode = "";
            rdp.PurchaseMultilineDiscountProductGroupCode = "";
            rdp.PurchaseOverdeliveryPercentage = 0m;
            rdp.PurchasePrice = 12.44m;
            rdp.PurchasePriceDate = new DateTimeOffset(DateTime.Now);
            rdp.PurchasePriceQuantity = 1m;
            rdp.PurchasePriceToleranceGroupId = "";
            rdp.PurchasePricingPrecision = 0;
            rdp.PurchaseRebateProductGroupId = "";
            rdp.PurchaseSalesTaxItemGroupCode = "RP";
            rdp.PurchaseSupplementaryProductProductGroupId = "";
            rdp.PurchaseUnderdeliveryPercentage = 0m;
            rdp.PurchaseUnitSymbol = "EA";
            rdp.RawMaterialPickingPrinciple = WHSAllowMaterialOverPick.Staging;
            rdp.RevenueABCCode = ABC.None;
            rdp.SalesChargeProductGroupId = "";
            rdp.SalesChargesQuantity = 0m;
            //rdp.SalesGSTReliefCategoryCode = null; // cannot insert for the given value
            rdp.SalesItemWithholdingTaxGroupCode = "";
            rdp.SalesLineDiscountProductGroupCode = "";
            rdp.SalesMultilineDiscountProductGroupCode = "";
            rdp.SalesOverdeliveryPercentage = 0m;
            rdp.SalesPrice = 24m;
            rdp.SalesPriceCalculationChargesPercentage = 0m;
            rdp.SalesPriceCalculationContributionRatio = 0m;
            rdp.SalesPriceCalculationModel = SalesPriceModel.PercentMarkup;
            rdp.SalesPriceDate = new DateTimeOffset(DateTime.Now);
            rdp.SalesPriceQuantity = 1m;
            rdp.SalesPricingPrecision = 0;
            rdp.SalesRebateProductGroupId = "";
            rdp.SalesSalesTaxItemGroupCode = "";
            rdp.SalesSupplementaryProductProductGroupId = "";
            rdp.SalesUnderdeliveryPercentage = 0m;
            rdp.SalesUnitSymbol = "Ea";
            rdp.SearchName = dp.ProductName;
            rdp.SellEndDate = new DateTimeOffset(DateTime.Now.AddMonths(12));
            rdp.SellStartDate = new DateTimeOffset(DateTime.Now.AddMonths(-12));
            rdp.SerialNumberGroupCode = "";
            rdp.ServiceFiscalInformationCode = "";
            rdp.ShelfAdvicePeriodDays = 0;
            rdp.ShelfLifePeriodDays = 0;
            rdp.ShippingAndReceivingSortOrderCode = 0;
            rdp.ShipStartDate = new DateTimeOffset(DateTime.Now);
            rdp.StorageDimensionGroupName = "SiteWH";
            rdp.TareProductWeight = 0m;
            rdp.TrackingDimensionGroupName = "None";
            rdp.TransferOrderOverdeliveryPercentage = 0m;
            rdp.TransferOrderUnderdeliveryPercentage = 0m;
            rdp.UnitConversionSequenceGroupId = "";
            rdp.UnitCost = 11m;
            rdp.UnitCostDate = new DateTimeOffset(DateTime.Now);
            rdp.UnitCostQuantity = 1;
            rdp.ValueABCCode = ABC.None;
            rdp.VariableScrapPercentage = 0m;
            rdp.VendorInvoiceLineMatchingPolicy = PurchMatchingPolicyWithNotSetOption.NotSet;
            rdp.WarehouseMobileDeviceDescriptionLine1 = "";
            rdp.WarehouseMobileDeviceDescriptionLine2 = "";
            rdp.WillInventoryIssueAutomaticallyReportAsFinished = NoYes.No;
            rdp.WillInventoryReceiptIgnoreFlushingPrinciple = NoYes.No;
            rdp.WillPickingWorkbenchApplyBoxingLogic = NoYes.No;
            rdp.WillTotalPurchaseDiscountCalculationIncludeProduct = NoYes.No;
            rdp.WillTotalSalesDiscountCalculationIncludeProduct = NoYes.No;
            rdp.WillWorkCenterPickingAllowNegativeInventory = NoYes.No;
            rdp.YieldPercentage = 0m;
            #endregion
            //ProductColorGroup
            var master = new ProductMasterWriteDTO();
            master.AreIdenticalConfigurationsAllowed = NoYes.No;
            master.HarmonizedSystemCode = "";
            master.IsAutomaticVariantGenerationEnabled = NoYes.Yes;
            master.IsCatchWeightProduct = NoYes.No;
            master.IsProductKit = NoYes.No;
            master.IsProductVariantUnitConversionEnabled = NoYes.No;
            //master.KPMInstructionGroupId = "";
            //master.KRFColorRatioCurve = "";
            //master.KRFSizeRatioCurve = "";
            //master.KRFStyleRatioCurve = "";
            //master.KRFUseRatioCurves = NoYes.Yes;
            master.NMFCCode = "";
            master.ProductColorGroupId = "Basic";
            master.ProductDescription = "";
            //master.ProductDimensionGroupName = "CSF";
            master.ProductDimensionGroupName = "SizeCol";
            master.ProductName = dp.ProductName;
            master.ProductNumber = dp.ProductNumber;
            master.ProductSearchName = dp.ProductSearchName;
            master.ProductSizeGroupId = "10-18";
            master.ProductStyleGroupId = "";
            master.VariantConfigurationTechnology = EcoResVariantConfigurationTechnologyType.PredefinedVariants;
            master.RetailProductCategoryName = "";
            master.ProductType = EcoResProductType.Item;
            master.STCCCode = "";
            master.TrackingDimensionGroupName = "None";
            master.StorageDimensionGroupName = "Ware";


            var rpm = new ReleasedProductMasterWriteDTO();
            rpm.TransferOrderOverdeliveryPercentage =  0;
            rpm.SalesUnitSymbol =  "Ea";
            rpm.ProductionConsumptionWidthConversionFactor =  0;
            //rpm.BOMLevel =  0;
            rpm.IsPurchasePriceAutomaticallyUpdated =  NoYes.No;
            rpm.IsPurchaseWithholdingTaxCalculated =  NoYes.No;
            rpm.TransferOrderUnderdeliveryPercentage =  0;
            rpm.IsDeliveredDirectly =  NoYes.No;
            rpm.SalesSupplementaryProductProductGroupId =  "";
            rpm.SalesMultilineDiscountProductGroupCode =  "";
            rpm.WillTotalPurchaseDiscountCalculationIncludeProduct =  NoYes.Yes;
            rpm.IsVariantShelfLabelsPrintingEnabled =  NoYes.No;
            rpm.ProductionGroupId =  "";
            rpm.OriginStateId =  "";
            rpm.RevenueABCCode =  ABC.None;
            rpm.NecessaryProductionWorkingTimeSchedulingPropertyId =  "";
            rpm.IntrastatCommodityCode =  "";
            rpm.SalesRebateProductGroupId =  "";
            rpm.UnitCostDate = new DateTimeOffset(new DateTime(2012, 10, 6, 12, 0, 0, 0));
            rpm.DefaultProductSizeId =  "";
            rpm.AlternativeProductStyleId =  "";
            rpm.PotencyBaseAttributeId =  "";
            rpm.AlternativeProductUsageCondition =  ItemNumAlternative.Never;
            rpm.PurchaseUnderdeliveryPercentage =  0;
            rpm.DefaultProductStyleId =  "";
            rpm.AlternativeProductSizeId =  "";
            rpm.WillInventoryIssueAutomaticallyReportAsFinished =  NoYes.No;
            rpm.ProductVolume =  0;
            rpm.TareProductWeight =  0;
            rpm.ItemNumber = master.ProductNumber;
            rpm.IsPhantom =  NoYes.No;
            rpm.DefaultProductConfigurationId =  "";
            rpm.FlushingPrinciple = ProdFlushingPrincipItem.Start;
            rpm.VariableScrapPercentage =  0;
            rpm.ArrivalHandlingTime =  0;
            rpm.SearchName = master.ProductSearchName;
            rpm.IsUnitCostIncludingCharges =  NoYes.No;
            rpm.AlternativeProductColorId =  "";
            rpm.PhysicalDimensionGroupId =  "";
            rpm.MinimumCatchWeightQuantity =  0;
            rpm.SalesChargeProductGroupId =  "";
            //rpm.ProductSearchName =  master.ProductSearchName;
            rpm.WillPickingWorkbenchApplyBoxingLogic =  NoYes.No;
            rpm.ItemFiscalClassificationExceptionCode =  "";
            rpm.InventoryReservationHierarchyName =  "";
            rpm.IsZeroPricePOSRegistrationAllowed =  NoYes.No;
            rpm.IntrastatChargePercentage =  0;
            rpm.IsScaleProduct = NoYes.No;
            rpm.PlanningFormulaItemNumber = "";
            rpm.ProductFiscalInformationType = "";
            rpm.StorageDimensionGroupName = "Ware";
            rpm.ShelfAdvicePeriodDays = 0;
            rpm.ContinuityScheduleId = "";
            rpm.VendorInvoiceLineMatchingPolicy = PurchMatchingPolicyWithNotSetOption.NotSet;
            //rpm.InventoryGSTReliefCategoryCode = "";
            rpm.MustKeyInCommentAtPOSRegister = NoYes.No;
            rpm.SalesPriceQuantity = 1;
            rpm.ServiceFiscalInformationCode = "";
            rpm.BarcodeSetupId = "";
            rpm.IsUnitCostAutomaticallyUpdated = NoYes.No;
            rpm.PotencyBaseAttibuteTargetValue = 0;
            rpm.DefaultReceivingQuantity = 0;
            rpm.ProductionConsumptionDensityConversionFactor = 0;
            rpm.PurchaseItemWithholdingTaxGroupCode = "";
            rpm.PurchasePricingPrecision = 0;
            rpm.UnitCostQuantity = 1;
            rpm.IsRestrictedForCoupons = NoYes.No;
            rpm.SellStartDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0));
            rpm.SellEndDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0));
            rpm.ConstantScrapQuantity = 0;
            rpm.BatchNumberGroupCode = "";
            rpm.CostCalculationGroupId = "";
            rpm.PackingDutyQuantity = 0;
            rpm.AlternativeProductConfigurationId = "";
            rpm.SalesPrice = 19.99m;
            rpm.DefaultProductColorId = "";
            rpm.IsSalesPriceIncludingCharges = NoYes.No;
            rpm.ProductionType = PmfProductType.None;
            rpm.WillTotalSalesDiscountCalculationIncludeProduct = NoYes.Yes;
            rpm.PotencyBaseAttributeValueEntryEvent = PDSPotencyAttribRecordingEnum.PurchProdReceipt;
            rpm.ItemModelGroupId = "MOV_AVG";
            rpm.PurchaseMultilineDiscountProductGroupCode = "";
            //rpm.DynamicsConnectorIntegrationKey = Guid.Empty;
            rpm.SalesChargesQuantity = 0;
            rpm.SalesPriceCalculationContributionRatio = 0;
            //rpm.ProductGroupId = "Apparel";
            rpm.IsSalesPriceAdjustmentAllowed = NoYes.No;
            rpm.SalesPriceCalculationChargesPercentage = 0;
            rpm.ShippingAndReceivingSortOrderCode = 0;
            rpm.GrossProductHeight = 0;
            rpm.ProductLifeCycleValidFromDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0));
            //rpm.PurchaseGSTReliefCategoryCode = "";
            rpm.POSRegistrationPlannedBlockedDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0));
            rpm.SalesItemWithholdingTaxGroupCode = "";
            rpm.PurchaseLineDiscountProductGroupCode = "";
            rpm.PurchasePriceDate = new DateTimeOffset(new DateTime(2012, 10, 6, 12, 0, 0, 0));
            rpm.IsIntercompanyPurchaseUsageBlocked = NoYes.No;
            rpm.NGPCode = 0;
            rpm.ShelfLifePeriodDays = 0;
            rpm.UnitConversionSequenceGroupId = "";
            rpm.ProductionConsumptionHeightConversionFactor = 0;
            rpm.BOMUnitSymbol = "";
            rpm.MaximumCatchWeightQuantity = 0;
            rpm.PurchasePriceQuantity = 1;
            rpm.PurchasePrice = 11.994m;
            rpm.ProductLifeCycleSeasonCode = "";
            rpm.IsDiscountPOSRegistrationProhibited = NoYes.No;
            rpm.ProductLifeCycleValidToDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0));
            rpm.PurchaseOverdeliveryPercentage = 0;
            rpm.TrackingDimensionGroupName = "None";
            rpm.FixedSalesPriceCharges = 0;
            rpm.ProductTaxationOrigin = FITaxationOrigin_BR.National;
            rpm.ProductionPoolId = "";
            rpm.ValueABCCode = ABC.None;
            rpm.PurchaseUnitSymbol = "Ea";
            rpm.PurchaseSupplementaryProductProductGroupId = "";
            rpm.AlternativeItemNumber = "";
            //rpm.SalesSalesTaxItemGroupCode = "RP";
            rpm.ProductCoverageGroupId = "";
            rpm.CostGroupId = "";
            rpm.IsPurchasePriceIncludingCharges = NoYes.No;
            rpm.IsShipAloneEnabled = NoYes.No;
            rpm.ProductNumber = master.ProductNumber;
            rpm.RawMaterialPickingPrinciple = WHSAllowMaterialOverPick.Staging;
            rpm.FixedPurchasePriceCharges = 0;
            rpm.FreightAllocationGroupId = "";
            rpm.ContinuityEventDuration = 0;
            //rpm.PurchaseSalesTaxItemGroupCode = "RP";
            rpm.DefaultDirectDeliveryWarehouse = "";
            //rpm.SalesGSTReliefCategoryCode = "";
            rpm.SalesPriceDate = new DateTimeOffset(new DateTime(2012, 10, 6, 12, 0, 0, 0));
            rpm.OriginCountryRegionId = "";
            rpm.DefaultOrderType = ReqPOType.Purch;
            rpm.IsPOSRegistrationQuantityNegative = NoYes.No;
            rpm.PurchaseChargesQuantity = 0;
            //rpm.PrimaryVendorAccountNumber = "1004";
            rpm.MaximumPickQuantity = 0;
            rpm.SalesUnderdeliveryPercentage = 0;
            rpm.IsInstallmentEligible = NoYes.No;
            rpm.KeyInQuantityRequirementsAtPOSRegister = RetailQtyKeyingRequirement.NotMandatory;
            rpm.CommissionProductGroupId = "";
            rpm.IsIntercompanySalesUsageBlocked = NoYes.No;
            rpm.YieldPercentage = 0;
            rpm.BaseSalesPriceSource = SalesPriceModelBasic.PurchPrice;
            rpm.IsSalesWithholdingTaxCalculated = NoYes.No;
            rpm.ApprovedVendorCheckMethod = PdsVendorCheckItem.NoCheck;
            rpm.BestBeforePeriodDays = 0;
            rpm.GrossDepth = 0;
            rpm.PurchaseRebateProductGroupId = "";
            rpm.PackSizeCategoryId = "";
            rpm.PackageClassId = "";
            rpm.FixedCostCharges = 0;
            rpm.UnitCost = 11.994m;
            rpm.SerialNumberGroupCode = "";
            rpm.CarryingCostABCCode = ABC.None;
            rpm.SalesLineDiscountProductGroupCode = "";
            rpm.IsPOSRegistrationBlocked = NoYes.No;
            rpm.POSRegistrationBlockedDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0)); ;
            rpm.ProjectCategoryId = "";
            rpm.PurchasePriceToleranceGroupId = "";
            rpm.AreTransportationManagementProcessesEnabled = NoYes.Yes;
            rpm.IsExemptFromAutomaticNotificationAndCancellation = NoYes.No;
            rpm.PackingMaterialGroupId = "";
            rpm.InventoryUnitSymbol = "Ea";
            rpm.ComparisonPriceBaseUnitSymbol = "";
            rpm.WillWorkCenterPickingAllowNegativeInventory = NoYes.No;
            rpm.IsICMSTaxAppliedOnService = NoYes.No;
            //rpm.ProductType = EcoResProductType.Item;
            rpm.KeyInPriceRequirementsAtPOSRegister = RetailPriceKeyingRequirement.NotMandatory;
            rpm.ApproximateSalesTaxPercentage = 0;
            rpm.POSRegistrationActivationDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0)); ;
            rpm.WillInventoryReceiptIgnoreFlushingPrinciple = NoYes.No;
            rpm.NetProductWeight = 0;
            rpm.CostChargesQuantity = 0;
            rpm.BatchMergeDateCalculationMethod = InventBatchMergeDateCalculationMethod.Manual;
            rpm.SalesPriceCalculationModel = SalesPriceModel.None;
            rpm.PurchaseChargeProductGroupId = "";
            rpm.SalesOverdeliveryPercentage = 0;
            rpm.DefaultLedgerDimensionDisplayValue = "006--";
            rpm.SalesPricingPrecision = 0;
            rpm.MarginABCCode = ABC.None;
            rpm.CatchWeightUnitSymbol = "";
            rpm.WarehouseMobileDeviceDescriptionLine1 = "";
            rpm.WarehouseMobileDeviceDescriptionLine2 = "";
            rpm.ProductionConsumptionDepthConversionFactor = 0;
            rpm.ItemFiscalClassificationCode = "";
            rpm.PackageHandlingTime = 0;
            rpm.IsUnitCostProductVariantSpecific = NoYes.No;
            rpm.BuyerGroupId = "";
            rpm.GrossProductWidth = 0;
            rpm.ShipStartDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0));

            //var r = AXServiceConnector.CallOdataEndpointPost("ProductMasters", null, header, master).Result;
            var rpmr = AXServiceConnector.CallOdataEndpointPost("ReleasedProductMasters", null, header, rpm);
            //ReleasedProductVariantDTO v = new ReleasedProductVariantDTO();
            ////v.DataAreaId = "usrt";
            //v.ItemNumber = "AGRPOC_176025568";
            //v.ProductColorId = "Black";
            //v.ProductSizeId = "Large";
            //v.ProductConfigurationId = "";
            //v.ProductStyleId = "";
            //v.ProductMasterNumber = "AGRPOC_176025568";

            var v = new ReleasedProductVariantDTO();
            //v.DataAreaId = "usrt";
            v.ItemNumber = rpm.ItemNumber;
            v.ProductColorId = "Black";
            v.ProductSizeId = "10";
            v.ProductConfigurationId = "";
            v.ProductStyleId = "";
            v.ProductName = master.ProductName + "_" + v.ProductColorId + "_" + v.ProductSizeId;
            v.ProductSearchName = v.ProductName;
            v.ProductMasterNumber = master.ProductNumber;


            var pt = new ProductTranslation { LanguageId = "en-us", ProductNumber = master.ProductNumber, ProductName = master.ProductName };
            var list = new List<ProductTranslation>();
            list.Add(pt);
            var vv = new ReleasedProductVariant();


            var vars = new List<ReleasedProductVariantDTO>();
            vars.Add(v);

            var rv = AXServiceConnector.CallOdataEndpointPost("ReleasedProductVariants", null, header, v).Result;

        }
        private void Context_SendingRequest2(object sender, global::Microsoft.OData.Client.SendingRequest2EventArgs e)
        {
            e.RequestMessage.SetHeader("Authorization", AdalAuthenticate());
        }

        private static string AdalAuthenticate()
        {
            var uri = new UriBuilder("https://login.windows.net/reynd.is/oauth2/token");
            var redirectUri = new UriBuilder("http://agrdynamics.com/agr5ax7");

            var authenticationContext = new AuthenticationContext(uri.ToString());
            var cred = new ClientCredential("4d2a3c5d-7e63-40a8-9c37-c8769b1c5af3", "px8O9/yP1alySqXxYBtHgKo2LdRlBYBJCr1mio/Quns=");

            var authResult = authenticationContext.AcquireTokenAsync(ConfigurationManager.AppSettings["ax_base_url"], cred).Result;

            return authResult.CreateAuthorizationHeader();
        }

        public static AuthenticationResult GetAdalToken()
        {
            var uri = new UriBuilder("https://login.windows.net/reynd.is/oauth2/token");
            var redirectUri = new UriBuilder("http://agrdynamics.com/agr5ax7");

            var authenticationContext = new AuthenticationContext(uri.ToString());
            var cred = new ClientCredential("4d2a3c5d-7e63-40a8-9c37-c8769b1c5af3", "px8O9/yP1alySqXxYBtHgKo2LdRlBYBJCr1mio/Quns=");

            var authResult = authenticationContext.AcquireTokenAsync(System.Configuration.ConfigurationManager.AppSettings["ax_base_url"], cred).Result;

            return authResult;
        }

    }
}
