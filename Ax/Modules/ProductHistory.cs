using System;
using System.Linq;
using ErpConnector.Ax.DTO;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common;
using ErpConnector.Common.Util;
using ErpConnector.Common.ErpTasks;

namespace ErpConnector.Ax.Modules
{
    public class ProductHistory
    {
        private int ActionId { get; set; }
        public ProductHistory(int actionId)
        {
            ActionId = actionId;
        }

        public AxBaseException WriteInventTrans()
        {
            return ServiceConnector.CallService<InventTransDTO>(ActionId, "GetInventTrans", "AGRInventTransService", "[ax].[INVENTTRANS]", 5000,  Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365));
        }

        public AxBaseException WriteInventTransOrigin()
        {
            return ServiceConnector.CallService<InventTransOriginDTO>(ActionId, "GetInventTransOrigin", "AGRInventTransService", "[ax].[INVENTTRANSORIGIN]", 5000,  Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365));
        }

        public AxBaseException WriteInventSumFull()
        {
            return ServiceConnector.CallService<InventSumDTO>(ActionId, "GetInventSum", "AGRItemCustomService", "[ax].[INVENTSUM]", 5000,  Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365));
        }

        public AxBaseException WriteInventSumRefresh(DateTime minDate)
        {
            return ServiceConnector.CallServiceByDate<InventSumDTO>(minDate, ActionId, "GetInventSumByDate", "AGRItemCustomService", "[ax].[INVENTSUM_Increment]",  Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365));
        }

        public AxBaseException WriteInventTransRefresh(DateTime minDate)
        {
            return ServiceConnector.CallServiceByDate<InventTransDTO>(minDate, ActionId, "GetInventTransByDate", "AGRInventTransService", "[ax].[INVENTTRANS_Increment]",  Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365));
        }
    }
}
