using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.Authentication
{
    public partial class ClientConfiguration
    {
        public ClientConfiguration(string uriString,
                                    string secret,
                                    string activeDirectoryResource,
                                    String activeDirectoryTenant,
                                    String activeDirectoryClientAppId)// OneBox = new ClientConfiguration()
        {
            UriString = uriString;
            ClientSecret = secret;
            ActiveDirectoryResource = activeDirectoryResource;
            ActiveDirectoryTenant = activeDirectoryTenant;
            ActiveDirectoryClientAppId = activeDirectoryClientAppId;
        }

        public string ClientSecret { get; set; }
        public string UriString { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public String ActiveDirectoryTenant { get; set; }
        public String ActiveDirectoryClientAppId { get; set; }
    }
}
