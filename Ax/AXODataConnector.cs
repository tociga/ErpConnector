using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using ErpConnector.Ax.Modules;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.DTO;
using System.Diagnostics;
using System.Configuration;
using ErpConnector.Common;
using ErpConnector.Ax.Utils;
using System.Collections.Generic;
using ErpConnector.Ax.Authentication;
using System.Collections;
using ErpConnector.Common.AGREntities;
using System.Linq;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common.ErpTasks;
using System.Reflection;
using ErpConnector.Ax.DB;

namespace ErpConnector.Ax
{
    public class AxODataConnector : IErpConnector
    {
        #region Initialization
        private Resources _context;
        private Resources Context
        {
            get
            {
                if (_context == null)
                {
                    var axBaseUrl = ConfigurationManager.AppSettings["ax_base_url"];
                    _context = new Resources(new Uri(axBaseUrl + "/data"));
                    _context.SendingRequest2 += Context_SendingRequest2;
                    return _context;
                }
                return _context;
            }
        }
        private bool includesFashion;
        private bool includeB_M;
        public AxODataConnector()
        {
			Boolean.TryParse(ConfigurationManager.AppSettings["includesFashion"], out includesFashion);
            Boolean.TryParse(ConfigurationManager.AppSettings["includeBAndM"], out includeB_M);
        }



        private void Context_SendingRequest2(object sender, global::Microsoft.OData.Client.SendingRequest2EventArgs e)
        {
            e.RequestMessage.SetHeader("Authorization", Authenticator.GetAdalHeader());
        }
        #endregion

        #region Public Interface
        public string GetDBScript(string entity)
        {
            return ScriptGeneratorModule.GenerateScript(entity);
        }
        private AxBaseException ExecuteTask(int actionId, ErpTaskStep erpStep, DateTime date)
        {
            if (erpStep.TaskType == ErpTaskStep.ErpTaskType.ODATA_ENDPOINT)
            {
                MethodInfo method = typeof(ServiceConnector).GetMethod("CallOdataEndpoint");
                MethodInfo generic = method.MakeGenericMethod(erpStep.ReturnType);

                Object[] parameters = new Object[4];
                if (erpStep.MaxPageSize.HasValue)
                {
                    parameters = new object[] { erpStep.EndPoint, erpStep.MaxPageSize.Value, erpStep.DbTable, actionId };
                }
                else
                {
                    parameters = new object[] { erpStep.EndPoint, erpStep.EndpointFilter, erpStep.DbTable, actionId };
                }
                generic.Invoke(null, parameters);
            }
            else if (erpStep.TaskType == ErpTaskStep.ErpTaskType.CUSTOM_SERVICE)
            {
                MethodInfo method = typeof(ServiceConnector).GetMethod("CallService");
                MethodInfo generic = method.MakeGenericMethod(erpStep.ReturnType);
                generic.Invoke(null, new Object[5] { actionId, erpStep.ServiceMethod, erpStep.ServiceName, erpStep.DbTable, erpStep.MaxPageSize });
            }
            else if (erpStep.TaskType == ErpTaskStep.ErpTaskType.CUSTOM_SERVICE_BY_DATE)
            {
                MethodInfo method = typeof(ServiceConnector).GetMethod("CallServiceByDate");
                MethodInfo generic = method.MakeGenericMethod(erpStep.ReturnType);
                Func<DateTime, DateTime> action = null;
                switch (erpStep.PeriodIncrement)
                {
                    case ErpTaskStep.PeriodIncrementType.HOURS:
                        {
                            action = delegate (DateTime d) { return d.AddHours(1); };
                            break;
                        }
                    case ErpTaskStep.PeriodIncrementType.DAYS:
                        {
                            action = delegate (DateTime d) { return d.AddDays(1); };
                            break;
                        }
                    case ErpTaskStep.PeriodIncrementType.MONTHS:
                        {
                            action = delegate (DateTime d) { return d.AddMonths(1); };
                            break;
                        }
                    default:
                        {
                            action = null;
                            break;
                        }

                }
                Object[] parameters = new Object[6] { date, actionId, erpStep.ServiceMethod, erpStep.ServiceName, erpStep.DbTable, action };
                generic.Invoke(null, parameters);
            }
            return null;
        }
        public AxBaseException TaskList(int actionId, ErpTask erpTasks, DateTime date, int? noParallelProcess)
        {
            //DataWriter.TruncateTables(erpTasks.truncate_items, erpTasks.truncate_sales_trans_dump, erpTasks.truncate_sales_trans_refresh, erpTasks.truncate_locations_and_vendors,
            //    erpTasks.truncate_lookup_info, erpTasks.truncate_bom, erpTasks.truncate_po_to, erpTasks.truncate_price, erpTasks.truncate_attribute_refresh);
            TaskExecute exec = new TaskExecute(erpTasks.Steps, noParallelProcess.HasValue ? noParallelProcess.Value : 4 , actionId, date);
            exec.Execute();

            //foreach (var erpStep in erpTasks.Steps)
            //{
            //    ExecuteTask(actionId, erpStep, date); // possible to do some parallel processing.
            //}
            return null;
        }

