using System.Collections.Generic;
using System.Data;
using System.Linq;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;

namespace ErpConnector.Ax.Modules
{
    public class LocationsAndVendorsTransfer
    {
        public static void WriteLocationsAndVendors()
        {
            //var channel = ReadRetailChannel(context);
            var channel = ServiceConnector.CallOdataEndpoint<RetailChannel>("RetailChannels",
               // "?$filter=ChannelType eq Microsoft.Dynamics.DataEntities.RetailChannelType'RetailStore'",
                "").Result.value.GetDataReader();
            DataWriter.WriteToTable<RetailChannel>(channel, "[ax].[RETAILCHANNELTABLE]");

            //var assortment = ReadRetailAssortment(context);
            var assortment = ServiceConnector.CallOdataEndpoint<RetailAssortment>("RetailAssortments",
                "?$filter=Status eq Microsoft.Dynamics.DataEntities.RetailAssortmentStatusType'Published'"
                ).Result.value.GetDataReader();
            DataWriter.WriteToTable<RetailAssortment>(assortment, "[ax].[RETAILASSORTMENTTABLE]");

            //var locSetup = context.Locations.ToList().GetDataReader<Location>();
            var locSetup = ServiceConnector.CallOdataEndpoint<Location>("Locations", "").Result.value.GetDataReader();
            DataWriter.WriteToTable<Location>(locSetup, "[ax].[INVENTLOCATION]");

            //var dir = context.DirParties.ToList().GetDataReader<DirParty>();            
            var dir = ServiceConnector.CallOdataEndpoint<DirParty>("DirParties", "").Result.value.GetDataReader();
            DataWriter.WriteToTable<DirParty>(dir, "[ax].[DIRPARTYTABLE]");

            //var vendor = context.Vendors.ToList().GetDataReader<Vendor>();
            var vendor = ServiceConnector.CallOdataEndpoint<Vendor>("Vendors", "").Result.value.GetDataReader();
            DataWriter.WriteToTable<Vendor>(vendor, "[ax].[VENDTABLE]");


           
            var channelLines = ServiceConnector.CallOdataEndpoint<RetailAssortmentChannelLine>("RetailAssortmentChannelLines",
                "?$filter=Status eq Microsoft.Dynamics.DataEntities.RetailAssortmentStatusType'Published'")
                .Result.value.GetDataReader<RetailAssortmentChannelLine>();
            DataWriter.WriteToTable<RetailAssortmentChannelLine>(channelLines, "[ax].[RETAILASSORTMENTCHANNELLINE]");

            var productLines = ServiceConnector.CallOdataEndpoint<RetailAssortmentProductLine>("RetailAssortmentProductLines",
                "?$filter=Status eq Microsoft.Dynamics.DataEntities.RetailAssortmentStatusType'Published'")
                .Result.value.GetDataReader<RetailAssortmentProductLine>();
            DataWriter.WriteToTable<RetailAssortmentProductLine>(productLines, "[ax].[RETAILASSORTMENTPRODUCTLINE]");
        }

        private static IGenericDataReader ReadRetailChannel(Resources context)
        {
            var retailChannels = context.RetailChannels.Where(r => r.ChannelType == RetailChannelType.RetailStore);
            var list = new List<dynamic>();
            foreach(RetailChannel ch in retailChannels)
            {
                list.Add(new
                {
                    ONLINECATALOGNAME = ch.OnlineCatalogName,
                    STORENUMBER = ch.StoreNumber,
                    CLOSINGMETHOD = (int)ch.ClosingMethod.GetValueOrDefault(),
                    FUNCTIONALITYPROFILE = ch.FunctionalityProfile,
                    INVENTORYLOOKUP = ch.InventoryLookup.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
                    REMOVEADDTENDER = ch.RemoveAddTender,
                    STOREAREA = ch.StoreArea,
                    OMOPERATINGUNITID = ch.OperatingUnitNumber,
                    INVENTLOCATIONDATAAREAID = ch.InventLocationDataAreaId,
                    INVENTLOCATION = ch.InventLocation,
                    DEFAULTDIMENSION = ch.DefaultDimensionDisplayValue,
                    CHANNELTYPE = (int)ch.ChannelType.GetValueOrDefault(),
                    CATEGORYHIERARCHY = ch.CategoryHierarchyName,
                    PARTYNUMBER = ch.OperatingUnitPartyNumber,
                    RETAILCHANNELID = ch.RetailChannelId
                });

            }
            return list.GetDataReader<dynamic>();
        }

        private static IGenericDataReader ReadVendorTable(Resources context)
        {
            var vendors = context.Vendors;
            var list = new List<dynamic>();
            foreach(var v in vendors)
            {
                list.Add(new
                {
                    DATAAREAID = v.DataAreaId,
                    ACCOUNTNUM = v.VendorAccountNumber,
                    VENDGROUP = v.VendorGroupId,
                    CURRENCY = v.CurrencyCode,
                    BLOCKED = (int)v.OnHoldStatus.GetValueOrDefault(),
                    ONTIMEVENDOR = v.IsOneTimeVendor.GetValueOrDefault() == NoYes.Yes ? 1 : 0,
                    INVENTLOCATION = v.DefaultProcumentWarehouseId,
                    ITEMBUYERGROUPID = v.BuyerGroupId,
                    PARTY = v.VendorPartyNumber
                });
            }
            return list.GetDataReader<dynamic>();
        }

        private static IGenericDataReader ReadRetailAssortmentProductLine(Resources context)
        {
            var productLines = context.RetailAssortmentProductLines;
            var list = new List<dynamic>();
            foreach(var pl in productLines)
            {
                list.Add(new
                {
                    LINETYPE = pl.LineType,
                    STATUS = pl.Status,
                    ASSORTMENTID = pl.AssortmentId,
                    ITEMID = pl.ItemId,
                    COLOR = pl.Color,
                    SIZE = pl.Size,
                    STYLE = pl.Style,
                    CONFIGURATIONID = pl.ConfigurationId,
                    LINENUMBER = pl.LineNumber
                });
            }
            return list.GetDataReader<dynamic>();
        }

        private static IGenericDataReader ReadRetailAssortmentChannelLines(Resources context)
        {
            var channelGroup = context.RetailAssortmentChannelLines.ToList();//.Where(x=>x.Status == RetailAssortmentStatusType.Published);
            var list = new List<dynamic>();
            foreach(var cg in channelGroup)
            {
                list.Add(new
                {
                    ASSORTMENTID = cg.AssortmentId,
                    PARTYNUMBER = cg.PartyNumber,
                    STATUS = cg.Status.GetValueOrDefault()
                });
            }
            return list.GetDataReader<dynamic>();
        }

        private static IGenericDataReader ReadRetailAssortment(Resources context)
        {
            var retailAssortments = context.RetailAssortments.Where(x=>x.Status == RetailAssortmentStatusType.Published);
            var list = new List<dynamic>();
            foreach(var r in retailAssortments)
            {
                list.Add(new
                {
                    ASSORTMENTID = r.AssortmentID,
                    NAME = r.Name,
                    PUBLISHEDDATETIME = r.PublishedDateTime.DateTime,
                    STATUS = RetailAssortmentStatusType.Published,
                    VALIDFROM = r.ValidFrom.DateTime,
                    VALIDTO = r.ValidTo.DateTime
                });
            }
            return list.GetDataReader<dynamic>();
        }
    }
}
