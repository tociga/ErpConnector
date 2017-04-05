using AxConCommon.Extensions;
using AxConnect.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxConnect;
using AxConnect.DTO;

namespace AxConnect.Modules
{
    public class ItemTransfer
    {
        public static void WriteItems(Resources context, string authHeader)
        {
            var releasedMasters = AXServiceConnector.CallOdataEndpoint<ReleasedProductMasterReadDTO>("ReleasedProductMasters", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable<ReleasedProductMasterReadDTO>(releasedMasters.GetDataReader(), "[ax].[ReleasedProductMaster]");
            //var items = ReadProducts(context);
            var items = AXServiceConnector.CallOdataEndpoint<DistinctProductDTO>("ReleasedDistinctProducts", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable(items.GetDataReader(), "[ax].[ReleasedDistinctProducts]");

            var inventDim = AXServiceConnector.CallOdataEndpoint<InventDimDTO>("InventDims", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable(inventDim.GetDataReader(), "[ax].[INVENTDIM]");

            var custVendExt = AXServiceConnector.CallOdataEndpoint<CustVendExternalItemsDTO>("CustVendExternalItems", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable(custVendExt.GetDataReader(), "[ax].[CUSTVENDEXTERNALITEM]");

            var variants = ReadVariants(context);
            DataAccess.DataWriter.WriteToTable(variants, "[ax].[ReleasedProductVariants]");

            var combos = AXServiceConnector.CallOdataEndpoint<InventDimComboDTO>("InventDimCombinations", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable(combos.GetDataReader(), "[ax].[INVENTDIMCOMBINATIONS]");

            WriteServiceData<RetailAssortmentLookupDTO>("[ax]", "[RETAILASSORTMENTLOOKUP]", "GetRetailAssortmentLookup");
            WriteServiceData<RetailAssortmentLookupChannelGroupDTO>("[ax]", "[RETAILASSORTMENTLOOKUPCHANNELGROUP]", "GetRetailAssortmentLookupChannelGroup");

            var reqItems = ReadReqItemTable(context);
            DataAccess.DataWriter.WriteToTable(reqItems, "[ax].[REQITEMTABLE]");

            var reqKey = ReadReqSafetyKey(context);
            DataAccess.DataWriter.WriteToTable(reqKey, "[ax].[REQSAFETYKEY]");

            WriteServiceData<ReqSafetyLineDTO>("[ax]", "[REQSAFETYLINE]", "GetSafetyLines");

            // item_order_routes
            var itemPurchSetup = AXServiceConnector.CallOdataEndpoint<ItemPurchSetupDTO>("ItemPurchSetups", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable(itemPurchSetup.GetDataReader(), "[ax].[INVENTITEMPURCHSETUP]");

            var itemInventSetup = AXServiceConnector.CallOdataEndpoint<ItemInventSetupsDTO>("ItemInventSetups", "", authHeader).Result.value;
            DataAccess.DataWriter.WriteToTable(itemInventSetup.GetDataReader(), "[ax].[INVENTITEMINVENTSETUP]");

            WriteServiceData<UnitOfMeasureDTO>("[ax]", "[UNITOFMEASURE]", "GetUnitOfMeasure");
            WriteServiceData<UnitOfMeasureConversionDTO>("[ax]", "[UNITOFMEASURECONVERSION]", "GetUnitOfMeasureConversion");
        }

        
        private static void WriteServiceData<T>(string schemaName, string tableName, string webMethodName)
        {
            Int64 recId = DataAccess.DataWriter.GetMaxRecId(schemaName, tableName);
            Int64 pageSize = 20000;
            bool foundData = true;
            while (foundData)
            {
                foundData = WriteFromService<T>(recId, pageSize, webMethodName, schemaName + "."+tableName);
                recId = DataAccess.DataWriter.GetMaxRecId("[ax]", tableName);
            }

        }
 
        private static bool WriteFromService<T>(Int64 recId, Int64 pageSize, string webMethod, string destTable)
        {
            AXServiceConnector connector = new AXServiceConnector();
            string postData = "{ \"lastRecId\": " + recId.ToString() + ", \"pageSize\" : " + (pageSize).ToString() + "}";
            var result = connector.CallAGRServiceArray<T>("AGRItemCustomService", webMethod, postData);

            var reader = result.Result.GetDataReader();

            DataAccess.DataWriter.WriteToTable(reader, destTable);

            return result.Result.Any();
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

    }
}
