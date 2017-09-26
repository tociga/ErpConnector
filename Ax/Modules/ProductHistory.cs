using System;
using System.Linq;
using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Utils;
using System.Text;

namespace ErpConnector.Ax.Modules
{
    public class ProductHistory
    {

        public void WriteInventTrans()
        {
            Int64 recId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANS]");            
            bool foundData = true;
            while(foundData)
            {
                foundData =  WriteFromService<InventTransDTO>(recId, 5000, "GetInventTrans", "AGRInventTransService", "[INVENTTRANS]", DateTime.MinValue);
                recId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANS]");                
            }
        }

        public void WriteInventTransOrigin()
        {
            Int64 recId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSORIGIN]");
            bool foundData = true;
            while(foundData)
            {
                foundData = WriteFromService<InventTransOriginDTO>(recId, 5000, "GetInventTransOrigin", "AGRInventTransService", "[INVENTTRANSORIGIN]", DateTime.MinValue);
                recId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSORIGIN]");
    }
        }

        private bool WriteFromService<T>(Int64 recId, Int64 pageSize, string webMethod, string serviceName, string destTable, DateTime minDate, bool useDate = false)
        {
            StringBuilder sb = new StringBuilder();
            if (useDate)
            {
                sb.Append("{\"firstDate\" : \"" + minDate.ToString("yyyy-MM-dd HH:mm:ss") + "\"");
                sb.Append(", \"lastDate\" : \"" + minDate.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") +"\"}");
            }
            else
            {
                sb.Append("{ \"lastRecId\": " + recId.ToString() + ", \"pageSize\" : " + (pageSize).ToString() + " }");
            }
            var result = ServiceConnector.CallAGRServiceArray<T>(serviceName, webMethod , sb.ToString(), null);

            var reader = result.Result.value.GetDataReader();

            DataWriter.WriteToTable<T>(reader, "[ax]." + destTable);

            return result.Result.value.Any();
        }



        public void WriteInventSumFull()
        {
            Int64 recId = DataWriter.GetMaxRecId("[ax]", "[INVENTSUM]");
            bool foundData = true;
            while (foundData)
            {
                foundData = WriteFromService<InventSumDTO>(recId, 5000, "GetInventSum", "AGRItemCustomService", "[INVENTSUM]", DateTime.MinValue, false);
                recId = DataWriter.GetMaxRecId("[ax]", "[INVENTSUM]");
            }

        }

        public void WriteInventSumRefresh(DateTime minDate)
        {
            for (DateTime d = minDate.Date; d <= DateTime.Now.Date; d = d.AddDays(1))
            {
                WriteFromService<InventSumDTO>(0, 5000, "GetInventSumByDate", "AGRItemCustomService", "[INVENTSUM_Increment]", d, true);
            }

        }

        public void WriteInventTransRefresh(DateTime minDate)
        {
            for (DateTime d = minDate.Date; d <= DateTime.Now.Date; d = d.AddDays(1))
            {
                WriteFromService<InventTransDTO>(0, 5000, "GetInventTransByDate", "AGRInventTransService", "[INVENTTRANS_Increment]", d, true);
            }
        }
    }
}
