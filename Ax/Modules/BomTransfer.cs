using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.DTO;

namespace ErpConnector.Ax.Modules
{
    public class BomTransfer
    {
        public static void GetBom(string header)
        {
            var bomHeaders = AXServiceConnector.CallOdataEndpoint<BillOfMaterialsHeader>("BillOfMaterialsHeaders", null, header).Result;
            DataWriter.WriteToTable<BillOfMaterialsHeader>(bomHeaders.value.GetDataReader(), "[ax].[BillOfMaterialsHeaders]");

            var bomLines = AXServiceConnector.CallOdataEndpoint<BillOfMaterialsLine>("BillOfMaterialsLines", null, header).Result;
            DataWriter.WriteToTable<BillOfMaterialsLine>(bomLines.value.GetDataReader(), "[ax].[BillOfMaterialsLines]");

            var bomVersion = AXServiceConnector.CallOdataEndpoint<BillOfMaterialsVersion>("BillOfMaterialsVersions", null, header).Result;
            DataWriter.WriteToTable<BillOfMaterialsVersion>(bomVersion.value.GetDataReader(), "[ax].[BillOfMaterialsVersions]");

        }
    }
}
