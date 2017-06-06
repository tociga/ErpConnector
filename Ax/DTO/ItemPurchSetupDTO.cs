using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;

namespace ErpConnector.Ax.DTO
{
    public class ItemPurchSetupDTO
    {
        private NoYes _stopped;
        private NoYes _calendarDays;
        private NoYes _mandatoryInventSite;
        private NoYes _mandatoryInventLocation;
        private NoYes _override;

        public object Stopped
        {
            get
            {
                
                return (int)_stopped;
            }
            set
            {
                if (value is NoYes)
                {
                    _stopped = (NoYes)value;
                }
                else
                {
                    _stopped = NoYes.No;
                }
            }

        }
        public decimal MultipleQty { get; set; }
        public decimal HighestQty { get; set; }
        public string InventDimIdDefault { get; set; }
        public decimal StandardQty { get; set; }
        public decimal LowestQty { get; set; }
        public object CalendarDays
        {
            get
            {
                return (int)_calendarDays;
            }
            set
            {
                if (value is NoYes)
                {
                    _calendarDays = (NoYes)value;
                }
                else
                {
                    _calendarDays = NoYes.No;
                }
            }
        }
        public object MandatoryInventSite
        {
            get
            {
                return (int)_mandatoryInventSite;
            }
            set
            {
                if (value is NoYes)
                {
                    _mandatoryInventSite = (NoYes)value;
                }
                else
                {
                    _mandatoryInventSite = NoYes.No;
                }
            }
        }
        public string dataAreaId { get; set; }
        public object MandatoryInventLocation
        {
            get
            {
                return (int)_mandatoryInventLocation;
            }
            set
            {
                if (value is NoYes)
                {
                    _mandatoryInventLocation = (NoYes)value;
                }
                else
                {
                    _mandatoryInventLocation = NoYes.No;
                }
            }
        }
        public string ItemId { get; set; }
        public int LeadTime { get; set; }
        public object Override
        {
            get
            {
                return (int)_override;
            }
            set
            {
                if (value is NoYes)
                {
                    _override = (NoYes)value;
                }
                else
                {
                    _override = NoYes.No;
                }
            }
        }
        public string InventDimId { get; set; }
    }
}
