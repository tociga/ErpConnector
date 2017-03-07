using AxConnect.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxConCommon.Extensions;
using AxConnect.DTO;

namespace AxConnect.Modules
{
    public class LocationsAndVendorsTransfer
    {
        public static void WriteLocationsAndVendors(Resources context, string authHeader)
        {
            //var channel = ReadRetailChannel(context);
            var channel = AXServiceConnector.CallOdataEndpoint<RetailChannelDTO>("RetailChannels", 
                "?$filter=ChannelType eq Microsoft.Dynamics.DataEntities.RetailChannelType'RetailStore'", 
                authHeader).Result.value.GetDataReader();
            DataAccess.DataWriter.WriteToTable(channel, "[ax].[RETAILCHANNELTABLE]");

            //var assortment = ReadRetailAssortment(context);
            var assortment = AXServiceConnector.CallOdataEndpoint<RetailAssortmentDTO>("RetailAssortments", 
                "?$filter=Status eq Microsoft.Dynamics.DataEntities.RetailAssortmentStatusType'Published'", 
                authHeader).Result.value.GetDataReader();
            DataAccess.DataWriter.WriteToTable(assortment, "[ax].[RETAILASSORTMENTTABLE]");

            var locSetup = ReadLocations(context);
            DataAccess.DataWriter.WriteToTable(locSetup, "[ax].[INVENTLOCATION]");

            var dir = ReadDirParty(context);
            DataAccess.DataWriter.WriteToTable(dir, "[ax].[DIRPARTYTABLE]");

            var vendor = ReadVendorTable(context);
            DataAccess.DataWriter.WriteToTable(vendor, "[ax].[VENDTABLE]");

            
            var channelLines = ReadRetailAssortmentChannelLines(context);
            DataAccess.DataWriter.WriteToTable(channelLines, "[ax].[RETAILASSORTMENTCHANNELLINE]");

            var productLines = ReadRetailAssortmentProductLine(context);
            DataAccess.DataWriter.WriteToTable(productLines, "[ax].[RETAILASSORTMENTPRODUCTLINE]");
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
        private static IGenericDataReader ReadLocations(Resources context)
        {
            var locations = context.AGRInventLocations;
            var list = new List<dynamic>();
            foreach(var loc in locations)
            {
                list.Add(new
                {
                    DataAreaId = loc.DataAreaId,
                    INVENTLOCATIONID = loc.Warehouse,
                    NAME = loc.Name,
                    REQREFILL = loc.Refilling.GetValueOrDefault() == NoYes.Yes,
                    INVENTLOCATIONTYPE = loc.Type,                    
                    FSHSTORE = (int)loc.Store.GetValueOrDefault(),
                    RETAILWEIGHTEX1 = loc.Weight,
                    INVENTSITEID = loc.Site,
                    INVENTLOCATIONIDREQMAIN = loc.MainWarehouse
                });
            }
            return list.GetDataReader<dynamic>();
        }

        private static IGenericDataReader ReadDirParty(Resources context)
        {
            var dirParties = context.DirParties;
            var list = new List<dynamic>();
            foreach(var d in dirParties)
            {
                list.Add(new
                {

                    PARTYID = d.PartyID,
                    NAME = d.Name,
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
            var channelGroup = context.RetailAssortmentChannelLines.Where(x=>x.Status == RetailAssortmentStatusType.Published);
            var list = new List<dynamic>();
            foreach(var cg in channelGroup)
            {
                list.Add(new
                {
                    ASSORTMENTID = cg.AssortmentId,
                    PARTYNUMBER = cg.PartyNumber,
                    STATUS = RetailAssortmentStatusType.Published
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
