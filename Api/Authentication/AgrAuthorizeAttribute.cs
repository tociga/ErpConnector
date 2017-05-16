using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ErpConnector.Controllers
{
    internal class AgrAuthorizeAttribute : AuthorizeAttribute
    {
        static readonly HttpClient client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["Agr5ApiUrl"]) };
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authHeader = HttpContext.Current.Request.Headers["Authorization"].Replace("Bearer ", "");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authHeader);
            var authorized = Task.FromResult(false);
            var result = client.GetAsync("users/auth").Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}