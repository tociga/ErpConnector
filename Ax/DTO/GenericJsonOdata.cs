using System.Collections.Generic;

namespace ErpConnector.Ax.DTO
{
    public class GenericJsonOdata<T>
    {
        public List<T> value { get; set;}
    }
}
