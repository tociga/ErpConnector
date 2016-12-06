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

        private Resources context;
        private static string header = "";
        public AXODataConnector()
        {
            Authorize().Wait();
            context = new Resources(new Uri("https://agrax7u2devaos.cloudax.dynamics.com/data"));
            context.SendingRequest2 += Context_SendingRequest2;           
        }

        private Task Authorize()
        {
            return Task.Run(() => {
                WithoutADAL().Wait();
                //AdalAuthenticate().Wait();
            });
        }

        public void RunTransfer()
        {
            DataAccess.DataWriter.TruncateTables(true, false, false);
            //SalesValueTransactions.WriteSalesValueTrans(context);
            ItemCategoryTransfer.WriteCategories(context);
            //LocationsAndVendorsTransfer.WriteLocationsAndVendors(context);
            ItemTransfer.WriteItems(context);
            //ItemAttributeLookup.ReadItemAttributes(context);
        }
        private static void Context_SendingRequest2(object sender, global::Microsoft.OData.Client.SendingRequest2EventArgs e)
        {
            e.RequestMessage.SetHeader("Authorization", header);
        }

        private static async Task WithoutADAL()
        {
            var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("resource", "https://agrax7u2devaos.cloudax.dynamics.com"),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", "dadi@reynd.is"),
                new KeyValuePair<string, string>("password", "ZiK289dt"),
                new KeyValuePair<string, string>("client_id", "b15e9fb9-68ba-4bec-8ce5-f1094689a573")
            };

            using (var client = new HttpClient())
            {

                string baseUrl = "https://login.windows.net/reynd.is/oauth2/";
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new FormUrlEncodedContent(postData);

                HttpResponseMessage response = await client.PostAsync("token", content);
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<dynamic>(jsonString);
                header = responseData.token_type + " " + responseData.access_token;
                //return jsonString;
            }
        }

        private static async Task AdalAuthenticate()
        {
            UriBuilder uri = new UriBuilder("https://login.windows.net/reynd.is/oauth2/");
            UriBuilder redirectUri = new UriBuilder("http://agrdynamics.com/agr5ax7");

            AuthenticationContext authenticationContext = new AuthenticationContext(uri.ToString());
            ClientCredential cred = new ClientCredential("ef354935-1a94-4c76-bd66-ac83b21a9590", "JwaY47/JdiO8liltYobBUGEcl5SFTyaZboO6FooeejQ=");

            var authResult = await authenticationContext.AcquireTokenAsync("https://agrax7u2devaos.cloudax.dynamics.com", cred);

            header = authResult.CreateAuthorizationHeader();
        }
 
    }
}
