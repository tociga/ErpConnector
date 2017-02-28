﻿using Newtonsoft.Json;
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

namespace AxConnect
{
    public class AXServiceConnector
    {
        private static string header = "";

        public AXServiceConnector()
        {
            Authorize().Wait();
        }
        private Task Authorize()
        {
            return Task.Run(() => {
                WithoutADAL().Wait();
                //AdalAuthenticate().Wait();
            });
        }
        private static async Task WithoutADAL()
        {
            var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("resource", "https://agrax7u2devaos.cloudax.dynamics.com"),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", "dadi@reynd.is"),
                new KeyValuePair<string, string>("password", "ZiK289dt"),
                new KeyValuePair<string, string>("client_id", "b15e9fb9-68ba-4bec-8ce5-f1094689a573")
            };

            using (var client = new HttpClient())
            {

                string baseUrl = "https://login.windows.net/reynd.is/oauth2/";
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new FormUrlEncodedContent(postData);

                HttpResponseMessage response = await client.PostAsync("token", content);
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<dynamic>(jsonString);
                header = responseData.token_type + " " + responseData.access_token;
                //return jsonString;
            }
        }

        public static async Task<DTO.GenericJsonOdata<T>> CallOdataEndpoint<T>(string oDataEndpoint, string filters, string adalHeader)
        {
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["ax_base_url"];
            string endpoint = baseUrl + "/data/" + oDataEndpoint + filters??"";

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = adalHeader;

            request.Method = "GET";
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
                        return JsonConvert.DeserializeObject<DTO.GenericJsonOdata<T>>(responseString);

                    }
                }
            }
        }
        public async Task<List<T>> CallAGRServiceArray<T>(string service, string serviceMethod, string postData)
        {
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["ax_base_url"];
            string standardServiceGroup = System.Configuration.ConfigurationManager.AppSettings["StandardServiceGroup"];
            string endpoint = baseUrl + "/api/services/" + standardServiceGroup + "/" + service + "/" + serviceMethod;

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = header;
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
                        return JsonConvert.DeserializeObject<List<T>>(responseString);
                      
                    }
                }
            }
        }

        public async Task<T> CallAGRServiceScalar<T>(string service, string serviceMethod, string postData)
        {
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["ax_base_url"];
            string standardServiceGroup = System.Configuration.ConfigurationManager.AppSettings["StandardServiceGroup"];
            string endpoint = baseUrl + "/api/services/" + standardServiceGroup + "/" + service + "/" + serviceMethod;

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = header;
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
        public static string SanitizeJsonString(string json )
        {
            string pattern = "\"$id\" : \"\\d+\",";
            Regex regex = new Regex(pattern);
            return regex.Replace(json, "");
        }

    }
}
