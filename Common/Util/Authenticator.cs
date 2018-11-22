using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpConnector.Common.ErpTasks;
using ErpConnector.Common.DTO;

namespace ErpConnector.Common.Util
{
    public class Authenticator
    {
        public static ServiceData GetAuthData(ErpTaskStep.AuthenticationType authType)
        {
            if (authType == ErpTaskStep.AuthenticationType.TEMPO)
            {
                return JiraAuthenticator.GetTempoServiceData();
            }
            else if (authType == ErpTaskStep.AuthenticationType.JIRA)
            {
                return JiraAuthenticator.GetJiraServiceData();
            }
            else if (authType == ErpTaskStep.AuthenticationType.JIRASERVICEDESK)
            {
                return JiraAuthenticator.GetJiraServiceDeskData();
            }
            else
            {
                return D365Authenticator.GetD365ServiceData();
            }
        }
    }
}
