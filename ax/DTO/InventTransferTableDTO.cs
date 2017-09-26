using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class InventTransferTableDTO
    {
        public int AtpApplyDemandTimeFence { get; set; }
        public int AtpApplySupplyTimeFence { get; set; }
        public int AtpBackwardDemandTimeFence { get; set; }
        public int AtpBackwardSupplyTimeFence { get; set; }
        public bool AtpInclPlannedOrders { get; set; }
        public int AtpTimeFence { get; set; }
        public int AutoReservation { get; set; }
        public string CargoDescription_RU { get; set; }
        public string CargoPacking_RU { get; set; }
        public string CarrierCode_RU { get; set; }
        public int CarrierType_RU { get; set; }
        public string CurrencyCode_RU { get; set; }
        public DateTime DeliveryDate_RU { get; set; }
        public int DeliveryDateControlType { get; set; }
        public string DlvModelId { get; set; }
        public string DlvTermId { get; set; }
        public string DriverContact_RU { get; set; }
        public string DriverName_RU { get; set; }
        public string DriverLicenseNum_RU { get; set; }
        public int FreightSlipType { get; set; }
        public string FreightZoneId { get; set; }
        public string FromAddressName { get; set; }
        public long FromContactPerson { get; set; }
        public long FromPostalAddress { get; set; }
        public DateTime IntrastatFulfillmentDate_HU { get; set; }
        public string IntrastatSpecMove_CZ { get; set; }
        public string InventLocationIdFrom { get; set; }
        public string InventLocationIdTo { get; set; }
        public string InventLocationIdTransit { get; set; }
        public string InventProfileId_RU { get; set; }
        public string InventProfileIdTo_RU { get; set; }
        public int InventProfileType_RU { get; set; }
        public int InventProfileUseRelated_RU { get; set; }
        public long LadingPostalAddress_RU { get; set; }
        public string LicenseCardNum_RU { get; set; }
        public string LicenseCardRegNum_RU { get; set; }
        public string LicenseCardSeries_RU { get; set; }
        public int LicenseCardType_RU { get; set; }
        public string PartyAccountNum_RU { get; set; }
        public long PartyAgreementHeaderExt_RU { get; set; }
        public int PdsOverrideFEFO { get; set; }
        public string PortId { get; set; }
        public string PriceGroupId_RU { get; set; }
        public DateTime ReceiveDate { get; set; }
        public long RetailReplenishRefRecId { get; set; }
        public int RetailReplenishRefTableId { get; set; }
        public int RetailRetailStatusType { get; set; }
        public DateTime ShipDate { get; set; }
        public string StatProcId { get; set; }
        public string ToAddressName { get; set; }
        public long ToContactPersion { get; set; }
        public long ToPostalAddress { get; set; }
        public string TransactionCode { get; set; }
        public string TransferId { get; set; }
        public int TransferStatus { get; set; }
        public int TransferType_IN { get; set; }
        public int TransferType_RU { get; set; }
        public string Transport { get; set; }
        public long TransportationDocument { get; set; }
        public string TransportationPayer_RU { get; set; }
        public int TransportationPayerType_RU { get; set; }
        public string TransportationType_RU { get; set; }
        public int TransportInvoiceType_RU { get; set; }
        public int TrPackingSlipAutoNumbering_LT { get; set; }
        public long UnladingPostalAddress_RU { get; set; }
        public string VehicleModel_RU { get; set; }
        public string VehiclePlateNum_RU { get; set; }
        public string WaybillNum_RU { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string DataAreaId { get; set; }
        public int RecVersion { get; set; }
        public long Partition { get; set; }
        public long RecId { get; set; }
    }
}
