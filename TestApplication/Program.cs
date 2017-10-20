using ErpConnector.Ax;
using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Utils;
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
            Type entityObject = typeof(ErpConnector.Ax.Microsoft.Dynamics.DataEntities.CustVendExternalItem);

            if (entityObject != null)
            {
                ScriptGenerator sg = new ScriptGenerator(entityObject);
                string str =  sg.CreateScript("[ax].[INVENTDIM]");
                Console.WriteLine(str);
            }

           
           
        }
    }
}
