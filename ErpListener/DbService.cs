using Common.Logging;
using ErpConnector.Common;
using System;
using System.Configuration;
using System.Linq;

namespace ErpConnector.Listener
{
    public class DbService
    {
        public bool? Sync()
        {
            using (var db = new Staging())
            {
                var pendingDataTransfer = db.erp_actions.Where(a => a.action_type == "run_transfer" && a.success != true).Any();
                if (pendingDataTransfer)
                {
                    var erpType = ConfigurationManager.AppSettings["erp_type"];
                    var connector = new GenericConnector(erpType);
                    connector.RunTransfer();
                    var actions = db.erp_actions.Where(a => a.action_type == "run_transfer").ToList();
                    foreach (var item in actions)
                    {
                        item.success = true;
                    }
                    db.SaveChanges();
                    return true;
                }
                return null;
            }
        }
    }
}
