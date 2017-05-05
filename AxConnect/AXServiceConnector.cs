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
using ErpDTO.DTO;
using ErpDTO.Microsoft.Dynamics.DataEntities;

namespace AxConnect
{
    public class AXServiceConnector
    {
        private static string header = null;

        public AXServiceConnector()
        {
            //Authorize();
        }
        //private static void Authorize()
        //{
        //    header = WithoutADAL().Result;
        //}

        //private static string Header
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(header))
        //        {
        //            Authorize();
        //        }
        //        return header;
        //    }
        //}
        //private static async Task<string> WithoutADAL()
        //{
        //    var postData = new List<KeyValuePair<string, string>>
        //    {
        //        new KeyValuePair<string, string>("resource", "https://agrax7u2devaos.cloudax.dynamics.com"),
        //        new KeyValuePair<string, string>("grant_type", "client_credentials"),
        //        new KeyValuePair<string, string>("client_secret", "px8O9/yP1alySqXxYBtHgKo2LdRlBYBJCr1mio/Quns="),
        //        new KeyValuePair<string, string>("client_id", "4d2a3c5d-7e63-40a8-9c37-c8769b1c5af3")
        //    };

        //    using (var client = new HttpClient())
        //    {

        //        string baseUrl = "https://login.windows.net/reynd.is/oauth2/";
        //        client.BaseAddress = new Uri(baseUrl);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        var content = new FormUrlEncodedContent(postData);

        //        HttpResponseMessage response = await client.PostAsync("token", content);
        //        string jsonString = await response.Content.ReadAsStringAsync();
        //        var responseData = JsonConvert.DeserializeObject<dynamic>(jsonString);
        //        return responseData.token_type + " " + responseData.access_token;
        //        //return jsonString;
        //    }
        //}
        public static async Task<T> CallOdataEndpointPost<T>(string oDataEndpoint, string filters, string adalHeader, T postDataObject)
        {
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["ax_base_url"];
            string endpoint = baseUrl + "/data/" + oDataEndpoint + filters ?? "";

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = adalHeader;
            //request.Headers["Accept"] = "application/json;odata.metadata=none";
            //request.Headers["Content-Type"] = "application/json";

            request.Method = "POST";
            var postData = JsonConvert.SerializeObject(postDataObject, new Converters.AxEnumConverter());
            request.ContentLength = postData != null ? postData.Length : 0;
            request.ContentType = "application/json";
           

            using (var requestStream = request.GetRequestStream())
            {
                using (StreamWriter writer = new StreamWriter(requestStream))
                {
                    writer.Write(postData);
                    writer.Flush();
                }
            }

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            string responseString = streamReader.ReadToEnd();
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
            var postData = JsonConvert.SerializeObject(postDataObject, new Converters.AxEnumConverter());
            request.ContentLength = postData != null ? postData.Length : 0;
            request.ContentType = "application/json";

            using (var requestStream = request.GetRequestStream())
            {
                using (StreamWriter writer = new StreamWriter(requestStream))
                {
                    writer.Write(postData);
                    writer.Flush();
                }
            }

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            string responseString = streamReader.ReadToEnd();
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
        public static async Task<ErpDTO.DTO.GenericJsonOdata<T>> CallOdataEndpoint<T>(string oDataEndpoint, string filters, string adalHeader)
        {
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["ax_base_url"];
            string endpoint = baseUrl + "/data/" + oDataEndpoint + filters??"";

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
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string responseString = streamReader.ReadToEnd();
                        //string sanitized = SanitizeJsonString(responseString);
                        return JsonConvert.DeserializeObject<ErpDTO.DTO.GenericJsonOdata<T>>(responseString);

                    }
                }
            }
        }
        public static async Task<List<T>> CallAGRServiceArray<T>(string service, string serviceMethod, string postData, string adalHeader, string serviceGroup = null)
        {
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["ax_base_url"];
            serviceGroup = serviceGroup ?? System.Configuration.ConfigurationManager.AppSettings["StandardServiceGroup"];
            string endpoint = baseUrl + "/api/services/" + serviceGroup + "/" + service + "/" + serviceMethod;

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = adalHeader;
            //request.Headers["Content-Type"] = "application/json";
            request.Method = "POST";
            request.ContentLength = postData != null ? postData.Length : 0;

            if (request.ContentLength > 0)
            {
                using (var requestStream = request.GetRequestStream())
                {
                    using (StreamWriter writer = new StreamWriter(requestStream))
                    {
                        writer.Write(postData);
                        writer.Flush();
                    }
                }
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string responseString = streamReader.ReadToEnd();
                        //string sanitized = SanitizeJsonString(responseString);
                        return JsonConvert.DeserializeObject<List<T>>(responseString);

                    }
                }
            }
        }

        public static async Task<T> CallAGRServiceScalar<T>(string service, string serviceMethod, string postData, string adalHeader)
        {
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["ax_base_url"];
            string standardServiceGroup = System.Configuration.ConfigurationManager.AppSettings["StandardServiceGroup"];
            string endpoint = baseUrl + "/api/services/" + standardServiceGroup + "/" + service + "/" + serviceMethod;

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = adalHeader;
            //request.Headers["Content-Type"] = "application/json";
            request.Method = "POST";
            request.ContentLength = postData.Length;

            using (var requestStream = request.GetRequestStream())
            {
                using (StreamWriter writer = new StreamWriter(requestStream))
                {
                    writer.Write(postData);
                    writer.Flush();
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
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