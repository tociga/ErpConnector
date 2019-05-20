using ErpConnector.Common.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.Util
{
    public class BusinessCentralAuthenticator
    {
        public static ServiceData GetBCServiceData()
        {
            
            return new ServiceData
            {
                AuthHeader = "Basic " + ConfigurationManager.AppSettings["client_secret"],
                AuthToken = ConfigurationManager.AppSettings["client_secret"],
                BaseUrl = ConfigurationManager.AppSettings["base_url"],
                OdataUrlPostFix = "/",
                AuthType = ErpTasks.ErpTaskStep.AuthenticationType.BC
            };
        }


    }
}
