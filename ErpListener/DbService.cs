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
            try
            {
                using (var db = new Staging())
                {
                    var pendingDataTransfer = db.erp_actions.Where(a => a.action_type == "run_transfer" && a.success != true).Any();
                    if (pendingDataTransfer)
                    {
                        var erpType = ConfigurationManager.AppSettings["erp_type"];
                        var connector = new GenericConnector(erpType);
                        connector.RunTransfer();
                        return true;
                        //TODO: Log that data was transferred!
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
