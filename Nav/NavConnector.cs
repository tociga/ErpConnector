﻿using ErpConnector.Common;
using ErpConnector.Common.AGREntities;
using ErpConnector.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Nav
{
    public class NavConnector : ErpGenericConnector
    {
        public override int CreatePoTo(List<POTOCreate> po_to_create, int actionId)
        {
            return base.CreatePoTo(po_to_create, actionId);
        }
    }
}
