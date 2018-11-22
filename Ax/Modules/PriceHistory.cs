using ErpConnector.Ax.DTO;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common;
using ErpConnector.Common.Util;
using ErpConnector.Common.ErpTasks;

namespace ErpConnector.Ax.Modules
{
    public class PriceHistory
    {
        public static AxBaseException GetPriceHistory(int actionId, bool includeBAndM)
        {
            if (includeBAndM)
            {
                var openSalesPriceJournalLines = ServiceConnector.CallOdataEndpoint<OpenSalesPriceJournalLinesDTO>("OpenSalesPriceJournalLines", "", "[ax].[OpenSalesPriceJournalLines]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
                //if (openSalesPriceJournalLines != null)
                //{
                //    return openSalesPriceJournalLines;
                //}

                var openPurchasePriceJournalLines = ServiceConnector.CallOdataEndpoint<OpenPurchasePriceJournalLinesDTO>("OpenPurchasePriceJournalLines", "", "[ax].[OpenPurchasePriceJournalLines]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
                //if (openPurchasePriceJournalLines != null)
                //{
                //    return openPurchasePriceJournalLines;
                //}

                var bonSalesPrice = ServiceConnector.CallOdataEndpoint<BONSalesPriceDTO>("BONSalesPrices", "", "[ax].[SalesPrices]", actionId, Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365)).Result;
                //if (bonSalesPrice != null)
                //{
                //    return bonSalesPrice;
                //}
            }
            return null;
        }
    }
}
