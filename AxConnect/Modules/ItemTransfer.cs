using AxConCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpDTO.DTO;
using ErpDTO.Microsoft.Dynamics.DataEntities;

namespace AxConnect.Modules
{
    public class ItemTransfer
    {
        public static void WriteItems(Resources context, string authHeader)
        {
            var productMaster = AXServiceConnector.CallOdataEndpoint<ProductMasterReadDTO>("ProductMasters", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable<ProductMasterReadDTO>(productMaster.GetDataReader(), "[ax].[ProductMaster]");

            var releasedMasters = AXServiceConnector.CallOdataEndpoint<ReleasedProductMasterReadDTO>("ReleasedProductMasters", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable<ReleasedProductMasterReadDTO>(releasedMasters.GetDataReader(), "[ax].[ReleasedProductMaster]");
            //var items = ReadProducts(context);
            var distinctProducts = AXServiceConnector.CallOdataEndpoint<DistinctProductsDTO>("DistinctProducts", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable<DistinctProductsDTO>(distinctProducts.GetDataReader(), "[ax].[DistinctProduct]");

            var items = AXServiceConnector.CallOdataEndpoint<ReleasedDistinctProductsReadDTO>("ReleasedDistinctProducts", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable<ReleasedDistinctProductsReadDTO>(items.GetDataReader(), "[ax].[ReleasedDistinctProducts]");

            var inventDim = AXServiceConnector.CallOdataEndpoint<InventDimDTO>("InventDims", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable(inventDim.GetDataReader(), "[ax].[INVENTDIM]");

            var custVendExt = AXServiceConnector.CallOdataEndpoint<CustVendExternalItemsDTO>("CustVendExternalItems", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable(custVendExt.GetDataReader(), "[ax].[CUSTVENDEXTERNALITEM]");

            var variants = ReadVariants(context);
            DataAccess.DataWriter.WriteToTable(variants, "[ax].[ReleasedProductVariants]");

            var combos = AXServiceConnector.CallOdataEndpoint<InventDimComboDTO>("InventDimCombinations", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable(combos.GetDataReader(), "[ax].[INVENTDIMCOMBINATIONS]");

            WriteServiceData<RetailAssortmentLookupDTO>("[ax]", "[RETAILASSORTMENTLOOKUP]", "GetRetailAssortmentLookup", authHeader);
            WriteServiceData<RetailAssortmentLookupChannelGroupDTO>("[ax]", "[RETAILASSORTMENTLOOKUPCHANNELGROUP]", "GetRetailAssortmentLookupChannelGroup", authHeader);

            var reqItems = ReadReqItemTable(context);
            DataAccess.DataWriter.WriteToTable(reqItems, "[ax].[REQITEMTABLE]");

            var reqKey = ReadReqSafetyKey(context);
            DataAccess.DataWriter.WriteToTable(reqKey, "[ax].[REQSAFETYKEY]");

            WriteServiceData<ReqSafetyLineDTO>("[ax]", "[REQSAFETYLINE]", "GetSafetyLines", authHeader);

            // item_order_routes
            var itemPurchSetup = AXServiceConnector.CallOdataEndpoint<ItemPurchSetupDTO>("ItemPurchSetups", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable(itemPurchSetup.GetDataReader(), "[ax].[INVENTITEMPURCHSETUP]");

            var itemInventSetup = AXServiceConnector.CallOdataEndpoint<ItemInventSetupsDTO>("ItemInventSetups", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable(itemInventSetup.GetDataReader(), "[ax].[INVENTITEMINVENTSETUP]");

            WriteServiceData<UnitOfMeasureDTO>("[ax]", "[UNITOFMEASURE]", "GetUnitOfMeasure", authHeader);
            WriteServiceData<UnitOfMeasureConversionDTO>("[ax]", "[UNITOFMEASURECONVERSION]", "GetUnitOfMeasureConversion", authHeader);

            var inventSeason = ReadInventSeasonTable(context);
            DataAccess.DataWriter.WriteToTable(inventSeason, "[ax].[InventSeasonTable]");

            //WriteServiceData<InventColorSeasonDTO>("[ax]", "[InventColorSeason]", "GetInventSeasonColor");
            var inventColorSeason = GetFromService<InventColorSeasonDTO>("AGRFashionServiceGroup", "AGRFashionService", "GetInventSeasonColor", null, authHeader);
            DataAccess.DataWriter.WriteToTable(inventColorSeason.GetDataReader(), "[ax].[InventColorSeason]");
        }

        private static IGenericDataReader ReadInventSeasonTable(Resources context)
        {
            var inventSeasons = context.InventSeasonTables.ToList();
            List<dynamic> list = new List<dynamic>();
            foreach (var i in inventSeasons)
            {
                list.Add(
                    new
                    {
                        ItemId = i.ItemId,
                        SeasonCode = i.SeasonCode,
                        IsDefault = i.IsDefault
                    });
            }
            return list.GetDataReader<dynamic>();
        }

        private static void WriteServiceData<T>(string schemaName, string tableName, string webMethodName, string adalHeader)
        {
            Int64 recId = DataAccess.DataWriter.GetMaxRecId(schemaName, tableName);
            Int64 pageSize = 20000;
            bool foundData = true;
            while (foundData)
            {
                foundData = WriteFromService<T>(recId, pageSize, webMethodName, schemaName + "."+tableName, adalHeader);
                recId = DataAccess.DataWriter.GetMaxRecId("[ax]", tableName);
            }

        }

        private static bool WriteFromService<T>(Int64 recId, Int64 pageSize, string webMethod, string destTable, string adalHeader)
        {
            string postData = "{ \"lastRecId\": " + recId.ToString() + ", \"pageSize\" : " + (pageSize).ToString() + "}";
            //var result = AXServiceConnector.CallAGRServiceArray<T>("AGRItemCustomService", webMethod, postData);
            var result = GetFromService<T>(null, "AGRItemCustomService", webMethod, postData, adalHeader);
            var reader = result.GetDataReader();

            DataAccess.DataWriter.WriteToTable(reader, destTable);

            return result.Any();
        }

        private static List<T> GetFromService<T>(string serviceGroup, string service, string serviceMethod, string postData, string adalHeader)
        {
            return AXServiceConnector.CallAGRServiceArray<T>(service, serviceMethod, postData, adalHeader, serviceGroup).Result;
        }
        //private static IGenericDataReader ReadInventDimCombo(Resources context)
        //{
        //    var combos = context.AGRInventDimCombinations.ToList();
        //    var list = new List<dynamic>();
        //    foreach(var c in combos)
        //    {
        //        list.Add(
        //            new
        //            {
        //                ItemId                  = c.ItemId,
        //                InventDimId             = c.InventDimId,
        //                DataAreaId              = c.DataAreaId,
        //                DisplayProductNumber    = c.EcoResDistinctProductVariant_DisplayProductNumber,
        //                RetailVariantId         = c.RetailVariantId
        //            });
        //    }
        //    return list.GetDataReader<dynamic>();

        //}
        private static IGenericDataReader ReadProducts(Resources context)
        {
            var resProducts = from p in context.ReleasedDistinctProducts select p;
            var retailInvent = context.RetailInventTable.ToList();

            //context.ReleasedProductVariants.
            List<dynamic> products = new List<dynamic>();
            foreach (var prod in resProducts)
            {
                //var prodInvent = retailInvent.Single(s => s.ItemId == prod.ItemNumber);
                products.Add(new
                {
                    DATAAREAID = prod.DataAreaId,
                    //PARTITION = prod.Partition,
                    ITEMID = prod.ItemNumber,
                    ITEMTYPE = prod.ProductType.HasValue ? (int)prod.ProductType.Value : 0,
                    HEIGTH = prod.GrossProductHeight,
                    WIDTH = prod.GrossProductWidth,
                    PRIMARYVENDORID = prod.PrimaryVendorAccountNumber,
                    NETWEIGHT = prod.NetProductWeight,
                    DEPTH = prod.GrossDepth,
                    UNITVOLUME = prod.ProductVolume,
                    ABCREVENUE = prod.RevenueABCCode.HasValue ? (int)prod.RevenueABCCode.Value : 0,
                    ABCVALUE = prod.ValueABCCode.HasValue ? (int)prod.ValueABCCode.Value : 0,
                    ABCCONTRIBUTIONMARGIN = prod.MarginABCCode.HasValue ? (int)prod.MarginABCCode.Value : 0,
                    NAMEALIAS = prod.SearchName,
                    PRODUCTGROUPID = prod.ProductGroupId,
                    PROJCATEGORYID = prod.ProjectCategoryId,
                    STANDARDPALLETQUANTITY = 0m,///prod.StandardPalletQty,
                    QTYPERLAYER = 0.0m,//prod.QtyPerLayer,
                    ITEMBUYERGROUPID = prod.BuyerGroupId,
                    PRODUCT = prod.ProductRecId,//,
                    SALEPRICE = prod.FixedSalesPriceCharges,

                });
            }
            return GenericListDataReaderExtensions.GetDataReader<dynamic>(products);
        }

        private static IGenericDataReader ReadVariants(Resources context)
        {
            var resVariants = from variant in context.ReleasedProductVariants select variant;
            //var productMaster = context.ReleasedProductMasters.ToArray();
            List<dynamic> variants = new List<dynamic>();
            foreach (var v in resVariants)
            {
                //var pm = productMaster.FirstOrDefault(x=>x.P)
                variants.Add(new
                {
                    ITEMID = v.ItemNumber,
                    DATAAREAID = v.DataAreaId,
                    INVENTDIMID = v.ProductVariantNumber,
                    INVENTBATCHID = v.ReleasedProductMaster != null ?  v.ReleasedProductMaster.BatchNumberGroupCode : "",
                    //WMSLOCATIONID =
                    //WMSPALLETID =
                    INVENTSERIALID = v.ReleasedProductMaster != null ? v.ReleasedProductMaster.SerialNumberGroupCode : "",
                    //INVENTLOCATIONID =
                    CONFIGID = v.ProductConfigurationId,
                    INVENTSIZEID = v.ProductSizeId,
                    INVENTCOLORID = v.ProductColorId,
                    //INVENTSITEID = v.S,
                    INVENTSTYLEID = v.ProductStyleId,
                    PRODUCTMASTERNUMBER = v.ProductMasterNumber,
                    RECID = v.PublicRecId
                    //MODIFIEDDATETIME =
                    //RECVERSION = v.ProductMaster.Re
                });
            }
            return variants.GetDataReader<dynamic>();
        }

        private static IGenericDataReader ReadInventDim(Resources context)
        {
            var dims = context.InventDims;
            var list = new List<dynamic>();
            foreach(var dim in dims)
            {
                list.Add(new
                {
                    DATAAREAID = dim.DataAreaId,
                    INVENTDIMID = dim.DimensionNumber,
                    INVENTBATCHID = dim.BatchNumber,
                    WMSLOCATIONID = dim.Warehouse,
                    WMSPALLETID = dim.PalletID,
                    INVENTSERIALID = dim.SerialNumber,
                    INVENTLOCATIONID = dim.Location,
                    CONFIGID = dim.Configuration,
                    INVENTSIZEID = dim.Size,
                    INVENTCOLORID = dim.Color,
                    INVENTSITEID = dim.Site,
                    INVENTSTYLEID = dim.Style,
                    INVENTSTATUSID = dim.InventoryStatus,
                    //MODIFIEDDATETIME =
                    //RECVERSION =
                });
            }
            return list.GetDataReader<dynamic>();
        }

        private static IGenericDataReader ReadReqItemTable(Resources Context)
        {
            var reqItemTable = Context.AGRReqItemTables;
            var list = new List<dynamic>();
            foreach(var req in reqItemTable)
            {
                list.Add(new {
                    DATAAREAID = req.DataAreaId,
                    ITEMID = req.ItemId,
                    MININVENTONHAND = req.MinInventOnhand,
                    MAXINVENTONHAND = req.MaxInventOnhand,
                    LEADTIMETRANSFER = req.LeadTimeTransfer,
                    LEADTIMETRANSFERACTIVE = req.LeadTimeTransferActive.GetValueOrDefault() == NoYes.Yes,
                    LEADTIMEPURCHASE = req.LeadTimePurchase,
                    LEADTIMEPURCHASEACTIVE = req.LeadTimePurchaseActive.GetValueOrDefault() == NoYes.Yes,
                    COVINVENTDIMID = req.CovInventDimId,
                    ITEMCOVFIELDSACTIVE = req.ItemCovFieldsActive.GetValueOrDefault() == NoYes.Yes,
                    VENDID =req.VendId,
                    MINSAFETYKEYID = req.MinSafetyKeyId,
                    MAXSAFETYKEYID = req.MaxSafetyKeyId,
                    INVENTLOCATIONIDREQMAIN = req.InventLocationIdReqMain,
                    REQPOTYPE = req.ReqPOType,
                    REQPOTYPEACTIVE = req.ReqPOTypeActive.GetValueOrDefault() == NoYes.Yes
                });
            }
            return list.GetDataReader<dynamic>();
        }

        private static IGenericDataReader ReadReqSafetyKey(Resources context)
        {
            var list = new List<dynamic>();
            var keys = context.AGRReqSafetyKeys.ToList();
            foreach(var key in keys)
            {
                list.Add(
                    new
                    {
                        FIXED = key.Fixed.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
                        NAME = key.Name,
                        SAFETYKEY = key.SafetyKeyId,
                        FIXEDDATE = key.FixedDate.DateTime
                    });
            }
            return list.GetDataReader<dynamic>();
        }

        public static ItemDTO CreateItem(ItemDTO item, string header)
        {
            //ProductMasterWriteDTO master = new ProductMasterWriteDTO();
            //master.AreIdenticalConfigurationsAllowed = NoYes.No;
            //master.HarmonizedSystemCode = "";
            //master.IsAutomaticVariantGenerationEnabled = NoYes.Yes;
            //master.IsCatchWeightProduct = NoYes.No;
            //master.IsProductKit = NoYes.No;
            //master.IsProductVariantUnitConversionEnabled = NoYes.No;
            ////master.KPMInstructionGroupId = "";
            ////master.KRFColorRatioCurve = "";
            ////master.KRFSizeRatioCurve = "";
            ////master.KRFStyleRatioCurve = "";
            ////master.KRFUseRatioCurves = NoYes.Yes;
            //master.NMFCCode = "";
            //master.ProductColorGroupId = "Basic";
            //master.ProductDescription = "";
            ////master.ProductDimensionGroupName = "CSF";
            //master.ProductDimensionGroupName = "SizeCol";
            //master.ProductName = dp.ProductName;
            //master.ProductNumber = dp.ProductNumber;
            //master.ProductSearchName = dp.ProductSearchName;
            //master.ProductSizeGroupId = "10-18";
            //master.ProductStyleGroupId = "";
            //master.VariantConfigurationTechnology = EcoResVariantConfigurationTechnologyType.PredefinedVariants;
            //master.RetailProductCategoryName = "";
            //master.ProductType = EcoResProductType.Item;
            //master.STCCCode = "";
            //master.TrackingDimensionGroupName = "None";
            //master.StorageDimensionGroupName = "Ware";
            
            var r = AXServiceConnector.CreateEntity<ProductMasterWriteDTO>("ProductMasters", null, header, item.productMaster, item.ErrorMessages).Result;
            item.productMaster = r;            
            return item;
        }
    }
}