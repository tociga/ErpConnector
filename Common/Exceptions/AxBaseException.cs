﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.Exceptions
{
    public class AxBaseException : ErpConnectorException
    {
        public AxWebExceptions error { get; set; }

        public Exception ApplicationException { get; set; }
    }
}
