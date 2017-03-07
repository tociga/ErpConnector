using AxConnect.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxConnect.DTO
{
    public class UnitOfMeasureDTO
    {
        private NoYes _isBaseUnit;
        private NoYes _isSystemUnit;

        public long RecId { get; set; }
        public long Partition { get; set; }
        public int DecimalPrecision { get; set; }
        public object IsBaseUnit
        {
            get
            {
                return (int)_isBaseUnit;
            }
            set
            {
                DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public object IsSystemUnit
        {
            get { return (int)_isSystemUnit; }
            set { DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No); }
        }
        public string Symbol { get; set; }
        public int SystemOfUnits { get; set; }
        public int UnitOfMeasureClass { get; set; }
    }
}
