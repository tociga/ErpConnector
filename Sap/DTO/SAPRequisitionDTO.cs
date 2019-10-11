using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Sap.DTO
{
    public class SAPRequisitionDTO
    {
        public string CreatedBy { get; set; }
        public string DocType { get; set; }
        public string PreqName { get; set; }
        public string ShortText { get; set; }
        public string Material { get; set; }
        public string Plant { get; set; }
        public string StoreLoc { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public string DelivDate { get; set; }
        public decimal GrPrTime { get; set; }
        public decimal CAmtBapi { get; set; }
        public decimal PriceUnit { get; set; }

    }
}
