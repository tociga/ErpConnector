using ErpConnector.Ax.DTO;
using ErpConnector.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.Modules
{
    public class PriceHistory
    {
        public static AxBaseException GetPriceHistory(int actionId, bool includeBAndM)
        {
            if (includeBAndM)
            {
                var openSalesPriceJournalLines = ServiceConnector.CallOdataEndpoint<OpenSalesPriceJournalLinesDTO>("OpenSalesPriceJournalLines", "", "[ax].[OpenSalesPriceJournalLines]", actionId).Result;
                if (openSalesPriceJournalLines != null)
                {
                    return openSalesPriceJournalLines;
                }

                var openPurchasePriceJournalLines = ServiceConnector.CallOdataEndpoint<OpenPurchasePriceJournalLinesDTO>("OpenPurchasePriceJournalLines", "", "[ax].[OpenPurchasePriceJournalLines]", actionId).Result;
                if (openPurchasePriceJournalLines != null)
                {
                    return openPurchasePriceJournalLines;
                }

                var bonSalesPrice = ServiceConnector.CallOdataEndpoint<BONSalesPriceDTO>("BONSalesPrices", "", "[ax].[SalesPrices]", actionId).Result;
                if (bonSalesPrice != null)
                {
                    return bonSalesPrice;
                }
            }
            return null;
        }
    }
}
