using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxConnect;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using AxConCommon.Extensions;
using ErpDTO.DTO;

namespace AxConnect.Modules
{
    public class ProductHistory
    {

        public void WriteInventTrans(string adalHeader)
        {
            Int64 recId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANS]");
            recId = GetNextRecId(recId, "GetNextRecId", adalHeader);
            bool foundData = true;
            while(foundData)
            {
                foundData =  WriteInventTrans<InventTransDTO>(recId, 20000, "GetInventTransLines", "[INVENTTRANS]", adalHeader);
                recId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANS]");
                recId = GetNextRecId(recId, "GetNextRecId", adalHeader);
            }
        }

        public void WriteInventTransOrigin(string adalHeader)
        {
            Int64 recId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSORIGIN]");
            recId = GetNextRecId(recId, "GetNextTransOriginRecId", adalHeader);
            bool foundData = true;
            while(foundData)
            {
                foundData = WriteInventTrans<InventTransOriginDTO>(recId, 20000, "GetInventTransOriginLines", "[INVENTTRANSORIGIN]", adalHeader);
                recId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSORIGIN]");
                recId = GetNextRecId(recId, "GetNextTransOriginRecId", adalHeader);
    }
        }

        private bool WriteInventTrans<T>(Int64 recId, Int64 pageSize, string webMethod, string destTable, string adalHeader)
        {
            AXServiceConnector connector = new AXServiceConnector();
            string postData = "{ \"minRecId\": " + recId.ToString()+ ", \"maxRecId\" : "+ (recId + pageSize).ToString() +"}";
            var result = AXServiceConnector.CallAGRServiceArray<T>("AGRInventTransService", webMethod , postData, adalHeader);

            var reader = result.Result.GetDataReader();

            DataAccess.DataWriter.WriteToTable(reader, "[ax]." + destTable);

            return result.Result.Any();
        }

        public long GetNextRecId(long recId, string webMethod, string adalHeader)
        {
            string postData = "{ \"lastRecId\": " + recId.ToString()+" }";
            var result = AXServiceConnector.CallAGRServiceScalar<Int64>("AGRInventTransService", webMethod, postData, adalHeader);

            return result.Result;
        }
    }
}
