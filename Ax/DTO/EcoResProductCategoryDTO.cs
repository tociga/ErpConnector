using System;

namespace ErpConnector.Ax.DTO
{
    public class EcoResProductCategoryDTO
    {
        public long CategoryHierarchy { get; set; }
        public long Category { get; set; }
        public long Product { get; set; }
        public DateTime ModifiedDateAndTime { get; set; }
    }
}
