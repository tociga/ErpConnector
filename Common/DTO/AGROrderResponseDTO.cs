using System.Runtime.Serialization;
using ErpConnector.Common.Constants;

namespace ErpConnector.Common.DTO
{
    [DataContract]
    public class AGROrderResponseDTO
    {
        [DataMember]
        public int agr_order_id { get; set; }
        // 0 = purchase order, 1 = transfer order
        [DataMember]
        public AGRConstants.AGR_ORDER_TYPE agr_order_type { get; set; }        
        [DataMember]
        public AGRConstants.ERP_ORDER_STATUS erp_order_status { get; set; }
        [DataMember]
        public string error_message { get; set; }
        [DataMember]
        public string error_stack_trace { get; set; }
        [DataMember]
        public string erp_order_no { get; set; }
    
    }
}
