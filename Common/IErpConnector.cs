namespace ErpConnector.Common
{
    public enum ErpType
    {
        ax,
        sap
    }
    public interface IErpConnector
    {
        void RunTransfer();
        void GetPoTo();
        string GetDBScript(string entity);
        void GetBom();
    }
}
