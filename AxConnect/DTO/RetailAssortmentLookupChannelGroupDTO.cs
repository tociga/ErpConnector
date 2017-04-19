using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.DTO
{
    public class RetailAssortmentLookupChannelGroupDTO
    {
        public long AssortmentId { get; set; }
        public long OMOperatingUnitId { get; set; }
        public long RetailChannelTable { get; set; }
        public int Recversion { get; set; }
        public long Partition { get; set; }
        public long RecId { get; set; }
    }
}
