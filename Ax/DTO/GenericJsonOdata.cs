﻿using ErpConnector.Common.Exceptions;
using Newtonsoft.Json;
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

        [JsonProperty("@odata.nextLink")]
        public string NextLink { get; set; }

        public AxBaseException Exception { get; set; }

    }
}
