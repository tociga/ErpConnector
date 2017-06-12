using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using ErpConnector.Ax.Modules;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.DTO;
using System.Diagnostics;
using System.Configuration;
using ErpConnector.Common;
using ErpConnector.Ax.Utils;

namespace ErpConnector.Ax
{
    public class AxODataConnector : IErpConnector
    {
        #region Initialization
        private static string _header;
        private static Resources _context;
        public AxODataConnector()
        {
            var axBaseUrl = ConfigurationManager.AppSettings["ax_base_url"]; 
            _header = GetAdalToken();
            _context = new Resources(new Uri(axBaseUrl + "/data"));
            _context.SendingRequest2 += Context_SendingRequest2;                      
        }

        private void Context_SendingRequest2(object sender, global::Microsoft.OData.Client.SendingRequest2EventArgs e)
        {
            e.RequestMessage.SetHeader("Authorization", _header);
        }

        private static string GetAdalToken()
        {
            var axOAuthTokenUrl = ConfigurationManager.AppSettings["ax_oauth_token_url"];
            var axRedirectUrl = ConfigurationManager.AppSettings["ax_redirect_url"];
            var axClientKey = ConfigurationManager.AppSettings["ax_client_key"];
            var axClientSecret = ConfigurationManager.AppSettings["ax_client_secret"];

            var uri = new UriBuilder(axOAuthTokenUrl);

            var authenticationContext = new AuthenticationContext(uri.ToString());
            var credentials = new ClientCredential(axClientKey, axClientSecret);

            var authResult = authenticationContext.AcquireTokenAsync(ConfigurationManager.AppSettings["ax_base_url"], credentials).Result;

            return authResult.CreateAuthorizationHeader();
        }
        #endregion

        #region Public Interface
        public string GetDBScript(string entity)
        {
            return ScriptGeneratorModule.GenerateScript(entity);
        }
        public ItemDTO CreateItem(ItemDTO item)
        {
            return ItemTransfer.CreateItem(item, _header);
        }

        public void GetBom()
        {
            DataWriter.TruncateTables(false, false, false, false, false, true);
            BomTransfer.GetBom(_header);
        }

        public void GetPoTo()
        {
            POTransfer.GetPosAndTos(_header, _context);
        }
        public void RunTransfer()
        {
            var start = DateTime.Now;
            DataWriter.TruncateTables(true, false, false,true, true, false);
            var truncate = DateTime.Now;
            Debug.WriteLine("Truncate = " + truncate.Subtract(start).TotalSeconds);
            ItemCategoryTransfer.WriteCategories(_header);
            DateTime cat = DateTime.Now;
            Debug.WriteLine("Category = " + cat.Subtract(truncate).TotalSeconds);
            LocationsAndVendorsTransfer.WriteLocationsAndVendors(_context, _header);
            DateTime loc = DateTime.Now;
            Debug.WriteLine("Locations = " + loc.Subtract(cat).TotalSeconds);
            ItemTransfer.WriteItems(_context, _header);
            DateTime items = DateTime.Now;
            Debug.WriteLine("Items = " + items.Subtract(loc).TotalSeconds);
            ItemAttributeLookup.ReadItemAttributes(_context, _header);
            DateTime lookup = DateTime.Now;
            Debug.WriteLine("Lookup = " + lookup.Subtract(items).TotalSeconds);
        }
        #endregion
    }
}
