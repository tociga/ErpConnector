using System;

namespace ErpConnector.Ax.DTO
{
    public class RetailAssortmentLookupDTO
    {
        public long AssortmentId { get; set; }
        public int LineType { get; set; }
        public long ProductId { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public long VariantId { get; set; }
        public int Recversion { get; set; }
        public long Partition { get; set; }
        public long RecId { get; set; }
    }
}
