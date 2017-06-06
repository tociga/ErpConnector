using ErpConnector.Ax;
using ErpConnector.Common;
using System;

namespace ErpConnector.Listener
{
    public class GenericConnector
    {
        IErpConnector factory;
        public GenericConnector(string typeOfErp)
        {
            ErpType erpType;
            Enum.TryParse(typeOfErp, out erpType);
            switch (erpType)
            {
                case ErpType.ax:
                    factory = new AxODataConnector();
                    break;
                case ErpType.sap:
                    break;
                default:
                    throw new NotImplementedException("ErpListener has not been implemented for ERP type: " + typeOfErp);
            }
        }

        public void RunTransfer()
        {
            factory.RunTransfer();
        }

        public void GetPoTo()
        {
            factory.GetPoTo();
        }

        public string GetDBScript(string entity)
        {
            return factory.GetDBScript(entity);
        }

        public void GetBom()
        {
            factory.GetBom();
        }
    }
}
