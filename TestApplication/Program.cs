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
            //Type entityObject = typeof(ErpConnector.Ax.Microsoft.Dynamics.DataEntities.CustVendExternalItem);

            //if (entityObject != null)
            //{
            //    ScriptGenerator sg = new ScriptGenerator(entityObject);
            //    string str =  sg.CreateScript("[ax].[INVENTDIM]");
            //    Console.WriteLine(str);
            //}
            AxODataConnector connector = new AxODataConnector();

            List<POTOCreate> list = new List<POTOCreate>();
            list.Add(new POTOCreate { order_id = 19, item_no = "010611", size = "010", color = "53", unit_qty_chg = 10m, location_no = "GMOOR", order_from_location_no = "SUP00000030", est_delivery_date = new DateTime(2017, 11, 15),
                vendor_location_type = "vendor", style="-" });

            var result = connector.CreatePoTo(list);
                        

           
           
        }
    }
}
