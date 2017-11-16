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
            list.Add(new POTOCreate { order_id = 16, item_no = "0140", size = "Large", color = "Black", unit_qty_chg = 10m, location_no = "DC-WEST", order_from_location_no = "1004", est_delivery_date = new DateTime(2017, 11, 15),
                vendor_location_type = "vendor"});

            connector.CreatePoTo(list);
                        

           
           
        }
    }
}
