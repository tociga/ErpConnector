using AxConnect.Microsoft.Dynamics.DataEntities;
using AxConCommon.Extensions;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AxConnect.Modules;

namespace AxConnect
{
    public class AXODataConnector
    {
        static string header = "";
        private Resources context;
        public AXODataConnector()
        {
            Authorize().Wait();
            context = new Resources(new Uri("https://agrax7u2devaos.cloudax.dynamics.com/data"));
            context.SendingRequest2 += Context_SendingRequest2;           
        }

        private Task Authorize()
        {
            return Task.Run(() => {
                header = AdalAuthenticate().Result;
            });
        }

        public void RunTransfer()
        {
            DataAccess.DataWriter.TruncateTables(true, false, false,true);
            //SalesValueTransactions.WriteSalesValueTrans(context);
            ItemCategoryTransfer.WriteCategories(header);
            LocationsAndVendorsTransfer.WriteLocationsAndVendors(context, header);
            ItemTransfer.WriteItems(context, header);
            //ItemAttributeLookup.ReadItemAttributes(context);
            //WritePO writeTest = new WritePO(context);
            //writeTest.WriteTestPO();
        }
        private static void Context_SendingRequest2(object sender, global::Microsoft.OData.Client.SendingRequest2EventArgs e)
        {
            e.RequestMessage.SetHeader("Authorization", header);
        }

        private static async Task<string> AdalAuthenticate()
        {
            UriBuilder uri = new UriBuilder("https://login.windows.net/reynd.is/oauth2/token");
            UriBuilder redirectUri = new UriBuilder("http://agrdynamics.com/agr5ax7");

            AuthenticationContext authenticationContext = new AuthenticationContext(uri.ToString());
            ClientCredential cred = new ClientCredential("4d2a3c5d-7e63-40a8-9c37-c8769b1c5af3", "px8O9/yP1alySqXxYBtHgKo2LdRlBYBJCr1mio/Quns=");

            var authResult = await authenticationContext.AcquireTokenAsync("https://agrax7u2devaos.cloudax.dynamics.com", cred);

            return authResult.CreateAuthorizationHeader();
        }
 
    }
}
