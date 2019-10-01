using ErpConnectorApi.App_Start;
using ErpConnectorApi.Handlers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Security.ActiveDirectory;
using Owin;
using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;


namespace ErpConnectorApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            HttpConfiguration config = new HttpConfiguration();
            config.MessageHandlers.Add(new CustomLogHandler());
            //log4net.Config.XmlConfigurator.Configure();
            WebApiConfig.Register(config);
            app.UseWebApi(config);

            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }

        public void ConfigureAuth(IAppBuilder app)
        {

            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                {
                    //Audience = ConfigurationManager.AppSettings["ida:Audience"]
                    Tenant = ConfigurationManager.AppSettings["ida:Tenant"],
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = ConfigurationManager.AppSettings["ida:Audience"],
                    }
                }
            );
        }

        public static void Start()
        {
            using (WebApp.Start<Startup>("http://localhost:12345"))
            {
                Console.ReadLine();
            }

        }


    }
}
