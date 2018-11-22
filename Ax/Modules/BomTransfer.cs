using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common;
using ErpConnector.Common.Util;
using ErpConnector.Common.ErpTasks;

namespace ErpConnector.Ax.Modules
{
    public class BomTransfer
    {
        public static AxBaseException GetBom(int actionId)
        {
            var bomHeaders = ServiceConnector.CallOdataEndpoint<BillOfMaterialsHeader>("BillOfMaterialsHeaders", null, "[ax].[BillOfMaterialsHeaders]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            //if (bomHeaders != null)
            //{
            //    return bomHeaders;
            //}
            var bomLines = ServiceConnector.CallOdataEndpoint<BillOfMaterialsLine>("BillOfMaterialsLines", null, "[ax].[BillOfMaterialsLines]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            //if (bomLines != null)
            //{
            //    return bomLines;
            //}

            var bomVersion = ServiceConnector.CallOdataEndpoint<BillOfMaterialsVersion>("BillOfMaterialsVersions", null, "[ax].[BillOfMaterialsVersions]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
            //if (bomVersion != null)
            //{
            //    return bomVersion;
            //}

            return null;
        }
    }
}
