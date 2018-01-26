using ErpConnector.Ax;
using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Utils;
using ErpConnector.Common.AGREntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //ErpConnector.Ax.Microsoft.Dynamics.DataEntities.ReleasedDistinctProduct rd = new ErpConnector.Ax.Microsoft.Dynamics.DataEntities.ReleasedDistinctProduct();
            Type entityObject = typeof(ErpConnector.Ax.Microsoft.Dynamics.DataEntities.Vendor);

            if (entityObject != null)
            {
                ScriptGenerator sg = new ScriptGenerator(entityObject);
                string str = sg.CreateScript("[ax].[RETAILVENDTABLE]");
                Console.WriteLine(str);
            }
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //AxODataConnector connector = new AxODataConnector();

            //List<POTOCreate> list = new List<POTOCreate>();
            //list.Add(new POTOCreate { order_id = 1, item_no = "2000003", unit_qty_chg = 10m, location_no = "Picc", order_from_location_no = "OI010", est_delivery_date = new DateTime(2017, 11, 2),
            //    vendor_location_type = "vendor"});

            //var result = connector.CreatePoTo(list, -1);
                        

           
           
        }
    }
}
