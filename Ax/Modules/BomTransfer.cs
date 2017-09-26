using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;

namespace ErpConnector.Ax.Modules
{
    public class BomTransfer
    {
        public static void GetBom()
        {
            var bomHeaders = ServiceConnector.CallOdataEndpoint<BillOfMaterialsHeader>("BillOfMaterialsHeaders", null).Result;
            DataWriter.WriteToTable<BillOfMaterialsHeader>(bomHeaders.value.GetDataReader(), "[ax].[BillOfMaterialsHeaders]");

            var bomLines = ServiceConnector.CallOdataEndpoint<BillOfMaterialsLine>("BillOfMaterialsLines", null).Result;
            DataWriter.WriteToTable<BillOfMaterialsLine>(bomLines.value.GetDataReader(), "[ax].[BillOfMaterialsLines]");

            var bomVersion = ServiceConnector.CallOdataEndpoint<BillOfMaterialsVersion>("BillOfMaterialsVersions", null).Result;
            DataWriter.WriteToTable<BillOfMaterialsVersion>(bomVersion.value.GetDataReader(), "[ax].[BillOfMaterialsVersions]");

        }
    }
}
