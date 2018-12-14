using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpConnector.Common.DTO;

namespace ErpConnector.Common.Util
{
    public class JiraIssueAuthenticator : JiraAuthenticator
    {
        public override ServiceData GetJiraServiceData()
        {
            var serviceData = base.GetJiraServiceData();
            serviceData.AuthType = ErpTasks.ErpTaskStep.AuthenticationType.JIRAISSUE;
            return serviceData;
        }
    }
}
