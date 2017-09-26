using ErpConnector.Common.Exceptions;
using System.Collections.Generic;

namespace ErpConnector.Ax.DTO
{
    public class GenericJsonOdata<T>
    {
        public GenericJsonOdata()
        {
            value = new List<T>();
        }
        public List<T> value { get; set;}

        public AxBaseException Exception { get; set; }
    }
}
