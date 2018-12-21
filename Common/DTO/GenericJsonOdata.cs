using ErpConnector.Common.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ErpConnector.Common.DTO
{
    public class GenericJsonOdata<T>
    {
        public GenericJsonOdata()
        {
            value = new List<T>();
        }
        public virtual List<T> value { get; set;}

        [JsonProperty("@odata.nextLink")]
        public virtual string NextLink { get; set; }

        public AxBaseException Exception { get; set; }
        public bool appendNextLink { get; set; }

    }
}
