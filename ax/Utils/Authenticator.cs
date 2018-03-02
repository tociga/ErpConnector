using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.Utils
{
    public class Authenticator
    {
        public static string GetAdalHeader()
        {
            return Authenticate().CreateAuthorizationHeader();
        }
        public static string GetAdalToken()
        {
            return Authenticate().AccessToken;
        }
        private static AuthenticationResult Authenticate()
        {
            var axOAuthTokenUrl = ConfigurationManager.AppSettings["ax_oauth_token_url"];
            var axRedirectUrl = ConfigurationManager.AppSettings["ax_redirect_url"];
            var axClientKey = ConfigurationManager.AppSettings["ax_client_key"];
            var axClientSecret = ConfigurationManager.AppSettings["ax_client_secret"];

            var uri = new UriBuilder(axOAuthTokenUrl);

            var authenticationContext = new AuthenticationContext(uri.ToString());
            var credentials = new ClientCredential(axClientKey, axClientSecret);

            var authResult = authenticationContext.AcquireTokenAsync(ConfigurationManager.AppSettings["ax_base_url"], credentials).Result;

            return authResult;
        }

    }
}
