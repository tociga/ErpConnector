using System;
using System.Collections.Generic;
using System.Linq;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Utils;
using ErpConnector.Common.Exceptions;

namespace ErpConnector.Ax.Modules
{
    public class ItemTransfer
    {
        public static AxBaseException WriteItems(bool includeFashion, int actionId)
        {
            var productMaster = ServiceConnector.CallOdataEndpoint<ProductMasterReadDTO>("ProductMasters", "", "[ax].[ProductMaster]", actionId).Result;
            if (productMaster != null)
            {
                return productMaster;
            }

            var releasedMasters = ServiceConnector.CallOdataEndpoint<ReleasedProductMaster>("ReleasedProductMasters", 2000, "[ax].[ReleasedProductMaster]", actionId).Result;
            if (releasedMasters != null)
            {
                return releasedMasters;
            }

            var distinctProducts = ServiceConnector.CallOdataEndpoint<DistinctProductsDTO>("DistinctProducts", "", "[ax].[DistinctProduct]", actionId).Result;
            if (distinctProducts != null)
            {
                return distinctProducts;
            }
            var items = ServiceConnector.CallOdataEndpoint<ReleasedDistinctProduct>("ReleasedDistinctProducts", 2000, "[ax].[ReleasedDistinctProducts]", actionId).Result;
            if (items != null)
            {
                return items;
            }

            var inventDim = ServiceConnector.CallOdataEndpoint<InventDimDTO>("InventDims", "", "[ax].[INVENTDIM]", actionId).Result;
            if (inventDim != null)
            {
                return inventDim;
            }

            var invTableModule = ServiceConnector.CallOdataEndpoint<TableModule>("TableModules", "", "[ax].[INVENTTABLEMODULE]",actionId).Result;
            if (invTableModule != null)
            {
                return invTableModule;
            }

            var custVendExt = ServiceConnector.CallOdataEndpoint<CustVendExternalItem>("CustVendExternalItems", "", "[ax].[CUSTVENDEXTERNALITEM]", actionId).Result;
            if (custVendExt != null)
            {
                return custVendExt;
            }

            var variants = ServiceConnector.CallOdataEndpoint<ReleasedProductVariant>("ReleasedProductVariants", "", "[ax].[ReleasedProductVariants]", actionId).Result;
            if (variants != null)
            {
                return variants;
            }

            var combos = ServiceConnector.CallOdataEndpoint<InventDimComboDTO>("InventDimCombinations", "", "[ax].[INVENTDIMCOMBINATIONS]", actionId).Result;
            if (combos != null)
            {
                return combos;
            }

            //var assortLookup = WriteServiceData<RetailAssortmentLookupDTO>("[ax]", "[RETAILASSORTMENTLOOKUP]", "GetRetailAssortmentLookup");
            //if (assortLookup != null)
            //{
            //    return assortLookup;
            //}

            var retailChannelLookup = WriteServiceData<RetailAssortmentLookupChannelGroupDTO>("[ax]", "[RETAILASSORTMENTLOOKUPCHANNELGROUP]", "GetRetailAssortmentLookupChannelGroup", actionId);
            if (retailChannelLookup != null)
            {
                return retailChannelLookup;
            }

            var reqItems = ServiceConnector.CallOdataEndpoint<AGRReqItemTable>("AGRReqItemTables", "", "[ax].[REQITEMTABLE]", actionId).Result;
            if (reqItems != null)
            {
                return reqItems;
            }

            

            var reqKey = ServiceConnector.CallOdataEndpoint<AGRReqSafetyKey>("AGRReqSafetyKeys", "", "[ax].[REQSAFETYKEY]", actionId).Result;
            if (reqKey != null)
            {
                return reqKey;
            }

            var saftyLines =  WriteServiceData<ReqSafetyLineDTO>("[ax]", "[REQSAFETYLINE]", "GetSafetyLines", actionId);
            if (saftyLines != null)
            {
                return saftyLines;
            }

            // item_order_routes
            var itemPurchSetup = ServiceConnector.CallOdataEndpoint<ItemPurchSetup>("ItemPurchSetups", "", "[ax].[INVENTITEMPURCHSETUP]", actionId).Result;
            if (itemPurchSetup != null)
            {
                return itemPurchSetup;
            }

            var itemInventSetup = ServiceConnector.CallOdataEndpoint<ItemInventSetup>("ItemInventSetups", "", "[ax].[INVENTITEMINVENTSETUP]",actionId).Result;
            if (itemInventSetup != null)
            {
                return itemInventSetup;
            }

            var unitOfMeasure = WriteServiceData<UnitOfMeasureDTO>("[ax]", "[UNITOFMEASURE]", "GetUnitOfMeasure",actionId);
            if (unitOfMeasure != null)
            {
                return unitOfMeasure;
            }
            var unitConv = WriteServiceData<UnitOfMeasureConversionDTO>("[ax]", "[UNITOFMEASURECONVERSION]", "GetUnitOfMeasureConversion",actionId);
            if (unitConv != null)
            {
                return unitConv;
            }

            if (includeFashion)
            {
            ////var inventSeason = context.InventSeasonTables.ToList().GetDataReader<InventSeasonTable>();
            ////    DataWriter.WriteToTable<InventSeasonTable>(inventSeason, "[ax].[InventSeasonTable]");

            ////WriteServiceData<InventColorSeasonDTO>("[ax]", "[InventColorSeason]", "GetInventSeasonColor");
            //    var inventColorSeason = GetFromService<InventColorSeasonDTO>("AGRFashionServiceGroup", "AGRFashionService", "GetInventSeasonColor", null);
            //    if (inventColorSeason.Exception != null)
            //    {
            //        return inventColorSeason.Exception;
            //    }
            //    DataWriter.WriteToTable(inventColorSeason.value.GetDataReader(), "[ax].[InventColorSeason]");
            
            }
            return null;
        }

