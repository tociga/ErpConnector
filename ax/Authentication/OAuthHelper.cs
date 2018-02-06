using System;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace ErpConnector.Ax.Authentication
{
    public class OAuthHelper
    {
        /// <summary>
        /// The header to use for OAuth.
        /// </summary>
        public const string OAuthHeader = "Authorization";

        private ClientConfiguration ClientConfig;

        public OAuthHelper(ClientConfiguration config)
        {
            ClientConfig = config;
        }

        public string UriString { get { return ClientConfig.UriString; } }

        /// <summary>
        /// Retrieves an authentication header from the service.
        /// </summary>
        /// <returns>The authentication header for the Web API call.</returns>
        public string GetAuthenticationHeader()
        {
            var uri = new UriBuilder(ClientConfig.ActiveDirectoryTenant); // "https://login.windows.net/reynd.is/oauth2/token");
            //var redirectUri = new UriBuilder("http://agrdynamics.com/agr5ax7");

            var authenticationContext = new AuthenticationContext(uri.ToString());
            var cred = new ClientCredential(ClientConfig.ActiveDirectoryClientAppId/*"4d2a3c5d-7e63-40a8-9c37-c8769b1c5af3"*/, ClientConfig.ClientSecret/*"px8O9/yP1alySqXxYBtHgKo2LdRlBYBJCr1mio/Quns="*/);

            var authResult = authenticationContext.AcquireTokenAsync(ClientConfig.ActiveDirectoryResource/*"https://agrax365u6cff78f756c41ad0eaos.cloudax.dynamics.com"*/, cred).Result;


            return authResult.CreateAuthorizationHeader();

        }
    }
}
