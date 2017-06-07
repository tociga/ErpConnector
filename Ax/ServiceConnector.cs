using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ErpConnector.Ax.DTO;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using System.Configuration;
using ErpConnector.Ax.Utils;

namespace ErpConnector.Ax
{
    public class ServiceConnector
    {
        public static async Task<T> CallOdataEndpointPost<T>(string oDataEndpoint, string filters, string adalHeader, T postDataObject)
        {
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["ax_base_url"];
            string endpoint = baseUrl + "/data/" + oDataEndpoint + filters ?? "";

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = adalHeader;
            //request.Headers["Accept"] = "application/json;odata.metadata=none";
            //request.Headers["Content-Type"] = "application/json";

            request.Method = "POST";
            var postData = JsonConvert.SerializeObject(postDataObject, new EnumConverter());
            request.ContentLength = postData != null ? postData.Length : 0;
            request.ContentType = "application/json";
           

            using (var requestStream = request.GetRequestStream())
            {
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(postData);
                    writer.Flush();
                }
            }

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(responseStream))
                        {
                            var responseString = streamReader.ReadToEnd();
                            //string sanitized = SanitizeJsonString(responseString);

                            return JsonConvert.DeserializeObject<T>(responseString);
                            //return responseString;
                        }
                    }
                }
            }
            catch (WebException e)
            {
                using (var rStream = e.Response.GetResponseStream())
                {
                    using (var reader = new StreamReader(rStream))
                    {
                        var r = reader.ReadToEnd();

                        // TODO: Need to log error;
                        return default(T);
                    }
                }
            }
        }

        

        public static async Task<T> CreateEntity<T>(string oDataEndpoint, string filters, string adalHeader, T postDataObject, List<string> errorMessage)
        {
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["ax_base_url"];
            string endpoint = baseUrl + "/data/" + oDataEndpoint + filters ?? "";

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = adalHeader;
            //request.Headers["Content-Type"] = "application/json";

            request.Method = "POST";
            var postData = JsonConvert.SerializeObject(postDataObject, new EnumConverter());
            request.ContentLength = postData != null ? postData.Length : 0;
            request.ContentType = "application/json";

            using (var requestStream = request.GetRequestStream())
            {
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(postData);
                    writer.Flush();
                }
            }

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(responseStream))
                        {
                            var responseString = streamReader.ReadToEnd();
                            //string sanitized = SanitizeJsonString(responseString);

                            return JsonConvert.DeserializeObject<T>(responseString);
                            //return responseString;
                        }
                    }
                }
            }
            catch (WebException e)
            {
                using (var rStream = e.Response.GetResponseStream())
                {
                    using (var reader = new StreamReader(rStream))
                    {
                        var r = reader.ReadToEnd();

                        // TODO: Need to log error;
                        errorMessage.Add(r);
                        return default(T);
                    }
                }
            }
        }
        public static async Task<GenericJsonOdata<T>> CallOdataEndpoint<T>(string oDataEndpoint, string filters, string adalHeader)
        {
            var baseUrl = ConfigurationManager.AppSettings["ax_base_url"];
            var endpoint = baseUrl + "/data/" + oDataEndpoint + filters??"";

            var request =(HttpWebRequest)HttpWebRequest.Create(endpoint);
            request.Accept = "application/json;odata.metadata=none";
            request.Headers["Authorization"] = adalHeader;
            request.Method = "GET";
            request.Timeout = 1000 * 60 * 3;
            //request.ContentLength = postData != null ? postData.Length : 0;

            //using (var requestStream = request.GetRequestStream())
            //{
            //    using (StreamWriter writer = new StreamWriter(requestStream))
            //    {
            //        writer.Write(postData);
            //        writer.Flush();
            //    }
            //}

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        var responseString = streamReader.ReadToEnd();
                        //string sanitized = SanitizeJsonString(responseString);
                        return JsonConvert.DeserializeObject<GenericJsonOdata<T>>(responseString);

                    }
                }
            }
        }
        public static async Task<List<T>> CallAGRServiceArray<T>(string service, string serviceMethod, string postData, string adalHeader, string serviceGroup = null)
        {
            var baseUrl = System.Configuration.ConfigurationManager.AppSettings["ax_base_url"];
            serviceGroup = serviceGroup ?? System.Configuration.ConfigurationManager.AppSettings["StandardServiceGroup"];
            var endpoint = baseUrl + "/api/services/" + serviceGroup + "/" + service + "/" + serviceMethod;

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = adalHeader;
            //request.Headers["Content-Type"] = "application/json";
            request.Method = "POST";
            request.ContentLength = postData != null ? postData.Length : 0;

            if (request.ContentLength > 0)
            {
                using (var requestStream = request.GetRequestStream())
                {
                    using (var writer = new StreamWriter(requestStream))
                    {
                        writer.Write(postData);
                        writer.Flush();
                    }
                }
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        var responseString = streamReader.ReadToEnd();
                        //string sanitized = SanitizeJsonString(responseString);
                        return JsonConvert.DeserializeObject<List<T>>(responseString);

                    }
                }
            }
        }

        public static async Task<T> CallAGRServiceScalar<T>(string service, string serviceMethod, string postData, string adalHeader)
        {
            var baseUrl = ConfigurationManager.AppSettings["ax_base_url"];
            var standardServiceGroup = ConfigurationManager.AppSettings["StandardServiceGroup"];
            var endpoint = baseUrl + "/api/services/" + standardServiceGroup + "/" + service + "/" + serviceMethod;

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = adalHeader;
            request.Method = "POST";
            request.ContentLength = postData.Length;

            using (var requestStream = request.GetRequestStream())
            {
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(postData);
                    writer.Flush();
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        string responseString = streamReader.ReadToEnd();
                        //string sanitized = SanitizeJsonString(responseString);
                        return JsonConvert.DeserializeObject<T>(responseString);

                    }
                }
            }
        }        

    }
}