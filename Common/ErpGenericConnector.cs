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
    public class ErpGenericConnector : IErpConnector
    {
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
        public AxBaseException TaskList(int actionId, ErpTask erpTasks, DateTime date, int? no_of_paralle_processes)
        {
            //DataWriter.TruncateTables(erpTasks.truncate_items, erpTasks.truncate_sales_trans_dump, erpTasks.truncate_sales_trans_refresh, erpTasks.truncate_locations_and_vendors,
            //    erpTasks.truncate_lookup_info, erpTasks.truncate_bom, erpTasks.truncate_po_to, erpTasks.truncate_price, erpTasks.truncate_attribute_refresh);
            TaskExecute exec = new TaskExecute(erpTasks.Steps, 1, actionId, date);
            exec.Execute();

            //foreach (var erpStep in erpTasks.Steps)
            //{
            //    ExecuteTask(actionId, erpStep, date); // possible to do some parallel processing.
            //}
            return null;
        }

        public AxBaseException GetSingleTable(ErpTaskStep step, int actionId, DateTime date)
        {
            //if (date == DateTime.MaxValue)
            //{
            //    DataWriter.TruncateSingleTable(step.DbTable);
            //}
            List<ErpTaskStep> steps = new List<ErpTaskStep>();
            steps.Add(step);
            TaskExecute exec = new TaskExecute(steps, 1, actionId, date);
            exec.Execute();
            return null;

        }
        public AxBaseException GetBom(int actionId)
        {
            //DataWriter.TruncateTables(false, false, false, false, false, true, false, false, false);
            //return BomTransfer.GetBom(actionId);
            throw new NotImplementedException();
        }

        public void GetPoTo(int actionId)
        {
            //DataWriter.TruncateTables(false, false, false, false, false, false, true, false, false);
            //POTransfer.GetPosAndTos(_context, actionId);
            throw new NotImplementedException();
        }

        public void GetFullIoTrans(int actionId)
        {
            //ProductHistory ph = new ProductHistory(actionId);
            //ph.WriteInventTrans();
            //ph.WriteInventTransOrigin();
            //ph.WriteInventSumFull();
            //if (includeB_M)
            //{
            //    SalesValueTransactions.WriteSalesValueTrans(actionId);
            //    SalesValueTransactions.WriteSalesValueTransLines(actionId);
            //}
            throw new NotImplementedException();
        }
        #endregion




        public AxBaseException DailyRefresh(DateTime date, int actionId)
        {
            //var pim = PimFull(actionId);
            //if (pim != null)
            //{
            //    return pim;
            //}
            //TransactionRefresh(date, actionId);
            //return null;
            throw new NotImplementedException();
        }

        public AxBaseException FullTransfer(int actionId)
        {
            //var pim = PimFull(actionId);
            //if (pim != null)
            //{
            //    return pim;
            //}
            //TransactionFull(actionId);
            //return null;
            throw new NotImplementedException();
        }

        public AxBaseException PimFull(int actionId)
        {
            //DataWriter.TruncateTables(true, false, false, true, true, true, false, true, true);
            //var cat = ItemCategoryTransfer.WriteCategories(actionId);
            //if (cat != null)
            //{
            //    return cat;
            //}

            //var loc = LocationsAndVendorsTransfer.WriteLocationsAndVendors(actionId);
            //if (loc != null)
            //{
            //    return loc;
            //}

            //var items = ItemTransfer.WriteItems(includesFashion, actionId);
            //if (items != null)
            //{
            //    return items;
            //}

            //var attr = ItemAttributeLookup.ReadItemAttributes(includesFashion, includeB_M, actionId);
            //if (attr != null)
            //{
            //    return attr;
            //}

            //var bom = GetBom(actionId);
            //if (bom != null)
            //{
            //    return bom;
            //}
            //var price = PriceHistory.GetPriceHistory(actionId, includeB_M);
            //if (price != null)
            //{
            //    return price;
            //}
            //return null;
            throw new NotImplementedException();
        }

        public AxBaseException TransactionFull(int actionId)
        {
            //DataWriter.TruncateTables(false, true, true, false, false, false, true, true, false);
            //GetFullIoTrans(actionId);
            //GetPoTo(actionId);
            //return null;
            throw new NotImplementedException();
        }

        public AxBaseException TransactionRefresh(DateTime date, int actionId)
        {
            //DataWriter.TruncateTables(false, false, true, false, false, false, false, false, false);
            //ProductHistory ph = new ProductHistory(actionId);
            //ph.WriteInventSumRefresh(date);
            //ph.WriteInventTransRefresh(date);
            //ph.WriteInventTransOrigin();
            //POTransfer.RefreshPurchLines(date, actionId);
            //POTransfer.PullPurchTable(actionId);
            //POTransfer.PullAGROrders(actionId);
            //POTransfer.PullAGROrderLines(actionId);

            //if (includeB_M)
            //{
            //    SalesValueTransactions.WriteSalesValueTransRefresh(date, actionId);
            //    SalesValueTransactions.WriteSalesValueTransLinesRefresh(date, actionId);
            //}
            //return null;
            throw new NotImplementedException();
        }
        public AxBaseException UpdateProduct(int actionId)
        {
            //DataWriter.TruncateTables(false, false, false, false, false, false, false, false, true);
            //var attributes = ItemAttributeLookup.UpdateProductAttributes(actionId);
            //if (attributes != null)
            //{
            //    return attributes;
            //}
            //return null;
            throw new NotImplementedException();
        }

        public virtual AxBaseException CreatePoTo(List<POTOCreate> po_to_create, int actionId)
        {
            throw new NotImplementedException();
        }

        public virtual AxBaseException CreateItems(int tempId, int actionId)
        {
            throw new NotImplementedException();
        }
        public virtual AxBaseException UpdateProductLifecycleState(int actionId, int plcUpdateId)
        {
            throw new NotImplementedException();
        }
    //    public AxBaseException UpdateIssueAccount()
    //    {
    //        var issuesWithoutAccount = DataWriter.GetIssuesWithoutAccount();
    //        var authData = Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.JIRA);
    //        foreach (var i in issuesWithoutAccount)
    //        {
    //            StringBuilder sb = new StringBuilder();
    //            sb.Append("{ \"fields\": { \"customfield_11330\":");
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