        public AxBaseException GetSingleTable(ErpTaskStep step, int actionId, DateTime date)
        {
            if (date == DateTime.MaxValue)
            {
                DataWriter.TruncateSingleTable(step.DbTable);
            }
            List<ErpTaskStep> steps = new List<ErpTaskStep>();
            steps.Add(step);
            TaskExecute exec = new TaskExecute(steps, 1, actionId, date);
            exec.Execute();
            return null;

        }
        public AxBaseException GetBom(int actionId)
        {
            DataWriter.TruncateTables(false, false, false, false, false, true, false, false, false);
            return BomTransfer.GetBom(actionId);
        }

        public void GetPoTo(int actionId)
        {
            DataWriter.TruncateTables(false, false, false, false, false, false, true, false, false);
            POTransfer.GetPosAndTos(Context, actionId);
        }

        public void GetFullIoTrans(int actionId)
        {
            ProductHistory ph = new ProductHistory(actionId);
            ph.WriteInventTrans();
            ph.WriteInventTransOrigin();
            ph.WriteInventSumFull();
            if (includeB_M)
            {
                SalesValueTransactions.WriteSalesValueTrans(actionId);
                SalesValueTransactions.WriteSalesValueTransLines(actionId);
            }
        }
        #endregion
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
            rdp.POSRegistrationActivationDate = new DateTimeOffset(new DateTime(1900, 1, 1, 0, 0, 0));
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
            var master = new ProductMasterWriteDTO();
            master.AreIdenticalConfigurationsAllowed = NoYes.No;
            master.HarmonizedSystemCode = "";
            master.IsAutomaticVariantGenerationEnabled = NoYes.Yes;
            master.IsCatchWeightProduct = NoYes.No;
            master.IsProductKit = NoYes.No;
            master.IsProductVariantUnitConversionEnabled = NoYes.No;
            master.NMFCCode = "";
            master.ProductColorGroupId = "Basic";
            master.ProductDescription = "";
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
            rpm.TransferOrderOverdeliveryPercentage = 0;
            rpm.SalesUnitSymbol = "Ea";
            rpm.ProductionConsumptionWidthConversionFactor = 0;
            rpm.IsPurchasePriceAutomaticallyUpdated = NoYes.No;
            rpm.IsPurchaseWithholdingTaxCalculated = NoYes.No;
            rpm.TransferOrderUnderdeliveryPercentage = 0;
            rpm.IsDeliveredDirectly = NoYes.No;
            rpm.SalesSupplementaryProductProductGroupId = "";
            rpm.SalesMultilineDiscountProductGroupCode = "";
            rpm.WillTotalPurchaseDiscountCalculationIncludeProduct = NoYes.Yes;
            rpm.IsVariantShelfLabelsPrintingEnabled = NoYes.No;
            rpm.ProductionGroupId = "";
            rpm.OriginStateId = "";
            rpm.RevenueABCCode = ABC.None;
            rpm.NecessaryProductionWorkingTimeSchedulingPropertyId = "";
            rpm.IntrastatCommodityCode = "";
            rpm.SalesRebateProductGroupId = "";
            rpm.UnitCostDate = new DateTimeOffset(new DateTime(2012, 10, 6, 12, 0, 0, 0));
            rpm.DefaultProductSizeId = "";
            rpm.AlternativeProductStyleId = "";
            rpm.PotencyBaseAttributeId = "";
            rpm.AlternativeProductUsageCondition = ItemNumAlternative.Never;
            rpm.PurchaseUnderdeliveryPercentage = 0;
            rpm.DefaultProductStyleId = "";
            rpm.AlternativeProductSizeId = "";
            rpm.WillInventoryIssueAutomaticallyReportAsFinished = NoYes.No;
            rpm.ProductVolume = 0;
            rpm.TareProductWeight = 0;
            rpm.ItemNumber = master.ProductNumber;
            rpm.IsPhantom = NoYes.No;
            rpm.DefaultProductConfigurationId = "";
            rpm.FlushingPrinciple = ProdFlushingPrincipItem.Start;
            rpm.VariableScrapPercentage = 0;
            rpm.ArrivalHandlingTime = 0;
            rpm.SearchName = master.ProductSearchName;
            rpm.IsUnitCostIncludingCharges = NoYes.No;
            rpm.AlternativeProductColorId = "";
            rpm.PhysicalDimensionGroupId = "";
            rpm.MinimumCatchWeightQuantity = 0;
            rpm.SalesChargeProductGroupId = "";
            rpm.WillPickingWorkbenchApplyBoxingLogic = NoYes.No;
            rpm.ItemFiscalClassificationExceptionCode = "";
            rpm.InventoryReservationHierarchyName = "";
            rpm.IsZeroPricePOSRegistrationAllowed = NoYes.No;
            rpm.IntrastatChargePercentage = 0;
            rpm.IsScaleProduct = NoYes.No;
            rpm.PlanningFormulaItemNumber = "";
            rpm.ProductFiscalInformationType = "";
            rpm.StorageDimensionGroupName = "Ware";
            rpm.ShelfAdvicePeriodDays = 0;
            rpm.ContinuityScheduleId = "";
            rpm.VendorInvoiceLineMatchingPolicy = PurchMatchingPolicyWithNotSetOption.NotSet;
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
            rpm.SalesChargesQuantity = 0;
            rpm.SalesPriceCalculationContributionRatio = 0;
            rpm.IsSalesPriceAdjustmentAllowed = NoYes.No;
            rpm.SalesPriceCalculationChargesPercentage = 0;
            rpm.ShippingAndReceivingSortOrderCode = 0;
            rpm.GrossProductHeight = 0;
            rpm.ProductLifeCycleValidFromDate = new DateTimeOffset(new DateTime(1900, 1, 1, 12, 0, 0, 0));
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
            rpm.ProductCoverageGroupId = "";
            rpm.CostGroupId = "";
            rpm.IsPurchasePriceIncludingCharges = NoYes.No;
            rpm.IsShipAloneEnabled = NoYes.No;
            rpm.ProductNumber = master.ProductNumber;
            rpm.RawMaterialPickingPrinciple = WHSAllowMaterialOverPick.Staging;
            rpm.FixedPurchasePriceCharges = 0;
            rpm.FreightAllocationGroupId = "";
            rpm.ContinuityEventDuration = 0;
            rpm.DefaultDirectDeliveryWarehouse = "";
            rpm.SalesPriceDate = new DateTimeOffset(new DateTime(2012, 10, 6, 12, 0, 0, 0));
            rpm.OriginCountryRegionId = "";
            rpm.DefaultOrderType = ReqPOType.Purch;
            rpm.IsPOSRegistrationQuantityNegative = NoYes.No;
            rpm.PurchaseChargesQuantity = 0;
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
            //var rpmr = ServiceConnector.CallOdataEndpointPost("ReleasedProductMasters", null, rpm);
            var v = new ReleasedProductVariantDTO();
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
            //var rv = ServiceConnector.CallOdataEndpointPost("ReleasedProductVariants", null, v).Result;
        }
        public GenericWriteObject<ReleasedProductMaster> CreateMaster(List<ReleasedProductMaster> masters)
        {
            string axBaseUrl = ConfigurationManager.AppSettings["ax_base_url"];
            var clientconfig = new ClientConfiguration(axBaseUrl + "/data",
                                                       ConfigurationManager.AppSettings["ax_client_secret"],
                                                       axBaseUrl,
                                                       ConfigurationManager.AppSettings["ax_oauth_token_url"],
                                                       ConfigurationManager.AppSettings["ax_client_key"]);
            var oAuthHelper = new OAuthHelper(clientconfig);
            string dataarea = ConfigurationManager.AppSettings["DataAreaId"];
            AXODataContextConnector<ReleasedProductMaster> axConnector = new UpdateProductMaster<ReleasedProductMaster>(oAuthHelper, logMessageHandler: WriteLine, enableCrossCompany: true);
            axConnector.CreateRecordInAX(dataarea, masters);

            return new GenericWriteObject<ReleasedProductMaster> { Exception = null, WriteObject = masters[0] };
        }
        public GenericWriteObject<ReleasedProductVariant> UpdateVariants(List<ReleasedProductVariant> variants)
        {
            string axBaseUrl = ConfigurationManager.AppSettings["ax_base_url"];
            var clientconfig = new ClientConfiguration(axBaseUrl + "/data",
                                                       ConfigurationManager.AppSettings["ax_client_secret"],
                                                       axBaseUrl,
                                                       ConfigurationManager.AppSettings["ax_oauth_token_url"],
                                                       ConfigurationManager.AppSettings["ax_client_key"]);
            var oAuthHelper = new OAuthHelper(clientconfig);
            string dataarea = ConfigurationManager.AppSettings["DataAreaId"];
            AXODataContextConnector<ReleasedProductVariant> axConnector = new UpdateReleasedProductVariants<ReleasedProductVariant>(oAuthHelper, logMessageHandler: WriteLine, enableCrossCompany: true);
            axConnector.CreateRecordInAX(dataarea, variants);
            return new GenericWriteObject<ReleasedProductVariant> { Exception = null, WriteObject = variants[0] };
        }

