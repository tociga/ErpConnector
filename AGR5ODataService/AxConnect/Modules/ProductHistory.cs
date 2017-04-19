using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxConnect;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using AxConCommon.Extensions;
using AxConnect.DTO;

namespace AxConnect.Modules
{
    public class ProductHistory
    {

        public void WriteInventTrans()
        {
            Int64 recId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANS]");
            recId = GetNextRecId(recId, "GetNextRecId");
            bool foundData = true;
            while(foundData)
            {
                foundData =  WriteInventTrans<InventTransDTO>(recId, 20000, "GetInventTransLines", "[INVENTTRANS]");
                recId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANS]");
                recId = GetNextRecId(recId, "GetNextRecId");
            }
        }

        public void WriteInventTransOrigin()
        {
            Int64 recId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSORIGIN]");
            recId = GetNextRecId(recId, "GetNextTransOriginRecId");
            bool foundData = true;
            while(foundData)
            {
                foundData = WriteInventTrans<InventTransOriginDTO>(recId, 20000, "GetInventTransOriginLines", "[INVENTTRANSORIGIN]");
                recId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSORIGIN]");
                recId = GetNextRecId(recId, "GetNextTransOriginRecId");       
    }
        }

        private bool WriteInventTrans<T>(Int64 recId, Int64 pageSize, string webMethod, string destTable)
        {
            AXServiceConnector connector = new AXServiceConnector();
            string postData = "{ \"minRecId\": " + recId.ToString()+ ", \"maxRecId\" : "+ (recId + pageSize).ToString() +"}";
            var result = AXServiceConnector.CallAGRServiceArray<T>("AGRInventTransService", webMethod , postData);

            var reader = result.Result.GetDataReader();

            DataAccess.DataWriter.WriteToTable(reader, "[ax]." + destTable);

            return result.Result.Any();
        }

        public long GetNextRecId(long recId, string webMethod)
        {
            string postData = "{ \"lastRecId\": " + recId.ToString()+" }";
            var result = AXServiceConnector.CallAGRServiceScalar<Int64>("AGRInventTransService", webMethod, postData);

            return result.Result;
        }
    }
}
