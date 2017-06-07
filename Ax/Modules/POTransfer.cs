using System.Linq;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.Utils;

namespace ErpConnector.Ax.Modules
{
    public class POTransfer
    {
        public static void GetPosAndTos(string header, Resources context)
        {
            //var pol = AXServiceConnector.CallOdataEndpoint<PurchaseOrderHeader>("PurchaseOrderHeaders", null, header).Result;
            //var poh = context.PurchaseOrderHeaders.ToList();
            //DataWriter.WriteToTable<PurchaseOrderHeader>(poh.GetDataReader(), "[ax].[PurchaseOrderHeaders]");
            PullPoHeaders(context, 1000);

            PullPoLines(context, 3000);
        }

        private static void PullPoLines(Resources context, int pageSize)
        {
            var pol = context.PurchaseOrderLines.Take(pageSize).ToList();
            bool foundData = pol.Any();
            for(int i = 1; foundData; i++)
            {
                DataWriter.WriteToTable<PurchaseOrderLine>(pol.GetDataReader(), "[ax].[PurchaseOrderLines]");
                pol = context.PurchaseOrderLines.Skip(i * pageSize).Take(pageSize).ToList();
                foundData = pol.Any();
                System.GC.Collect();
                // Allow GC to work magic
                System.Threading.Thread.Sleep(1000);
            }
        }

        private static void PullPoHeaders(Resources context, int pageSize)
        {
            var poh = context.PurchaseOrderHeaders.Take(pageSize).ToList();
            bool foundData = poh.Any();
            for (int i = 1; foundData; i++)
            {
                DataWriter.WriteToTable<PurchaseOrderHeader>(poh.GetDataReader(), "[ax].[PurchaseOrderHeaders]");
                poh = context.PurchaseOrderHeaders.Skip(i * pageSize).Take(pageSize).ToList();
                foundData = poh.Any();
            }
        }
    }
}
