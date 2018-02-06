using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpDTO.DTO
{
    public class PriceDiscTableDTO
    {
        public long RecId { get; set; }
        public string DataAreaId { get; set; }
        public long Partition { get; set; }
        public int RecVersion { get; set; }
        public int AccountCode { get; set; }
        public string AccountRelation { get; set; }
        public string Agreement { get; set; }
        public int AgreementHeaderExt_RU { get; set; }
        public int AllocateMarkup { get; set; }
        public decimal Amount { get; set; }
        public int CalendarDays { get; set; }
        public string Currency { get; set; }
        public decimal DeliveryTime { get; set; }
        public int DisregardLeadTime { get; set; }
        public DateTime FromDate { get; set; }
        public int GenericCurrency { get; set; }
        public int InventBaileeFreeDays_RU { get; set; }
        public string InventDimId { get; set; }
        public long ItemCode { get; set; }
        public string ItemRelation { get; set; }
        public decimal Markup { get; set; }

        public decimal MaximumRetailPrice_IN { get; set; }
        public decimal MCRFixedAmountCur { get; set; }
        public string MCRMerchandisingEventID { get; set; }
        public int MCRPriceDiscGroupType { get; set; }
        public int Module { get; set; }
        public long OriginalPriceDicAdmTransRecId { get; set; }
        public string PDSCalculationId { get; set; }
        public decimal Percent1 { get; set; }
        public decimal Percent2 { get; set; }
        public decimal PriceUnit { get; set; }
        public decimal QuantityAmountFrom { get; set; }
        public decimal QuantityAmountTo { get; set; }
        public int Relation { get; set; }
        public int SearchAgain { get; set; }
        public DateTime ToDate { get; set; }
        public string UnitId { get; set; }
    }
}
