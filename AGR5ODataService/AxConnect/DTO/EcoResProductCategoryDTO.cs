using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.DTO
{
    public class EcoResProductCategoryDTO
    {
        public long CategoryHierarchy { get; set; }
        public long Category { get; set; }
        public long Product { get; set; }
        public DateTime ModifiedDateAndTime { get; set; }
    }
}
