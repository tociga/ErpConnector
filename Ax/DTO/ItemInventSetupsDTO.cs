using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;


namespace ErpConnector.Ax.DTO
{
    public class ItemInventSetupsDTO
    {
        private NoYes _stopped;
        private NoYes _calendarDays;
        private NoYes _mandatoryInventSite;
        private NoYes _mandatoryInventLocation;
        private NoYes _override;
        private SalesDeliveryDateControlType _deliverDateControlType;
        public string ItemId { get; set; }
        public string InventDimId { get; set; }
        public string dataAreaId
        {
            get; set;
        }
        public string InventDimIdDefault { get; set; }
        public decimal MultipleQty { get; set; }
        public decimal LowestQty { get; set; }
        public decimal HighestQty { get; set; }
        public decimal StandardQty { get; set; }
        public int LeadTime { get; set; }
        public object Override
        {
            get
            {
                return (int)_override;
            }
            set
            {
                _override = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        public object Stopped
        {
            get
            {
                return (int)_stopped;
            }
            set
            {
                _stopped = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
            }
        }
        //public int ATPBackwardDemandTimeFence { get; set; }
        //public object CalendarDays
        //{
        //    get
        //    {
        //        return (int)_calendarDays;
        //    }
        //    set
        //    {
        //        _calendarDays = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
        //    }
        //}
        //public object MandatoryInventSite
        //{
        //    get
        //    {
        //        return (int)_mandatoryInventSite;
        //    }
        //    set
        //    {
               
        //        _mandatoryInventSite = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);

        //    }
        //}
        //public int ATPBackwardSupplyTimeFence { get; set; }
        //public object MandatoryInventLocation
        //{
        //    get
        //    {
        //        return (int)_mandatoryInventLocation;
        //    }
        //    set
        //    {
        //        _mandatoryInventLocation = DTOUtil.GetEnumFromObj<NoYes>(value, NoYes.No);
        //    }
        //}
        //public int ATPTimeFence { get; set; }       
        //public int ATPApplyDemandTimeFence { get; set; }
        //public object DeliveryDateControlType
        //{
        //    get
        //    {
        //        return (int)_deliverDateControlType;
        //    }
        //    set
        //    {
        //        _deliverDateControlType = DTOUtil.GetEnumFromObj<SalesDeliveryDateControlType>(value, SalesDeliveryDateControlType.None);
        //    }
        //}
        //public bool ATPInclPlannedOrders { get; set; }
        //public object ATPApplySupplyTimeFence { get; set; }

    }
}
