using AxConCommon.Extensions;
using AxConnect.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.Modules
{
    public class ItemTransfer
    {
        public static void WriteItems(Resources context)
        {
            var items = ReadProducts(context);
            DataAccess.DataWriter.WriteToTable(items, "[ax].[INVENTTABLE]");

            var inventDim = ReadInventDim(context);
            DataAccess.DataWriter.WriteToTable(inventDim, "[ax].[INVENTDIM]");

            var variants = ReadVariants(context);
            DataAccess.DataWriter.WriteToTable(variants, "[ax].[ReleasedProductVariants]");

            var combos = ReadInventDimCombo(context);
            DataAccess.DataWriter.WriteToTable(combos, "[ax].[INVENTDIMCOMBINATIONS]");
        }

        private static IGenericDataReader ReadInventDimCombo(Resources context)
        {
            var combos = context.AGRInventDimCombinations.ToList();
            var list = new List<dynamic>();
            foreach(var c in combos)
            {
                list.Add(
                    new
                    {
                        ItemId = c.ItemId,
                        InventDimId = c.InventDimId,
                        DataAreaId = c.DataAreaId,
                        DisplayProductNumber = c.EcoResDistinctProductVariant_DisplayProductNumber,
                        RetailVariantId = c.RetailVariantId
                    });
            }
            return list.GetDataReader<dynamic>();

        }
        private static IGenericDataReader ReadProducts(Resources context)
        {
            var resProducts = from p in context.ReleasedDistinctProducts select p;
            var retailInvent = context.RetailInventTable.ToList();
            //context.ReleasedProductVariants.
            List<dynamic> products = new List<dynamic>();
            foreach (var prod in resProducts)
            {
                var prodInvent = retailInvent.Single(s => s.ItemId == prod.ItemNumber);
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
                    STANDARDPALLETQUANTITY = 0.0m,//prodInvent.StandardPalletQty,
                    QTYPERLAYER = 0.0m,//prodInvent.QtyPerLayer,
                    ITEMBUYERGROUPID = prod.BuyerGroupId,
                    PRODUCT = prodInvent.Product,//,
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
                    PRODUCTMASTERNUMBER = v.ProductMasterNumber//,
                    //MODIFIEDDATETIME = 
                    //RECVERSION = v.ProductMaster.Re
                });
            }
            return variants.GetDataReader<dynamic>();
        }

        private static IGenericDataReader ReadInventDim(Resources context)
        {
            var dims = context.AGRInventDims;
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

        private IGenericDataReader ReadReqItemTable(Resources Context)
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

    }
}
