using System;
using System.Linq;
using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Utils;
using System.Text;

namespace ErpConnector.Ax.Modules
{
    public class ProductHistory
    {
        private int ActionId { get; set; }
        public ProductHistory(int actionId)
        {
            ActionId = actionId;
        }

        public void WriteInventTrans()
        {
            DateTime startTime = DateTime.Now;
            try
            {
                Int64 recId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANS]");
                bool foundData = true;
                while (foundData)
                {
                    foundData = ServiceConnector.WriteFromService<InventTransDTO>(recId, 5000, "GetInventTrans", "AGRInventTransService", "[INVENTTRANS]", DateTime.MinValue);
                    recId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANS]");
                }
                DataWriter.LogErpActionStep(ActionId, "[ax].[INVENTTRANS]", startTime, true);
            }
            catch(Exception)
            {
                DataWriter.LogErpActionStep(ActionId, "[ax].[INVENTTRANS]", startTime, false);
                throw;
            }
        }

        public void WriteInventTransOrigin()
        {
            DateTime startTime = DateTime.Now;
            try
            { 
                Int64 recId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSORIGIN]");
                bool foundData = true;
                while(foundData)
                {
                    foundData = ServiceConnector.WriteFromService<InventTransOriginDTO>(recId, 5000, "GetInventTransOrigin", "AGRInventTransService", "[INVENTTRANSORIGIN]", DateTime.MinValue);
                    recId = DataWriter.GetMaxRecId("[ax]", "[INVENTTRANSORIGIN]");
                }
                DataWriter.LogErpActionStep(ActionId, "[ax].[INVENTTRANSORIGIN]", startTime, true);
            }
            catch (Exception)
            {
                DataWriter.LogErpActionStep(ActionId, "[ax].[INVENTTRANSORIGIN]", startTime, false);
                throw;
            }
        }


        public void WriteInventSumFull()
        {
            DateTime startTime = DateTime.Now;
            try
            { 
                Int64 recId = DataWriter.GetMaxRecId("[ax]", "[INVENTSUM]");
                bool foundData = true;
                while (foundData)
                {
                    foundData = ServiceConnector.WriteFromService<InventSumDTO>(recId, 5000, "GetInventSum", "AGRItemCustomService", "[INVENTSUM]", DateTime.MinValue, false);
                    recId = DataWriter.GetMaxRecId("[ax]", "[INVENTSUM]");
                }
                DataWriter.LogErpActionStep(ActionId, "[ax].[INVENTSUM]", startTime, true);
            }
            catch (Exception)
            {
                DataWriter.LogErpActionStep(ActionId, "[ax].[INVENTSUM]", startTime, false);
                throw;
            }

        }

        public void WriteInventSumRefresh(DateTime minDate)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                for (DateTime d = minDate.Date; d <= DateTime.Now.Date; d = d.AddDays(1))
                {
                    ServiceConnector.WriteFromService<InventSumDTO>(0, 5000, "GetInventSumByDate", "AGRItemCustomService", "[INVENTSUM_Increment]", d, true);
                }
                DataWriter.LogErpActionStep(ActionId, "[ax].[INVENTSUM_Increment]", startTime, true);
            }
            catch (Exception)
            {
                DataWriter.LogErpActionStep(ActionId, "[ax].[INVENTSUM_Increment]", startTime, false);
                throw;
            }

        }

        public void WriteInventTransRefresh(DateTime minDate)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                for (DateTime d = minDate.Date; d <= DateTime.Now.Date; d = d.AddDays(1))
                {
                    ServiceConnector.WriteFromService<InventTransDTO>(0, 5000, "GetInventTransByDate", "AGRInventTransService", "[INVENTTRANS_Increment]", d, true);
                }
                DataWriter.LogErpActionStep(ActionId, "[ax].[INVENTTRANS_Increment]", startTime, true);
            }
            catch (Exception)
            {
                DataWriter.LogErpActionStep(ActionId, "[ax].[INVENTTRANS_Increment]", startTime, false);
                throw;
            }

        }
    }
}
