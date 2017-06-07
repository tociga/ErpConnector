using System;
using System.Linq;
using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Utils;

namespace ErpConnector.Ax.Modules
{
    public class ProductHistory
    {

        public void WriteInventTrans(string adalHeader)
        {
            Int64 recId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANS]");
            recId = GetNextRecId(recId, "GetNextRecId", adalHeader);
            bool foundData = true;
            while(foundData)
            {
                foundData =  WriteInventTrans<InventTransDTO>(recId, 20000, "GetInventTransLines", "[INVENTTRANS]", adalHeader);
                recId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANS]");
                recId = GetNextRecId(recId, "GetNextRecId", adalHeader);
            }
        }

        public void WriteInventTransOrigin(string adalHeader)
        {
            Int64 recId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSORIGIN]");
            recId = GetNextRecId(recId, "GetNextTransOriginRecId", adalHeader);
            bool foundData = true;
            while(foundData)
            {
                foundData = WriteInventTrans<InventTransOriginDTO>(recId, 20000, "GetInventTransOriginLines", "[INVENTTRANSORIGIN]", adalHeader);
                recId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSORIGIN]");
                recId = GetNextRecId(recId, "GetNextTransOriginRecId", adalHeader);
    }
        }

        private bool WriteInventTrans<T>(Int64 recId, Int64 pageSize, string webMethod, string destTable, string adalHeader)
        {
            ServiceConnector connector = new ServiceConnector();
            string postData = "{ \"minRecId\": " + recId.ToString()+ ", \"maxRecId\" : "+ (recId + pageSize).ToString() +"}";
            var result = ServiceConnector.CallAGRServiceArray<T>("AGRInventTransService", webMethod , postData, adalHeader);

            var reader = result.Result.GetDataReader();

            DataWriter.WriteToTable(reader, "[ax]." + destTable);

            return result.Result.Any();
        }

        public long GetNextRecId(long recId, string webMethod, string adalHeader)
        {
            string postData = "{ \"lastRecId\": " + recId.ToString()+" }";
            var result = ServiceConnector.CallAGRServiceScalar<Int64>("AGRInventTransService", webMethod, postData, adalHeader);

            return result.Result;
        }
    }
}
