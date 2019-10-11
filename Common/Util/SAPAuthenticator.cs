using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpConnector.Common.DTO;

namespace ErpConnector.Common.Util
{
    public class SAPAuthenticator
    {
        public static ServiceData GetSAPServiceData()
        {

            return new ServiceData
            {
                AuthMethod = "Basic",
                AuthToken = ConfigurationManager.AppSettings["client_secret"],
                BaseUrl = ConfigurationManager.AppSettings["base_url"],
                OdataUrlPostFix = "/",
                AuthType = ErpTasks.ErpTaskStep.AuthenticationType.SAP
            };
        }
    }
}
