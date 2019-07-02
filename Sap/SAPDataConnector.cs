using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpConnector.Common;
using ErpConnector.Common.AGREntities;
using ErpConnector.Common.Exceptions;

namespace ErpConnector.Sap
{
    public class SAPDataConnector : ErpGenericConnector
    {
        public override AxBaseException CreatePoTo(List<POTOCreate> po_to_create, int actionId)
        {
            return base.CreatePoTo(po_to_create, actionId);
        }
    }
}
