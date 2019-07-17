using ErpConnector.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    public class GenericWriteObject<T>
    {
        public T WriteObject { get; set; }
        public AxBaseException Exception { get; set; }
        public string SimpleResult { get; set; }
    }
}
