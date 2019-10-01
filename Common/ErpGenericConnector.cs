using ErpConnector.Common.ErpTasks;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ErpConnector.Common.AGREntities;

namespace ErpConnector.Common
{
    public abstract class ErpGenericConnector : IErpConnector
    {
        #region
        public delegate void ErpTaskCompleted(object sender, ErpTaskCompletedArgs args);
        public event ErpTaskCompleted ErpTaskCompletedEvent;
        #endregion
        #region Initialization
        private bool includesFashion;
        private bool includeB_M;
        public ErpGenericConnector()
        {            
            Boolean.TryParse(ConfigurationManager.AppSettings["includesFashion"], out includesFashion);
            Boolean.TryParse(ConfigurationManager.AppSettings["includeBAndM"], out includeB_M);
        }

        #endregion

        #region Public Interface
        //public string GetDBScript(string entity)
        //{
        //    return ScriptGeneratorModule.GenerateScript(entity);
        //}
        public int TaskList(int actionId, ErpTask erpTasks, DateTime date, int? no_of_paralle_processes)
        {
            AxBaseException result = null;
            try
            {
                //DataWriter.TruncateTables(erpTasks.truncate_items, erpTasks.truncate_sales_trans_dump, erpTasks.truncate_sales_trans_refresh, erpTasks.truncate_locations_and_vendors,
                //    erpTasks.truncate_lookup_info, erpTasks.truncate_bom, erpTasks.truncate_po_to, erpTasks.truncate_price, erpTasks.truncate_attribute_refresh);
                TaskExecute exec = new TaskExecute(erpTasks.Steps, 1, actionId, date);
                exec.Execute();

                //foreach (var erpStep in erpTasks.Steps)
                //{
                //    ExecuteTask(actionId, erpStep, date); // possible to do some parallel processing.
                //}
            }
            catch (Exception e)
            {
                result = new AxBaseException { ApplicationException = e };
            }
            finally
            {
                OnTaskCompleted(null, new ErpTaskCompletedArgs { Exception = result, ActionId = actionId, Status = result == null ? 2 : 3 });
            }
            return actionId;
        }

        public int GetSingleTable(ErpTaskStep step, int actionId, DateTime date)
        {
            AxBaseException result = null;
            try
            {
                if (date == DateTime.MaxValue)
                {
                    DataWriter.TruncateSingleTable(step.DbTable);
                }
                result = TaskExecute.ExecuteTask(actionId, step, date);                
            }
            catch (Exception e)
            {
                result =  new AxBaseException { ApplicationException = e };
            }
            finally
            {
                OnTaskCompleted(null, new ErpTaskCompletedArgs { Exception = result, ActionId = actionId, Status = result== null ? 2 : 3 });                
            }
            return actionId;
        }

        #endregion




        public virtual int CreatePoTo(List<POTOCreate> po_to_create, int actionId)
        {
            throw new NotImplementedException();
        }

        public virtual int CreateItems(int tempId, int actionId)
        {
            throw new NotImplementedException();
        }
        public virtual int UpdateProductLifecycleState(int actionId, int plcUpdateId)
        {
            throw new NotImplementedException();
        }
                
        public void OnTaskCompleted(object sender, ErpTaskCompletedArgs args)
        {
            if(ErpTaskCompletedEvent !=null)
            {
                ErpTaskCompletedEvent(sender, args);
            }
        }
    //    public AxBaseException UpdateIssueAccount()
    //    {
    //        var issuesWithoutAccount = DataWriter.GetIssuesWithoutAccount();
    //        var authData = Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.JIRA);
    //        foreach (var i in issuesWithoutAccount)
    //        {
    //            StringBuilder sb = new StringBuilder();
    //            sb.Append("{ \"fields\": { \"cusiftomfield_11330\":");
    //            sb.Append(i.account_id);
    //            sb.Append("} }");

        //            string endpoint = "issue/" + i.issue_key;
        //            var result = ServiceConnector.CallOdataEndpointPut(endpoint, null, sb.ToString(), authData).Result;
        //            if (result != null)
        //            {
        //                return result;
        //            }
        //        }


        //        return null;
        //    }
    }
}
