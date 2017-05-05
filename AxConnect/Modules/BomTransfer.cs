using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpDTO.Microsoft.Dynamics.DataEntities;
using ErpDTO.DTO;
using AxConCommon.Extensions;

namespace AxConnect.Modules
{
    public class BomTransfer
    {
        public static void GetBom(string header)
        {
            var bomHeaders = AXServiceConnector.CallOdataEndpoint<BomHeaderDTO>("BillOfMaterialsHeaders", null, header).Result;
            DataAccess.DataWriter.WriteToTable<BomHeaderDTO>(bomHeaders.value.GetDataReader(), "[ax].[BillOfMaterialsHeaders]");

            var bomLines = AXServiceConnector.CallOdataEndpoint<BomLinesDTO>("BillOfMaterialsLines", null, header).Result;
            DataAccess.DataWriter.WriteToTable<BomLinesDTO>(bomLines.value.GetDataReader(), "[ax].[BillOfMaterialsLines]");

            var bomVersion = AXServiceConnector.CallOdataEndpoint<BomVersionDTO>("BillOfMaterialsVersions", null, header).Result;
            DataAccess.DataWriter.WriteToTable<BomVersionDTO>(bomVersion.value.GetDataReader(), "[ax].[BillOfMaterialsVersions]");

        }
    }
}
