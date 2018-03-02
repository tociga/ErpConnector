using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;
using ErpConnector.Common.Exceptions;

namespace ErpConnector.Ax.Modules
{
    public class BomTransfer
    {
        public static AxBaseException GetBom(int actionId)
        {
            var bomHeaders = ServiceConnector.CallOdataEndpoint<BillOfMaterialsHeader>("BillOfMaterialsHeaders", null, "[ax].[BillOfMaterialsHeaders]", actionId).Result;
            //if (bomHeaders != null)
            //{
            //    return bomHeaders;
            //}
            var bomLines = ServiceConnector.CallOdataEndpoint<BillOfMaterialsLine>("BillOfMaterialsLines", null, "[ax].[BillOfMaterialsLines]", actionId).Result;
            //if (bomLines != null)
            //{
            //    return bomLines;
            //}

            var bomVersion = ServiceConnector.CallOdataEndpoint<BillOfMaterialsVersion>("BillOfMaterialsVersions", null, "[ax].[BillOfMaterialsVersions]", actionId).Result;
            //if (bomVersion != null)
            //{
            //    return bomVersion;
            //}

            return null;
        }
    }
}
