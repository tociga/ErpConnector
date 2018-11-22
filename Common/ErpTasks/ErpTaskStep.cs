using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpConnector.Common.DTO;

namespace ErpConnector.Common.ErpTasks
{
    public class ErpTaskStep
    {
        public enum ErpTaskType { ODATA_ENDPOINT = 0, CUSTOM_SERVICE, CUSTOM_SERVICE_BY_DATE };
        public enum PeriodIncrementType { NONE = 0, HOURS, DAYS, MONTHS};
        public enum AuthenticationType { D365 = 1, TEMPO, JIRA, JIRASERVICEDESK};
        public int Id { get; set; }
        public string StepName { get; set; }
        public string EndPoint { get; set; }
        public string ServiceMethod { get; set; }
        public string ServiceName { get; set; }
        public ErpTaskType TaskType { get; set; }
        public Type ReturnType
        {
            get
            {
                string fullName = "";
                if (IsAGRType)
                {
                    if (AuthenitcationType == AuthenticationType.D365)
                    {
                        fullName = "ErpConnector.Ax.DTO." + ReturnTypeStr + ",ErpConnector.Ax";
                    }
                    else
                    {
                        var builder = new StringBuilder();
                        builder.Append(string.Format("ErpConnector.Jira.DTO.{0},ErpConnector.Jira", ReturnTypeStr));
                        fullName = builder.ToString();
                    }
                }
                else
                {
                    fullName = "ErpConnector.Ax.Microsoft.Dynamics.DataEntities." + ReturnTypeStr + ",ErpConnector.Ax";
                }
                return Type.GetType(fullName);
            }
        }
        public Type GenericObjectType
        {
            get
            {
                if (AuthenitcationType == AuthenticationType.D365)
                {
                    return typeof(GenericJsonOdata<>);
                }
                else
                {
                    return typeof(GenericJiraObjectList<>);
                }
            }
        }
        public string DbTable { get; set; }
        public string EndpointFilter { get; set; }
        public int? MaxPageSize { get; set; }
        public PeriodIncrementType PeriodIncrement { get; set; }
        public int Priority { get; set; }
        public string ReturnTypeStr { get; set; }
        public bool IsAGRType { get; set; }
        public AuthenticationType AuthenitcationType { get; set; }

        public class ErpTaskStepComparer : IComparer<ErpTaskStep>
        {
            public int Compare(ErpTaskStep a, ErpTaskStep b)
            {
                return a.Priority - b.Priority;
            }
        }
    }
}
