using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class SeasonTableDTO
    {
        public string SeasonCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string KRFRetailSeasonGroupId { get; set; }
        public string Description { get; set; }
        public string DataAreaId { get; set; }
    }
}
