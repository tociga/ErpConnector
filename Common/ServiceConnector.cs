using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ErpConnector.Common.DTO;
using System.Configuration;
using ErpConnector.Common.Exceptions;
using System;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using ErpConnector.Common.Util;

namespace ErpConnector.Common
{
    public class ServiceConnector
    {
        public static bool IsCrossCompany
        {
            get
            {
                bool crossCompany = false;
                Boolean.TryParse(ConfigurationManager.AppSettings["CrossCompany"], out crossCompany);
                return crossCompany;
            }
        }

        private static string ApplyCrossCompanyFilter(string filter)
        {
            if (IsCrossCompany)
            {
                if (string.IsNullOrEmpty(filter))
                {
                    return "?cross_company=true";
                }
                else
                {
                    return filter + "&?cross_company=true";
                }
            }
            return filter;
        }

        public static async Task<GenericWriteObject<T>> CallOdataEndpointPost<T, Y>(string oDataEndpoint, string filters, T postDataObject, ServiceData authData) where Y : JsonConverter
        {
            string baseUrl = authData.BaseUrl;
            string endpoint = baseUrl + "/data/" + oDataEndpoint + ApplyCrossCompanyFilter(filters) ?? "";

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = authData.AuthHeader;
            //request.Headers["Accept"] = "application/json;odata.metadata=none";
            //request.Headers["Content-Type"] = "application/json";

            request.Method = "POST";
            var postData = JsonConvert.SerializeObject(postDataObject, Activator.CreateInstance<Y>());
            //request.ContentLength = postData != null ? postData.Length : 0;
            request.ContentType = "application/json";

            GenericWriteObject<T> result = new GenericWriteObject<T>();
            try
            {
                using (var requestStream = await request.GetRequestStreamAsync())
            {
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(postData);
                    writer.Flush();
                }
            }

                using (var response = (HttpWebResponse)(await request.GetResponseAsync()))
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(responseStream))
                        {
                            var responseString = streamReader.ReadToEnd();
                            //string sanitized = SanitizeJsonString(responseString);

                            result.WriteObject = JsonConvert.DeserializeObject<T>(responseString);
                            return result;
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
                        result.Exception = JsonConvert.DeserializeObject<AxBaseException>(reader.ReadToEnd());
                        // TODO: Need to log error;
                        return result;
                    }
                }
            }
            catch(Exception e)
            {
                if (e is AggregateException)
                {
                    return new GenericWriteObject<T> { Exception = new AxBaseException { ApplicationException = e.InnerException } };
                }
                else
                {
                    return new GenericWriteObject<T> { Exception = new AxBaseException { ApplicationException = e } };
                }
            }
        }



        //public static async Task<T> CreateEntity<T>(string oDataEndpoint, string filters, T postDataObject, List<string> errorMessage)
        //{
        //    string baseUrl = System.Configuration.ConfigurationManager.AppSettings["base_url"];
        //    string endpoint = baseUrl + "/data/" + oDataEndpoint + filters ?? "";

        //    var request = HttpWebRequest.Create(endpoint);
        //    request.Headers["Authorization"] = Authenticator.GetAdalToken();
        //    //request.Headers["Content-Type"] = "application/json";

        //    request.Method = "POST";
        //    var postData = JsonConvert.SerializeObject(postDataObject, new EnumConverter());
        //    request.ContentLength = postData != null ? postData.Length : 0;
        //    request.ContentType = "application/json";

        //    using (var requestStream = request.GetRequestStream())
        //    {
        //        using (var writer = new StreamWriter(requestStream))
        //        {
        //            writer.Write(postData);
        //            writer.Flush();
        //        }
        //    }

        //    try
        //    {
        //        using (var response = (HttpWebResponse)request.GetResponse())
        //        {
        //            using (var responseStream = response.GetResponseStream())
        //            {
        //                using (var streamReader = new StreamReader(responseStream))
        //                {
        //                    var responseString = streamReader.ReadToEnd();
        //                    //string sanitized = SanitizeJsonString(responseString);

        //                    return JsonConvert.DeserializeObject<T>(responseString);
        //                    //return responseString;
        //                }
        //            }
        //        }
        //    }
        //    catch (WebException e)
        //    {
        //        using (var rStream = e.Response.GetResponseStream())
        //        {
        //            using (var reader = new StreamReader(rStream))
        //            {
        //                var r = reader.ReadToEnd();

        //                // TODO: Need to log error;
        //                errorMessage.Add(r);
        //                return default(T);
        //            }
        //        }
        //    }
        //}

        //public static async Task<AxBaseException> CallOdataEndpointTask<T>
        //public static async Task<AxBaseException> CallOdataEndpointArray<T>(string oDataEndpoint, string filters, string dbTable, int actionId, ServiceData authData)
        //{
        //    DateTime startTime = DateTime.Now;
        //    try
        //    {
        //        var baseUrl = authData.BaseUrl;
        //        var endpoint = baseUrl + authData.OdataUrlPostFix + oDataEndpoint + ApplyCrossCompanyFilter(filters) ?? "";

        //        var returnODataObject = await CallOdataEndpoint<T, Y>(endpoint, authData);
        //        DataWriter.WriteToTable<Y>(returnODataObject.value.GetDataReader<Y>(), dbTable);

        //        if (returnODataObject.Exception != null)
        //        {
        //            DataWriter.LogErpActionStep(actionId, dbTable, startTime, false, returnODataObject.Exception.ErrorMessage, returnODataObject.Exception.StackTrace);
        //        }
        //        else
        //        {
        //            DataWriter.LogErpActionStep(actionId, dbTable, startTime, true, null, null);
        //        }
        //        return returnODataObject.Exception;
        //    }
        //    catch (Exception e)
        //    {
        //        DataWriter.LogErpActionStep(actionId, dbTable, startTime, false, e.Message, e.StackTrace);
        //        return new AxBaseException { ApplicationException = e };
        //    }

        //}

        public static async Task<AxBaseException> CallOdataEndpoint<T, Y>(string oDataEndpoint, string filters, string dbTable, int actionId, ServiceData authData) where T : GenericJsonOdata<Y>
        {
            DateTime startTime = DateTime.Now;
            try
            {
                var baseUrl = authData.BaseUrl;
                var endpoint = baseUrl + authData.OdataUrlPostFix + oDataEndpoint + ApplyCrossCompanyFilter(filters) ?? "";

                var returnODataObject = await CallOdataEndpoint<T, Y>(endpoint, authData);
                DataWriter.WriteToTable<Y>(returnODataObject.value.GetDataReader<Y>(), dbTable);

                while (!string.IsNullOrEmpty(returnODataObject.NextLink))
                {
                    endpoint = returnODataObject.NextLink + ApplyCrossCompanyFilter(filters);
                    returnODataObject = await CallOdataEndpoint<T, Y>(endpoint, authData);
                    DataWriter.WriteToTable<Y>(returnODataObject.value.GetDataReader<Y>(), dbTable);
                    if (returnODataObject.Exception != null)
                    {
                        break;
                    }
                }
                if (returnODataObject.Exception != null)
                {
                    DataWriter.LogErpActionStep(actionId, dbTable, startTime, false, returnODataObject.Exception.ErrorMessage, returnODataObject.Exception.StackTrace);
                }
                else
                {
                    DataWriter.LogErpActionStep(actionId, dbTable, startTime, true, null, null);
                }
                return returnODataObject.Exception;
            }
            catch(Exception e)
            {
                DataWriter.LogErpActionStep(actionId, dbTable, startTime, false, e.Message, e.StackTrace);
                return new AxBaseException { ApplicationException = e };
            }

        }

        public static async Task<AxBaseException> CallOdataEndpointWithPageSize<T>(string oDataEndpoint, int maxNumber, string dbTable, int actionId, ServiceData authData)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                var filter = "?$top=" + maxNumber;// 1000 &$top = 1000
                var baseUrl = authData.BaseUrl;
                var endpoint = baseUrl + "/data/" + oDataEndpoint + ApplyCrossCompanyFilter(filter);

                var returnODataObject = await CallOdataEndpointAsync<T>(endpoint, authData);
                DataWriter.WriteToTable<T>(returnODataObject.value.GetDataReader<T>(), dbTable);
                for(int i = 1; returnODataObject.value.Count > 0; i++)
                {
                    filter = "?$skip=" + i * maxNumber +"&$top=" + maxNumber;
                    endpoint = baseUrl + "/data/" + oDataEndpoint + ApplyCrossCompanyFilter(filter);
                    returnODataObject = await CallOdataEndpointAsync<T>(endpoint, authData);
                    DataWriter.WriteToTable<T>(returnODataObject.value.GetDataReader<T>(), dbTable);
                    if (returnODataObject.Exception != null)
                    {
                        break;
                    }
                }
                if (returnODataObject.Exception != null)
                {
                    DataWriter.LogErpActionStep(actionId, dbTable, startTime, false, returnODataObject.Exception.ErrorMessage, returnODataObject.Exception.StackTrace);
                }
                else
                {
                    DataWriter.LogErpActionStep(actionId, dbTable, startTime, true, null, null);
                }
                return returnODataObject.Exception;
            }
            catch (Exception e)
            {
                DataWriter.LogErpActionStep(actionId, dbTable, startTime, false, e.Message, e.StackTrace);
                return new AxBaseException { ApplicationException = e };
            }

        }
        private static async Task<GenericJsonOdata<T>> CallOdataEndpointAsync<T>(string requestUri, ServiceData authData)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authData.AuthToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(0, 3, 0);
            var result = new GenericJsonOdata<T>();
            try
            {
                using (var response = await client.GetAsync(requestUri))
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<GenericJsonOdata<T>>(responseString);
                    return result;
                }
            }
            catch (WebException e)
            {
                using (var rStream = e.Response.GetResponseStream())
                {
                    using (var reader = new StreamReader(rStream))
                    {
                        result.Exception = JsonConvert.DeserializeObject<AxBaseException>(reader.ReadToEnd());
                        return result;
                    }
                }
            }
            catch(TaskCanceledException)
            {
                return new GenericJsonOdata<T>
                {
                    Exception = new AxBaseException { ApplicationException = new ApplicationException("Timeout Expired for " + requestUri) }
                };
            }
        }
        private static async Task<T> CallOdataEndpoint<T, Y>(string requestUri, ServiceData authData) where T : GenericJsonOdata<Y>
        {            
            var request =(HttpWebRequest)HttpWebRequest.Create(requestUri);
            request.Accept = "application/json;odata.metadata=none";            
            request.Headers["Authorization"] = authData.AuthHeader;
            request.Method = "GET";
            request.Timeout = 1000 * 60 * 2; // we set it to 2 minutes.
            var result = Activator.CreateInstance<T>();
            try
            {
                using (var response = (HttpWebResponse)(await request.GetResponseAsync()))
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(responseStream))
                        {
                            var responseString = streamReader.ReadToEnd();
                            if (authData.AuthType == ErpTasks.ErpTaskStep.AuthenticationType.JIRA)
                            {
                                var resultArray = JsonConvert.DeserializeObject<List<Y>>(responseString);
                                result.value = resultArray;
                            }
                            else
                            {
                                result = JsonConvert.DeserializeObject<T>(responseString);
                            }
                            return result;
                        }
                    }
                }
            }
            catch(WebException e)
            {
                using (var rStream = e.Response.GetResponseStream())
                {
                    using (var reader = new StreamReader(rStream))
                    {
                        result.Exception = JsonConvert.DeserializeObject<AxBaseException>(reader.ReadToEnd());
                        // TODO: Need to log error;
                        return result;
                    }
                }
            }
        }


        private static async Task<GenericJsonOdata<T>> CallAGRServiceArray<T>(string service, string serviceMethod, string postData, string serviceGroup, ServiceData authData)
        {
            var baseUrl = authData.BaseUrl;
            serviceGroup = serviceGroup ?? System.Configuration.ConfigurationManager.AppSettings["StandardServiceGroup"];
            var endpoint = baseUrl + "/api/services/" + serviceGroup + "/" + service + "/" + serviceMethod+ApplyCrossCompanyFilter("");

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = authData.AuthHeader;
            //request.Headers["Content-Type"] = "application/json";
            request.Method = "POST";
            request.ContentLength = postData != null ? postData.Length : 0;
            System.Diagnostics.Debug.Write("Endpoint :" + endpoint+ " ");
            System.Diagnostics.Debug.WriteLine("postData: " + postData);

            if (request.ContentLength > 0)
            {
                using (var requestStream = await request.GetRequestStreamAsync())
                {
                    using (var writer = new StreamWriter(requestStream))
                    {
                        writer.Write(postData);
                        writer.Flush();
                    }
                }
            }

            var result = new GenericJsonOdata<T>();
            try
            {
                using (var response = (HttpWebResponse)(await request.GetResponseAsync()))
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(responseStream))
                        {
                            var responseString = streamReader.ReadToEnd();
                            result.value = JsonConvert.DeserializeObject<List<T>>(responseString);

                            return result;

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
                        result.Exception = JsonConvert.DeserializeObject<AxBaseException>(r);
                        return result;
                    }
                }
            }

        }

        public static async Task<T> CallAGRServiceScalar<T>(string service, string serviceMethod, string postData, ServiceData authData)
        {
            var baseUrl = authData.BaseUrl; ;
            var standardServiceGroup = ConfigurationManager.AppSettings["StandardServiceGroup"];
            var endpoint = baseUrl + "/api/services/" + standardServiceGroup + "/" + service + "/" + serviceMethod + ApplyCrossCompanyFilter("");

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = authData.AuthHeader;
            request.Method = "POST";
            request.ContentLength = postData.Length;

            using (var requestStream = await request.GetRequestStreamAsync())
            {
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(postData);
                    writer.Flush();
                }
            }

            using (var response = (HttpWebResponse)(await request.GetResponseAsync()))
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

        private static GenericJsonOdata<T> WriteFromService<T>(Int64 recId, Int64 pageSize, string webMethod, string serviceName, string destTable, 
            DateTime minDate, DateTime maxDate, ServiceData authData, bool useDate = false)
        {
            StringBuilder sb = new StringBuilder();
            if (useDate)
            {
                sb.Append("{\"firstDate\" : \"" + minDate.ToString("yyyy-MM-dd HH:mm:ss") + "\"");
                sb.Append(", \"lastDate\" : \"" + maxDate.ToString("yyyy-MM-dd HH:mm:ss") + "\"}");
            }
            else
            {
                sb.Append("{ \"lastRecId\": " + recId.ToString() + ", \"pageSize\" : " + (pageSize).ToString() + " }");
            }
            var result = ServiceConnector.CallAGRServiceArray<T>(serviceName, webMethod, sb.ToString(), null, authData);

            var reader = result.Result.value.GetDataReader();

            DataWriter.WriteToTable<T>(reader, destTable);

            return result.Result;
        }

        public static AxBaseException CallService<T>(int actionId, string webMethod, string serviceName,  string dbTable, int pageSize, ServiceData authData)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                long recId = DataWriter.GetMaxRecId(dbTable);
                GenericJsonOdata<T> result = new GenericJsonOdata<T>();
                bool firstRound = true;
                while (firstRound || (result.value.Any() && result.Exception == null))
                {
                    result = ServiceConnector.WriteFromService<T>(recId, pageSize, webMethod, serviceName,  dbTable, DateTime.MinValue, DateTime.MinValue, authData, false);
                    recId = DataWriter.GetMaxRecId(dbTable);
                    firstRound = false;
                }

                if (result.Exception == null)
                {
                    DataWriter.LogErpActionStep(actionId,  dbTable, startTime, true, null, null);
                }
                else
                {
                    DataWriter.LogErpActionStep(actionId, dbTable, startTime, false, result.Exception.ErrorMessage, result.Exception.StackTrace);
                }
                return result.Exception;
            }
            catch (Exception e)
            {
                DataWriter.LogErpActionStep(actionId, dbTable, startTime, false, e.Message, e.StackTrace);
                return new AxBaseException { ApplicationException = e };
            }
        }

        public static AxBaseException CallServiceByDate<T>(DateTime date, int actionId, string webMethod, string serviceName,  string dbTable, ServiceData authData, Func<DateTime, DateTime> nextPeriod = null )
        {
            if (nextPeriod == null)
            {
                nextPeriod = AddDay;
            }
            DateTime startTime = DateTime.Now;
            try
            {
                GenericJsonOdata<T> result = new GenericJsonOdata<T>();
                for (DateTime d = date.Date; d <= DateTime.Now.Date && result.Exception == null; d = nextPeriod(d))
                {
                    result = ServiceConnector.WriteFromService<T>(0, 10000, webMethod, serviceName,  dbTable, d, nextPeriod(d), authData, true);
                }
                if (result.Exception == null)
                {
                    DataWriter.LogErpActionStep(actionId, dbTable, startTime, true, null, null);
                }
                else
                {
                    DataWriter.LogErpActionStep(actionId, dbTable, startTime, false, result.Exception.ErrorMessage, result.Exception.StackTrace);
                }
                return result.Exception;
            }
            catch (Exception e)
            {
                DataWriter.LogErpActionStep(actionId, dbTable, startTime, false, e.Message, e.StackTrace);
                return new AxBaseException { ApplicationException = e };
            }

        }

        public static DateTime AddDay(DateTime d)
        {
            return d.AddDays(1);
        }


    }
}