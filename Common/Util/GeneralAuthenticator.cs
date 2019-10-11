using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Configuration;


namespace ErpConnector.Common.Utils
{
    public abstract class GeneralAuthenticator
    {
        public abstract string GetHeader();
        public abstract string GetToken();
    }
}
