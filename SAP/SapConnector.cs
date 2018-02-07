using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpConnector.Common;
using ErpConnector.Common.AGREntities;
using ErpConnector.Common.Exceptions;

namespace SAP
{
    public class SapConnector : IErpConnector
    {
        public ErpConnectorException CreateItems(List<ItemToCreate> itemsToCreate, int actionId)
        {
            throw new NotImplementedException();
        }

        public ErpConnectorException CreatePoTo(List<POTOCreate> po_to_create, int actionId)
        {
            throw new NotImplementedException();
        }

        public ErpConnectorException DailyRefresh(DateTime date, int actionId)
        {
            throw new NotImplementedException();
        }

        public ErpConnectorException FullTransfer(int actionId)
        {
            throw new NotImplementedException();
        }

        public ErpConnectorException PimFull(int actionId)
        {
            throw new NotImplementedException();
        }

        public ErpConnectorException TransactionFull(int actionId)
        {
            throw new NotImplementedException();
        }

        public ErpConnectorException TransactionRefresh(DateTime date, int actionId)
        {
            throw new NotImplementedException();
        }

        public ErpConnectorException UpdateProduct(int actionId)
        {
            throw new NotImplementedException();
        }
    }
}
