using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.DTO
{
    public class GenericJiraIssueObject<T> : GenericJsonOdata<T>
    {
        public GenericJiraIssueObject()
        {
            base.appendNextLink = true;
        }

        public string expand { get; set; }
        public int startAt { get; set; }
        public int maxResults { get; set; }
        public int total { get; set; }
        [JsonProperty("issues")]
        public override List<T> value { get; set; }
        public override string NextLink
        {
            get
            {
                if (total > (startAt + maxResults))
                {
                    int temp = startAt + maxResults;
                    return "&startAt=" + temp;
                }
                return null;
            }
       }
    }
}
