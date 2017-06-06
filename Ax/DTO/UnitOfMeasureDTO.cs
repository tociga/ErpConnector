using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;

namespace ErpConnector.Ax.DTO
{
    public class UnitOfMeasureDTO
    {
        public long RecId { get; set; }
        public long Partition { get; set; }
        public int DecimalPrecision { get; set; }
        public NoYes IsBaseUnit
        {
            get; set;
        }
        public NoYes IsSystemUnit
        {
            get; set;
        }
        public string Symbol { get; set; }
        public int SystemOfUnits { get; set; }
        public int UnitOfMeasureClass { get; set; }
    }
}
