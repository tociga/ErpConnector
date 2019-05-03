using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpConnector.Common.DTO;
using Newtonsoft.Json;

namespace ErpConnector.Common.DTO
{
    public class GenericJiraObjectList<T> : GenericJsonOdata<T>
    {
        public GenericJiraObjectList() : base()
        { }
        public string self { get; set; }
       
        public override string NextLink
        {
            get
            {
                if (metadata != null)
                {
                    return metadata.next;
                }
                return null;
            }
        }
        [JsonProperty("results")]
        public override List<T> value { get; set; }

        public JiraObjectMetaData metadata { get; set; }
    }
}
