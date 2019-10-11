using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    public class GenericJiraServiceDeskObject<T> : GenericJsonOdata<T>
    {
        public int size { get; set; }
        public int start { get; set; }
        public int limit { get; set; }
        public bool isLastPage { get; set; }
        [JsonProperty("_links")]
        public GenericJiraLinks links { get; set; }
        [JsonProperty("values")]
        public override List<T> value { get; set; }
        public override string NextLink
        {
            get
            {
                if (!isLastPage && links != null)
                {
                    return links.next;
                }
                return null;
            }
        }

    }
}