        public AxBaseException CreateItems(int tempId, int actionId)
        {
            List<ItemToCreate> itemsToCreate = AxDbHandler.GetItemsToCreate(tempId);
            DateTime startTime = DateTime.Now;
            if (itemsToCreate.Any())
            {
                var masterData = itemsToCreate.First();
                if (masterData.master_status < 2)
                {
                    var master = new ProductMasterWriteDTO();
                    master.ProductDimensionGroupName = "CS";
                    master.ProductNumber = masterData.product_no;
                    master.ProductName = masterData.product_name;
                    master.ProductSearchName = masterData.product_name.Trim();
                    master.ProductSizeGroupId = masterData.size_group_no;
                    master.ProductColorGroupId = masterData.color_group_no; // possible to use color_group_no
                    //master.RetailProductCategoryName = masterData.sup_department;
                    master.ProductDescription = masterData.description;

                    //var erpMaster = CreateMaster(master);
                    var erpMaster = ServiceConnector.CallOdataEndpointPost<ProductMasterWriteDTO>("ProductMasters", null, master).Result;

                    if (erpMaster.Exception != null)
                    {
                        DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, false, erpMaster.Exception.ErrorMessage, erpMaster.Exception.StackTrace);
                        return erpMaster.Exception;
                    }
                    else if (erpMaster.WriteObject.ProductNumber.ToLower().Trim() != masterData.product_no.ToLower().Trim())
                    {
                        DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, false, null, null);
                        return new AxBaseException
                        {
                            ApplicationException = new ApplicationException(
                            "The product number for Product Master does not match the returned number, AX value = " + erpMaster.WriteObject.ProductNumber + " AGR number = " + masterData.product_no)
                        };
                    }

                    DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, true, null, null);
                    startTime = DateTime.Now;
                    var releasedMaster = new ReleasedProductMasterWriteDTO(master.ProductNumber, master.ProductSearchName,
                        masterData.primar_vendor_no, masterData.sale_price, masterData.cost_price);
                    var erpReleasedMaster = ServiceConnector.CallOdataEndpointPost<ReleasedProductMasterWriteDTO>("ReleasedProductMasters", null, releasedMaster).Result;

