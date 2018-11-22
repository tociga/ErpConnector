using ErpConnector.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Common.Util
{
    public class JiraAuthenticator
    {
        private static string GetJiraHeader()
        {
            return "Basic ZGFkaUBhZ3JpbnZlbnRvcnkuY29tOjhoZDQxMTdhcGs4MGplR1RIY09uMjdGQQ==";
        }

        private static string GetTempoHeader()
        {
            return "Bearer l6IESyKUDLojiadw4bChrmEDxudGMt";
        }

        private static string GetJiraToken()
        {
            return "l6IESyKUDLojiadw4bChrmEDxudGMt";
        }

        private static string GetTempoToken()
        {
            return "l6IESyKUDLojiadw4bChrmEDxudGMt";
        }

        public static ServiceData GetJiraServiceData()
        {
            return new ServiceData
            {
                AuthHeader = GetJiraHeader(),
                AuthToken = GetJiraToken(),
                BaseUrl = "https://agrinventory.atlassian.net/",
                OdataUrlPostFix = "/rest/api/2/"
            };
        }

        public static ServiceData GetTempoServiceData()
        {
            return new ServiceData
            {
                AuthHeader = GetTempoHeader(),
                AuthToken = GetTempoToken(),
                BaseUrl = "https://api.tempo.io",
                OdataUrlPostFix = "/2/"
            };
        }

        public static ServiceData GetJiraServiceDeskData()
        {
            return new ServiceData
            {
                AuthHeader = GetJiraHeader(),
                AuthToken = GetJiraToken(),
                BaseUrl = "https://agrinventory.atlassian.net/",
                OdataUrlPostFix = "/rest/servicedeskapi/"
            };

        }
    }
}
