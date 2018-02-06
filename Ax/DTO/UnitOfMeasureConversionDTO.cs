namespace ErpConnector.Ax.DTO
{
    public class UnitOfMeasureConversionDTO
    {
        public long RecId { get; set; }
        public long Partition { get; set; }
        public int Denominator { get; set; }
        public decimal Factor { get; set; }
        public long FromUnitOfMeasure { get; set; }
        public decimal InnerOffset { get; set; }
        public int Numerator { get; set; }
        public decimal OuterOffset { get; set; }
        public long Product { get; set; }
        public int Rounding { get; set; }
        public long ToUnitOfMeasure { get; set; }
    }
}
