using ErpConnectorApi.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ErpConnectorApi.Handlers
{
    class CustomLogHandler : DelegatingHandler
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();
            var metadata = BuildRequestMetadata(request);
            return await base.SendAsync(request, cancellationToken).ContinueWith(response =>
            {
                LogicalThreadContext.Properties["RequestUri"] = metadata.RequestUri;
                LogicalThreadContext.Properties["RequestMethod"] = metadata.RequestMethod;
                LogicalThreadContext.Properties["RequestBody"] = metadata.RequestBody;
                LogicalThreadContext.Properties["ReferrerFullUri"] = metadata.ReferrerFullUri;

                if (response.IsCanceled)
                {
                    LogicalThreadContext.Properties["ResponseStatusCode"] = 400;
                    LogicalThreadContext.Properties["UserId"] = -1;
                    // Fire and forget
                    Task.Run(() => log.Info(""));
                    return null;
                }
                else if (response.IsFaulted)
                {
                    LogicalThreadContext.Properties["ResponseStatusCode"] = 500;
                    LogicalThreadContext.Properties["UserId"] = -1;
                    // Fire and forget
                    Task.Run(() => log.Info(""));
                    return response.Result;
                }

                var responseMessage = response.Result.RequestMessage;
                var shouldLogApiRequests = ConfigurationManager.AppSettings["logWebApiRequests"] != null && ConfigurationManager.AppSettings["logWebApiRequests"].ToString() == "true";

                if (shouldLogApiRequests)
                {
                    var userId = -1;
                    var isApiControllerRequest = responseMessage != null && responseMessage.Properties.FirstOrDefault(p => p.Key == "MS_DisposableRequestResources").Value != null;
                    if (isApiControllerRequest)
                    {
                        userId = TryGetUserId(responseMessage);
                    }

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;

                    LogicalThreadContext.Properties["UserId"] = userId;
                    LogicalThreadContext.Properties["ResponseStatusCode"] = (int)response.Result.StatusCode;
                    LogicalThreadContext.Properties["ElapsedTimeMs"] = elapsedMs;


                    // Fire and forget
                    Task.Run(() => log.Info(""));
                }
                return response.Result;
            });
        }

        private int TryGetUserId(HttpRequestMessage responseMessage)
        {
            try
            {
                var disposableResponses = responseMessage.Properties["MS_DisposableRequestResources"] as List<IDisposable>;
                var controller = disposableResponses.Where(r => (r as ApiController) != null).FirstOrDefault() as ApiController;
                var userIdClaim = (controller.User.Identity as ClaimsIdentity).Claims.FirstOrDefault(claim => claim.Type == "UserId");
                if (userIdClaim != null && userIdClaim.Value != null)
                {
                    return int.Parse(userIdClaim.Value);
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private LogMetadata BuildRequestMetadata(HttpRequestMessage request)
        {
            IEnumerable<String> referrerFullUri;
            var data = new LogMetadata
            {
                RequestMethod = request.Method.Method,
                RequestUri = request.RequestUri.ToString(),
                RequestBody = MaskAttributes(request)
            };

            if (request.Headers.TryGetValues("ReferrerFullUri", out referrerFullUri))
            {
                data.ReferrerFullUri = referrerFullUri.FirstOrDefault();
            }
            return data;
        }

        private string MaskAttributes(HttpRequestMessage request)
        {
            var body = String.Empty;
            try
            {
                body = request.Content.ReadAsStringAsync().Result;
                if (!String.IsNullOrEmpty(body) && body.Length < 1000)
                {
                    dynamic obj = JsonConvert.DeserializeObject(body);
                    var isArray = obj.GetType().Name == "JArray";
                    if (!isArray)
                    {
                        // Mask password before logging
                        if (obj.password != null)
                        {
                            obj.password.Value = "";
                        }
                        if (obj.password_confirmation != null)
                        {
                            obj.password_confirmation.Value = "";
                        }
                        if (obj.currentPassword != null)
                        {
                            obj.currentPassword.Value = "";
                        }
                        if (obj.current_password != null)
                        {
                            obj.current_password.Value = "";
                        }

                        body = JsonConvert.SerializeObject(obj);
                    }
                }
            }
            catch (Exception)
            {
                // Dig a hole and bury the evidence...
            }

            return body;
        }
    }
}
