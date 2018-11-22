using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common;
using ErpConnector.Common.Util;
using ErpConnector.Common.ErpTasks;

namespace ErpConnector.Ax.Modules
{
    public class LocationsAndVendorsTransfer
    {
        public static AxBaseException WriteLocationsAndVendors(int actionId)
        {
            //var channel = ReadRetailChannel(context);
            var channel = ServiceConnector.CallOdataEndpoint<RetailChannel>("RetailChannels",
               // "?$filter=ChannelType eq Microsoft.Dynamics.DataEntities.RetailChannelType'RetailStore'",
                "", "[ax].[RETAILCHANNELTABLE]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            //if (channel != null)
            //{
            //    return channel;
            //}

            //var assortment = ReadRetailAssortment(context);
            var assortment = ServiceConnector.CallOdataEndpoint<RetailAssortment>("RetailAssortments",
                "?$filter=Status eq Microsoft.Dynamics.DataEntities.RetailAssortmentStatusType'Published'"
                , "[ax].[RETAILASSORTMENTTABLE]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;

            //if (assortment != null)
            //{
            //    return assortment;
            //}
            //var locSetup = context.Locations.ToList().GetDataReader<Location>();
            var locSetup = ServiceConnector.CallOdataEndpoint<Location>("Locations", "", "[ax].[INVENTLOCATION]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            //if (locSetup != null)
            //{
            //    return locSetup;
            //}
            //var dir = context.DirParties.ToList().GetDataReader<DirParty>();            
            var dir = ServiceConnector.CallOdataEndpoint<DirParty>("DirParties", "", "[ax].[DIRPARTYTABLE]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            //if (dir != null)
            //{
            //    return dir;
            //}
            //var vendor = context.Vendors.ToList().GetDataReader<Vendor>();
            var vendor = ServiceConnector.CallOdataEndpointWithPageSize<Vendor>("Vendors", 200, "[ax].[VENDTABLE]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            //if (vendor != null)
            //{
            //    return vendor;
            //}

            var channelLines = ServiceConnector.CallOdataEndpoint<RetailAssortmentChannelLine>("RetailAssortmentChannelLines",
                "?$filter=Status eq Microsoft.Dynamics.DataEntities.RetailAssortmentStatusType'Published'", "[ax].[RETAILASSORTMENTCHANNELLINE]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365))
                .Result;
            //if (channelLines != null)
            //{
            //    return channelLines;
            //}

            var productLines = ServiceConnector.CallOdataEndpoint<RetailAssortmentProductLine>("RetailAssortmentProductLines",
                "?$filter=Status eq Microsoft.Dynamics.DataEntities.RetailAssortmentStatusType'Published'", "[ax].[RETAILASSORTMENTPRODUCTLINE]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365))
                .Result;
            //if (productLines != null)
            //{
            //    return productLines;
            //}
            return null;
        }
       
    }
}
