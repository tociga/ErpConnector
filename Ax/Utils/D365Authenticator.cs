using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.Utils
{
    public class D365Authenticator
    {
        private static AuthenticationResult _token;
        private static AuthenticationResult Token
        {
            get
            {
                if (_token == null)
                {
                    _token = Authenticate();
                }
                // If the token has less then 10 minutes of its lifetime, request a new one.
                else if (DateTime.Now.Subtract(_token.ExpiresOn.DateTime).Minutes < 10)
                {
                    _token = Authenticate();
                }
                return _token;
            }
        }
        public static string GetAdalHeader()
        {
            return Token.CreateAuthorizationHeader();
        }

        public static string GetAdalToken()
        {
            return Token.AccessToken;
        }
        private static AuthenticationResult Authenticate()
        {
            var axOAuthTokenUrl = ConfigurationManager.AppSettings["ax_oauth_token_url"];
            var axClientKey = ConfigurationManager.AppSettings["client_key"];
            var axClientSecret = ConfigurationManager.AppSettings["client_secret"];

            var uri = new UriBuilder(axOAuthTokenUrl);
            var authenticationContext = new AuthenticationContext(uri.ToString());
            var credentials = new ClientCredential(axClientKey, axClientSecret);

            var authResult = authenticationContext.AcquireTokenAsync(ConfigurationManager.AppSettings["base_url"], credentials).Result;
            return authResult;
        }

    }
}
