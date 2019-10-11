namespace ErpConnector.Common.Constants
{
    public static class AGRConstants
    {
        public enum AGR_ORDER_TYPE { PURCHASE_ORDER = 0, TRANSFER_ORDER};
        public enum AGR_LOCATION_TYPE { WAREHOUSE = 0, STORE};
        public enum ERP_ORDER_STATUS { IN_PROGRESS = 0, COMPLETED, ORDER_CREATION_ERROR  }
        public enum SYNC_TASK { START = 0, END, ERP_SIDE_ERROR, ABORTED }
    }
}
