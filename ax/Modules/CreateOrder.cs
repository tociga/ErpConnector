using ErpConnector.Ax.Authentication;
using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.Modules
{
    public class CreateOrder : AXODataContextConnector
    {
        public CreateOrder(OAuthHelper oAuthenticationHelper, LogMessage logMessageHandler, bool enableCrossCompany) : base(oAuthenticationHelper, logMessageHandler, enableCrossCompany)
        {
        }
        protected override bool CreateRecords(string targetAXLegalEntity, System.Collections.ArrayList dataFile)
        {
            bool ret = false;

            foreach (AGROrderDTO agrOrder in dataFile)
            {
                if (!orderExists(agrOrder.ARGId, targetAXLegalEntity))
                {
                    this.CreateOrderRecord(agrOrder, targetAXLegalEntity);
                    ret = true;
                }
                else
                {
                    // <update> 
                    this.UpdateArgOrderRecord(agrOrder, targetAXLegalEntity);

                    ret = true;
                }
            }

            return ret;
        }
        private bool orderExists(string custID, string targetAXLegalEntity)
        {
            var query = from entity in this.context.AGROrders
                        where entity.ARGId == custID
                         && entity.DataAreaId == targetAXLegalEntity
                        select entity;

            return query.Count() > 0;
        }

        private void CreateOrderRecord(AGROrderDTO argOrder, string targetAXLegalEntity)
        {
            AGROrder order = context.CreateTrackedEntityInstance<AGROrder>();
            order.ARGId = argOrder.ARGId; // "Firsthh";
            order.OrderFrom = argOrder.OrderFrom; // "1004"; //vendor number;
            order.OrderTo = argOrder.OrderTo; // "DC"; //warehouse;
            order.OrderType = argOrder.OrderType; // AGROrderType.PO;
            order.ReceiveDate = argOrder.ReceiveDate; // DateTime.Now.Date.AddDays(20);
            order.OrderStatus = argOrder.OrderStatus; // AGROrderStatus.Created;
            order.DataAreaId = targetAXLegalEntity;

            foreach (AGROrderLineDTO agrOrderLine in argOrder.ArgOrderLine)
            {
                AGROrderLine line = context.CreateTrackedEntityInstance<AGROrderLine>();
                line.AGRId = order.ARGId;
                line.Color = agrOrderLine.Color;
                line.Config = agrOrderLine.Config;
                line.ItemId = agrOrderLine.ItemId;
                line.LineNum = agrOrderLine.LineNum;
                line.Qty = agrOrderLine.Qty;
                line.Size = agrOrderLine.Size;
                line.Style = agrOrderLine.Style;
                line.OrderTo = agrOrderLine.OrderTo;
                line.DataAreaId = targetAXLegalEntity;
            }

            logMessageHandler(string.Format("Created distinct AGROrder '{0}' in company '{1}'.", argOrder.ARGId, targetAXLegalEntity));
        }

        private void UpdateArgOrderRecord(AGROrderDTO argOrder, string targetAXLegalEntity)
        {
            // update customer
            AGROrder order;

            var query = from entity in context.AGROrders
                        where entity.ARGId == argOrder.ARGId && entity.DataAreaId == targetAXLegalEntity
                        select entity;

            var ie = query.GetEnumerator();
            ie.MoveNext();

            order = ie.Current;

            // use tracned entity to only update change fields
            context.TrackEntityInstance(order);

            order.OrderStatus = argOrder.OrderStatus;

            logMessageHandler(string.Format("Update AGROrder '{0}' in company '{1}'.", order.ARGId, targetAXLegalEntity));
        }

    }

}
