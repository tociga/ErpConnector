using ErpConnector.Common.Constants;

namespace ErpConnector.Common.DTO
{
    public class SyncDTO
    {
        public int? id { get; set; }
        public AGRConstants.SYNC_TASK status { get; set; }
        public string message { get; set; }
    }
}
