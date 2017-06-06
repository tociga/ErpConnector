using System;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;

namespace ErpConnector.Ax.DTO
{
    public class RetailAssortmentDTO
    {
        RetailAssortmentStatusType _status; 
        public string AssortmentID { get; set; }
        public string Name { get; set; }
        public DateTime PublishedDateTime { get; set; }
        
        public object Status
        {
            get { return (int)_status; }
            set
            {
                _status = DTOUtil.GetEnumFromObj<RetailAssortmentStatusType>(value, RetailAssortmentStatusType.Draft);
            }
        }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public long PublicRecId { get; set; }
        public long PublicPartition { get; set; }
    }
}
