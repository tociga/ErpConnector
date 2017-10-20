using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;
using ErpConnector.Common.Exceptions;

namespace ErpConnector.Ax.Modules
{
    public class BomTransfer
    {
        public static AxBaseException GetBom()
        {
            var bomHeaders = ServiceConnector.CallOdataEndpoint<BillOfMaterialsHeader>("BillOfMaterialsHeaders", null, "[ax].[BillOfMaterialsHeaders]").Result;
            if (bomHeaders != null)
            {
                return bomHeaders;
            }
            var bomLines = ServiceConnector.CallOdataEndpoint<BillOfMaterialsLine>("BillOfMaterialsLines", null, "[ax].[BillOfMaterialsLines]").Result;
            if (bomLines != null)
            {
                return bomLines;
            }

            var bomVersion = ServiceConnector.CallOdataEndpoint<BillOfMaterialsVersion>("BillOfMaterialsVersions", null, "[ax].[BillOfMaterialsVersions]").Result;
            if (bomVersion != null)
            {
                return bomVersion;
            }

            return null;
        }
    }
}