                    if (erpReleasedMaster.Exception != null)
                    {
                        DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Master", startTime, false, erpReleasedMaster.Exception.ErrorMessage, erpReleasedMaster.Exception.StackTrace);
                        return erpReleasedMaster.Exception;
                    }
                    else if (erpReleasedMaster.WriteObject.ItemNumber.ToLower().Trim() != master.ProductNumber.ToLower().Trim())
                    {
                        DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Master", startTime, false, null, null);
                        return new AxBaseException
                        {
                            ApplicationException = new ApplicationException(
                            "The item number for Released Product Master does not match the returned number, AX value = " + erpReleasedMaster.WriteObject.ItemNumber + " AGR number = " + masterData.product_no)
                        };
                    }
                    DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Master", startTime, true, null, null);
                }

                foreach (var item in itemsToCreate)
                {
                    var variant = new ReleasedProductVariantDTO
                    {
                        ItemNumber = item.product_no,
                        //ProductColorId = item.color_group_no,
                        ProductColorId = item.color_no,
                        ProductSizeId = item.size_no,
                        ProductConfigurationId = "",
                        ProductStyleId = "",
                        ProductDescription = item.description,
                        ProductName = item.product_name + "_" + item.color_no + "_" + item.size,
                        ProductSearchName = (item.product_name + "_" + item.color_no + "_" + item.size).Trim(),
                        ProductMasterNumber = item.product_no
                    };
                    startTime = DateTime.Now;
                    var erpVariants = ServiceConnector.CallOdataEndpointPost<ReleasedProductVariantDTO>("ReleasedProductVariants", null, variant).Result;

                    if (erpVariants.Exception != null)
                    {
                        DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Variant", startTime, false, erpVariants.Exception.ErrorMessage, erpVariants.Exception.StackTrace);
                        return erpVariants.Exception;
                    }
                    DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Variant", startTime, true, null, null);
                }
            }
            return null;
        }

        public AxBaseException CreatePoTo(List<POTOCreate> po_to_create, int actionId)
        {
            try
            {
                string axBaseUrl = ConfigurationManager.AppSettings["ax_base_url"];
                var clientconfig = new ClientConfiguration(axBaseUrl + "/data",
                                                           ConfigurationManager.AppSettings["ax_client_secret"],
                                                           axBaseUrl,
                                                           ConfigurationManager.AppSettings["ax_oauth_token_url"],
                                                           ConfigurationManager.AppSettings["ax_client_key"]);
                var oAuthHelper = new OAuthHelper(clientconfig);

                List<AGROrderDTO> a = new List<AGROrderDTO>();

                string dataarea = ConfigurationManager.AppSettings["DataAreaId"];

                AXODataContextConnector<AGROrderDTO> axConnector = new CreateOrder<AGROrderDTO>(oAuthHelper, logMessageHandler: WriteLine, enableCrossCompany: true);
                if (po_to_create.Any())
                {
                    var o = po_to_create.First();

                    AGROrderDTO order = new AGROrderDTO();
                    order.ARGId = o.order_id.ToString();
                    order.OrderFrom = o.order_from_location_no; //"1004"; //vendor number;
                    order.OrderTo = o.location_no; //"DC"; //warehouse;
                    order.OrderType = o.vendor_location_type.ToLower() == "vendor" ? AGROrderType.PO : AGROrderType.TO;
                    order.ReceiveDate = o.est_delivery_date < DateTime.Now.Date ? DateTime.Now.Date : o.est_delivery_date;
                    order.OrderStatus = AGROrderStatus.Created;

                    ArrayList orderline = new ArrayList();

                    for (int i = 0; i < po_to_create.Count; i++)
                    {
                        AGROrderLineDTO line = new AGROrderLineDTO();
                        line.ARGId = order.ARGId;
                        line.Color = po_to_create[i].color;
                        line.Config = "";
                        line.ItemId = po_to_create[i].item_no;
                        line.LineNum = i + 1;
                        line.Qty = po_to_create[i].unit_qty_chg;
                        line.Size = po_to_create[i].size;
                        line.Style = po_to_create[i].style;
                        line.OrderTo = po_to_create[i].location_no;
                        orderline.Add(line);
                    }

                    order.ArgOrderLine = orderline;

                    a.Add(order);

                    // Send the data file to the connector object:
                    var createOrder = axConnector.CreateRecordInAX(dataarea, a);
                    if(createOrder != null)
                    {
                        return createOrder;
                    }

                    order.OrderStatus = AGROrderStatus.Ready;
                    order.ArgOrderLine.Clear();
                    a = new List<AGROrderDTO>();
                    a.Add(order);

                    // Send the data file to the connector object:
                    return axConnector.CreateRecordInAX(dataarea, a);
                }
            }
            catch (Exception e)
            {
                return new AxBaseException { ApplicationException = e };
            }
            return null;
        }

        static void WriteLine(string msg, ConsoleColor color = ConsoleColor.Green, bool leadingLine = true)
        {
            Console.ForegroundColor = color;

            if (leadingLine) Console.WriteLine();
            Console.WriteLine(msg);

            //Log.Info(msg);
        }


        public AxBaseException DailyRefresh(DateTime date, int actionId)
        {
            var pim = PimFull(actionId);
            if (pim != null)
            {
                return pim;
            }
            TransactionRefresh(date, actionId);
            return null;
        }

        public AxBaseException FullTransfer(int actionId)
        {
            var pim = PimFull(actionId);
            if (pim != null)
            {
                return pim;
            }
            TransactionFull(actionId);
            return null;
        }

        public AxBaseException PimFull(int actionId)
        {
            DataWriter.TruncateTables(true, false, false, true, true, true, false, true, true);
            var cat = ItemCategoryTransfer.WriteCategories(actionId);
            if (cat != null)
            {
                return cat;
            }

            var loc = LocationsAndVendorsTransfer.WriteLocationsAndVendors(actionId);
            if (loc != null)
            {
                return loc;
            }

            var items = ItemTransfer.WriteItems(includesFashion, actionId);
            if (items != null)
            {
                return items;
            }

            var attr = ItemAttributeLookup.ReadItemAttributes(includesFashion, includeB_M, actionId);
            if (attr != null)
            {
                return attr;
            }

            var bom = GetBom(actionId);
            if (bom != null)
            {
                return bom;
            }
            var price = PriceHistory.GetPriceHistory(actionId, includeB_M);
            if (price != null)
            {
                return price;
            }
            return null;
        }

        public AxBaseException TransactionFull(int actionId)
        {
            DataWriter.TruncateTables(false, true, true, false, false, false, true, true, false);
            GetFullIoTrans(actionId);
            GetPoTo(actionId);
            return null;
        }

        public AxBaseException TransactionRefresh(DateTime date, int actionId)
        {
            DataWriter.TruncateTables(false, false, true, false, false, false, false, false, false);
            ProductHistory ph = new ProductHistory(actionId);
            ph.WriteInventSumRefresh(date);
            ph.WriteInventTransRefresh(date);
            ph.WriteInventTransOrigin();
            POTransfer.RefreshPurchLines(date, actionId);
            POTransfer.PullPurchTable(actionId);
            POTransfer.PullAGROrders(actionId);
            POTransfer.PullAGROrderLines(actionId);

            if (includeB_M)
            {
                SalesValueTransactions.WriteSalesValueTransRefresh(date, actionId);
                SalesValueTransactions.WriteSalesValueTransLinesRefresh(date, actionId);
            }
            return null;
        }
        public AxBaseException UpdateProduct(int actionId)
        {
            DataWriter.TruncateTables(false, false, false, false, false, false, false, false, true);
            var attributes = ItemAttributeLookup.UpdateProductAttributes(actionId);
            if (attributes != null)
            {
                return attributes;
            }
            return null;
        }

        public AxBaseException UpdateProductLifecycleState(int plcUpdateId, int actionId)
        {
            DateTime startTime = DateTime.Now;
            var plc = AxDbHandler.GetProductLifeCycleStateUpdates(plcUpdateId);
            if (plc.Any())
            {
                var distinctMaster = plc.DistinctBy(x => x.product_no).Select(y => y.product_no);
                foreach (var m in distinctMaster)
                {
                    var plcPerMaster = plc.Where(x => x.product_no == m);
                    string masterLifecycleState = "";
                    if (plcPerMaster.Where(x => x.lifecycle_status.ToLower() == "confirmed").Any())
                    {
                        masterLifecycleState = "Confirmed";
                    }
                    else if (plcPerMaster.Count(x => x.lifecycle_status.ToLower() == "delete") == plcPerMaster.Count())
                    {
                        masterLifecycleState = "Delete";
                    }
                    else if (plcPerMaster.Count(x => x.lifecycle_status.ToLower() == "shortlist") == plcPerMaster.Count())
                    {
                        masterLifecycleState = "Shortlist";
                    }
                    else
                    {
                        return new AxBaseException
                        {
                            ApplicationException = new Exception(string.Format("Plc update batch = {0} contains an invalid Product Lifecycle State in D365",
                                plcPerMaster.First().product_lifecycle_state_update_id))
                        };
                    }

                    if (plcPerMaster.Any())
                    {
                        var master = new ReleasedProductMaster();
                        master.ProductNumber = plcPerMaster.First().product_no;
                        //master.ProductLifecycleStateId = masterLifecycleState;
                        var erpMaster = CreateMaster(new List<ReleasedProductMaster> { master });

                        if (erpMaster.Exception != null)
                        {
                            DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, false, erpMaster.Exception.ErrorMessage, erpMaster.Exception.StackTrace);
                            return erpMaster.Exception;
                        }
                        else if (erpMaster.WriteObject.ProductNumber.ToLower().Trim() != plcPerMaster.First().product_no.ToLower().Trim())
                        {
                            DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, false, null, null);
                            return new AxBaseException
                            {
                                ApplicationException = new ApplicationException(
                                "The product number for Product Master does not match the returned number, AX value = " + erpMaster.WriteObject.ProductNumber + " AGR number = "
                                + plcPerMaster.First().product_no)
                            };
                        }

                        DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, true, null, null);
                        startTime = DateTime.Now;
                    }
                    List<ReleasedProductVariant> variantList = new List<ReleasedProductVariant>();
                    string variantPlc;
                    foreach (var item in plcPerMaster)
                    {
                        if (item.lifecycle_status.ToLower() == "confirmed")
                        {
                            variantPlc = "Confirmed";
                        }
                        else if (item.lifecycle_status.ToLower() == "delete")
                        {
                            variantPlc = "Delete";
                        }
                        else if (item.lifecycle_status.ToLower() == "shortlist")
                        {
                            variantPlc = "Shortlist";
                        }
                        else
                        {
                            return new AxBaseException
                            {
                                ApplicationException = new Exception(string.Format("Plc update batch = {0} {1} {2} {3} {4} contains an invalid Product Lifecycle State {5} in D365",
                                    item.product_no, item.product_color_id, item.product_size_id, item.product_style_id, item.product_config_id, item.lifecycle_status))
                            };
                        }
                        var variant = new ReleasedProductVariant
                        {
                            ItemNumber = item.product_no,
                            ProductMasterNumber = item.product_no,
                            ProductSizeId = item.product_size_id,
                            ProductColorId = item.product_color_id,
                            ProductStyleId = item.product_style_id,
                            ProductConfigurationId = item.product_config_id,
                            //ProductLifecycleStateId = variantPlc
                        };
                        startTime = DateTime.Now;
                        //var erpVariants = ServiceConnector.CallOdataEndpointPost<ReleasedProductVariantDTO>("ReleasedProductVariants", null, variant).Result;
                        variantList.Add(variant);
                    }
                    var erpVariants = UpdateVariants(variantList);
                    if (erpVariants.Exception != null)
                    {
                        DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Variant", startTime, false, erpVariants.Exception.ErrorMessage, erpVariants.Exception.StackTrace);
                        return erpVariants.Exception;
                    }
                    DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Variant", startTime, true, null, null);
                }
            }
            return null;
        }
    }
}
