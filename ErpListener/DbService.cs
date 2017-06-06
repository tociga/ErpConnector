using ErpConnector.Common;
using System;
using System.Configuration;
using System.Linq;

namespace ErpConnector.Listener
{
    public class DbService
    {
        public void Sync()
        {
            using (var db = new Staging())
            {
                var erpActionsList = db.erp_actions.Where(a => a.success != true && a.action_type == "run_transfer").ToList();
                foreach (var item in erpActionsList)
                {
                    var erpType = ConfigurationManager.AppSettings["erp_type"];
                    var connector = new GenericConnector(erpType);
                    connector.RunTransfer();
                    Console.WriteLine("action type: " + item.action_type + " finished running.");
                }
            }
        }
    }
}
