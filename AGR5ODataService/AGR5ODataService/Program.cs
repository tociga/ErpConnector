using System;
using AxConnect;

namespace AGR5ODataService
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static string header = "test";
        static void Main()
        {


            //request token for the resource - which is the AX URI for your organization. NOTE: Important do not add a trailing slash at the end of the URI

            //ClientCredential clientCred = new ClientCredential("114df472-93fe-4afa-98d5-3d4a60b44991","")


            //this gets the authorization token, which needs to be passed in the Header of the HTTP Requests
            try
            {
                //AxConnect.Modules.ProductHistory ph = new AxConnect.Modules.ProductHistory();
                //ph.WriteInventTrans();
               // ph.WriteInventTransOrigin();
                AXODataConnector connector = new AXODataConnector();
                connector.RunTransfer();
                
                //List<RetailInventTable> retailInvent = context.RetailInventTable.ToList();
                //var prodReader = p.ReadProducts(context, retailInvent);
                //DataAccess.DataWriter.WriteToTable(prodReader, "[ax].[INVENTTABLE]");
                //context.SaveChanges();

                
                System.Threading.Thread.Sleep(1000);

            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new Service1()
            //};
            //ServiceBase.Run(ServicesToRun);
        }

    }
}
