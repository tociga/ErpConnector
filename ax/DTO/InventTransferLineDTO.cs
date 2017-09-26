using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class InventTransferLineDTO
    {
        public decimal AmountValue { get; set; }
        public int AtpApplyDemandTimeFence { get; set; }
        public int AtpApplySupplyTimeFence { get; set; }
        public int AtpBackwardDemandTimeFence { get; set; }
        public int AtpBackwardSupplyTimeFence { get; set; }
        public bool AtpInclPlannedOrders { get; set; }
        public int AtpTimeFence { get; set; }
        public int AutoReservation { get; set; }
        public long CombinedTransferOrderDeliv { get; set; }
        public int DeliveryDateControlType { get; set; }
        public string HhtHandheldUserId { get; set; }
        public DateTime HhtTransDate { get; set; }
        public int HhTTransTime { get; set; }
        public DateTime IntraStatFullFillmentDate_HU { get; set; }
        public string IntrastatSpecMoxe_CZ { get; set; }
        public string InventDimId { get; set; }
        public string InventDimIdTo_RU { get; set; }
        public string InventTransId { get; set; }
        public string InventTransIdReceive { get; set; }
        public string InventTransIdScrap { get; set; }
        public string InventTransIdTransitFrom { get; set; }
        public string InventTransIdTransitTo { get; set; }
        public string ItemId { get; set; }
        public decimal LineAmount_RU { get; set; }
        public decimal LineNum { get; set; }
        public string OrigCountryRegionId { get; set; }
        public string OrigCountId { get; set; }
        public string OrigStateId { get; set; }
        public decimal OverDeliveryPct { get; set; }
        public decimal PdsCWQtyReceived { get; set; }
        public decimal PdsCWQtyReceiveNow { get; set; }
        public decimal PdsCWQtyRemainReceive { get; set; }
        public decimal PdsCWQtyRemainShip { get; set; }
        public decimal PdsCWQtyScrapped { get; set; }
        public decimal PdsCWQtyShipNow { get; set; }
        public decimal PdsCWQtyShipped { get; set; }
        public decimal PdsCWQtyTransfer { get; set; }
        public int PdsOverrideFEFO { get; set; }
        public string Port { get; set; }
        public decimal Price_RU { get; set; }
        public decimal PriceUnit_RU { get; set; }
        public decimal QtyReceived { get; set; }
        public decimal QtyReceivedNow { get; set; }
        public decimal QtyRemain { get; set; }
        public decimal QtyRemainShip { get; set; }
        public decimal QtyScrapped { get; set; }
        public decimal QtyShipNow { get; set; }
        public decimal QtyShipped { get; set; }
        public decimal QtyTransfer { get; set; }
        public DateTime ReceiveDate { get; set; }
        public int RemainStatus { get; set; }
        public string RetailAreaId { get; set; }
        public string RetailInfocodeIdEx2 { get; set; }
        public string RetailInformationSubcodeIdEx2 { get; set; }
        public long RetailReplenishRefRecId { get; set; }
        public int RetailReplenishRefTableId { get; set; }
        public DateTime ShipDate { get; set; }
        public long StatisticalValue { get; set; }
        public string StatProcId { get; set; }
        public string TransactionCodeId { get; set; }
        public string TransferId { get; set; }
        public string Transport { get; set; }
        public decimal UnderDeliveryPct { get; set; }
        public string UnitId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string DataAreaId { get; set; }
        public int RecVersion { get; set; }
        public long Partition { get; set; }
        public long RecId { get; set; }
        public long IntrastatCommodity { get; set; }
    }
}