        private static IGenericDataReader ReadInventSeasonTable(Resources context)
        {
           // var inventSeasons = context.InventSeasonTables.ToList();
            List<dynamic> list = new List<dynamic>();
            //foreach (var i in inventSeasons)
            //{
            //    list.Add(
            //        new
            //        {
            //            ItemId = i.ItemId,
            //            SeasonCode = i.SeasonCode,
            //            IsDefault = i.IsDefault
            //        });
            //}
            return list.GetDataReader<dynamic>();
        }

        private static AxBaseException WriteServiceData<T>(string schemaName, string tableName, string webMethodName, int actionId)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                Int64 recId = DataWriter.GetMaxRecId(schemaName, tableName);
                Int64 pageSize = 20000;
                GenericJsonOdata<T> result = null;
                bool firstRound = true;
                while (firstRound || result.value.Any())
                {
                    firstRound = false;
                    result = WriteFromService<T>(recId, pageSize, webMethodName, schemaName + "." + tableName);
                    if (result.Exception != null)
                    {
                        return result.Exception;
                    }
                    recId = DataWriter.GetMaxRecId("[ax]", tableName);
                }
                DataWriter.LogErpActionStep(actionId, tableName, startTime, true);
            }
            catch(Exception)
            {
                DataWriter.LogErpActionStep(actionId, tableName, startTime, true);
                throw;
            }
            return null;
        }

        private static GenericJsonOdata<T> WriteFromService<T>(Int64 recId, Int64 pageSize, string webMethod, string destTable)
        {
            string postData = "{ \"lastRecId\": " + recId.ToString() + ", \"pageSize\" : " + (pageSize).ToString() + "}";
            //var result = ServiceConnector.CallAGRServiceArray<T>("AGRItemCustomService", webMethod, postData);
            var result = GetFromService<T>(null, "AGRItemCustomService", webMethod, postData);
            var reader = result.value.GetDataReader();

            DataWriter.WriteToTable<T>(reader, destTable);

            return result;
        }

        private static GenericJsonOdata<T> GetFromService<T>(string serviceGroup, string service, string serviceMethod, string postData)
        {
            return ServiceConnector.CallAGRServiceArray<T>(service, serviceMethod, postData, serviceGroup).Result;
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

 
    }
}