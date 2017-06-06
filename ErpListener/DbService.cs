using System;
using System.Linq;

namespace ErpListener
{
    public class DbService
    {
        public void Sync()
        {
            using (var db = new Staging())
            {
                var erpActionsList = db.erp_actions.Where(a => a.success != true).ToList();
                foreach (var item in erpActionsList)
                {
                    Console.WriteLine("action type: " + item.action_type);
                }

            }
        }
    }
}
