using ErpConnector.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common
{
    public class ErpTaskCompletedArgs : EventArgs
    {
        public AxBaseException Exception { get; set; }
        public int ActionId { get; set; }
        public int Status { get; set; }
    }
}
