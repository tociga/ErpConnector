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

        public void WriteInventTrans()
        {
            Int64 recId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANS]");            
            bool foundData = true;
            while(foundData)
            {
                foundData =  WriteInventTrans<InventTransDTO>(recId, 5000, "GetInventTrans", "[INVENTTRANS]");
                recId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANS]");                
            }
        }

        public void WriteInventTransOrigin()
        {
            Int64 recId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSORIGIN]");
            bool foundData = true;
            while(foundData)
            {
                foundData = WriteInventTrans<InventTransOriginDTO>(recId, 5000, "GetInventTransOrigin", "[INVENTTRANSORIGIN]");
                recId = DataAccess.DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSORIGIN]");
    }
        }

        private bool WriteInventTrans<T>(Int64 recId, Int64 pageSize, string webMethod, string destTable)
        {
            AXServiceConnector connector = new AXServiceConnector();
            string postData = "{ \"lastRecId\": " + recId.ToString()+ ", \"pageSize\" : "+ (pageSize).ToString() +"}";
            var result = AXServiceConnector.CallAGRServiceArray<T>("AGRInventTransService", webMethod , postData, null);

            var reader = result.Result.GetDataReader();

            DataAccess.DataWriter.WriteToTable(reader, "[ax]." + destTable);

            return result.Result.Any();
        }      
    }
}
