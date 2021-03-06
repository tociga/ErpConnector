﻿using System;
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
            var bomHeaders = AXServiceConnector.CallOdataEndpoint<BillOfMaterialsHeader>("BillOfMaterialsHeaders", null, header).Result;
            DataAccess.DataWriter.WriteToTable<BillOfMaterialsHeader>(bomHeaders.value.GetDataReader(), "[ax].[BillOfMaterialsHeaders]");

            var bomLines = AXServiceConnector.CallOdataEndpoint<BillOfMaterialsLine>("BillOfMaterialsLines", null, header).Result;
            DataAccess.DataWriter.WriteToTable<BillOfMaterialsLine>(bomLines.value.GetDataReader(), "[ax].[BillOfMaterialsLines]");

            var bomVersion = AXServiceConnector.CallOdataEndpoint<BillOfMaterialsVersion>("BillOfMaterialsVersions", null, header).Result;
            DataAccess.DataWriter.WriteToTable<BillOfMaterialsVersion>(bomVersion.value.GetDataReader(), "[ax].[BillOfMaterialsVersions]");

        }
    }
}
