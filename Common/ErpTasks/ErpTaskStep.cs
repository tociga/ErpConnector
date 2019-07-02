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
        public enum ErpTaskType { ODATA_ENDPOINT = 0, CUSTOM_SERVICE, CUSTOM_SERVICE_BY_DATE, ITERATIVE_ENDPOINT, COMPLEX_RETURN_TYPE };
        public enum PeriodIncrementType { NONE = 0, HOURS, DAYS, MONTHS};
        public enum AuthenticationType { D365 = 1, TEMPO, JIRA, JIRASERVICEDESK, JIRAISSUE, BC, SAP};
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
                string fullName = ReturnTypeStr + "," + ReturnTypeAssembly;
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
                else if (AuthenitcationType == AuthenticationType.TEMPO)
                {
                    return typeof(GenericJiraObjectList<>);
                }
                else if (AuthenitcationType == AuthenticationType.JIRAISSUE)
                {
                    return typeof(GenericJiraIssueObject<>);
                }
                else if (AuthenitcationType == AuthenticationType.JIRASERVICEDESK)
                {
                    return typeof(GenericJiraServiceDeskObject<>);
                }
                else if (AuthenitcationType == AuthenticationType.BC || AuthenitcationType == AuthenticationType.SAP)
                {
                    return typeof(GenericJsonOdata<>);
                }
                else
                {
                    return typeof(GenericJsonOdata<>);
                }

            }
        }
        public string DbTable { get; set; }
        public string EndpointFilter { get; set; }
        public int? MaxPageSize { get; set; }
        public PeriodIncrementType PeriodIncrement { get; set; }
        public int Priority { get; set; }
        public string ReturnTypeStr { get; set; }
        public string ReturnTypeAssembly { get; set; }
        public AuthenticationType AuthenitcationType { get; set; }
        public string ExternalProcess { get; set; }
        public string ExternalProcessArgument { get; set; }
        public string BaseTypeProcedure { get; set; }
        public string InjectionPropertyName { get; set; }
        public List<ErpTaskStepDetails> Details { get; set; }
        public class ErpTaskStepComparer : IComparer<ErpTaskStep>
        {
            public int Compare(ErpTaskStep a, ErpTaskStep b)
            {
                return a.Priority - b.Priority;
            }
        }

    }
}
