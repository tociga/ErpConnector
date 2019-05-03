using ErpConnector.Common.DTO;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Configuration;


namespace ErpConnector.Common.Util
{
    public class D365Authenticator
    {
        private static AuthenticationResult Authenticate()
        {
            var axOAuthTokenUrl = ConfigurationManager.AppSettings["ax_oauth_token_url"];
            var axRedirectUrl = ConfigurationManager.AppSettings["ax_redirect_url"];
            var axClientKey = ConfigurationManager.AppSettings["ax_client_key"];
            var axClientSecret = ConfigurationManager.AppSettings["ax_client_secret"];

            var uri = new UriBuilder(axOAuthTokenUrl);

            var authenticationContext = new AuthenticationContext(uri.ToString());
            var credentials = new ClientCredential(axClientKey, axClientSecret);

            var authResult = authenticationContext.AcquireTokenAsync(ConfigurationManager.AppSettings["base_url"], credentials).Result;

            return authResult;
        }

        public static ServiceData GetD365ServiceData()
        {
            var result = Authenticate();
            return new ServiceData
            {
                AuthHeader = result.CreateAuthorizationHeader(),
                AuthToken = result.AccessToken,
                BaseUrl = ConfigurationManager.AppSettings["base_url"],
                OdataUrlPostFix = "/data/",
                AuthType = ErpTasks.ErpTaskStep.AuthenticationType.D365
               
            };
        }


    }
}
