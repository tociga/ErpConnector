using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ErpConnector.Ax.DTO;
using System.Configuration;
using ErpConnector.Ax.Utils;
using ErpConnector.Common.Exceptions;
using System;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ErpConnector.Ax
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

        public static async Task<GenericWriteObject<T>> CallOdataEndpointPost<T>(string oDataEndpoint, string filters, T postDataObject)
        {
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["ax_base_url"];
            string endpoint = baseUrl + "/data/" + oDataEndpoint + ApplyCrossCompanyFilter(filters) ?? "";

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = Authenticator.GetAdalHeader();
            //request.Headers["Accept"] = "application/json;odata.metadata=none";
            //request.Headers["Content-Type"] = "application/json";

            request.Method = "POST";
            var postData = JsonConvert.SerializeObject(postDataObject, new EnumConverter());
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
        //    string baseUrl = System.Configuration.ConfigurationManager.AppSettings["ax_base_url"];
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
        public static async Task<AxBaseException> CallOdataEndpoint<T>(string oDataEndpoint, string filters, string dbTable, int actionId)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                var baseUrl = ConfigurationManager.AppSettings["ax_base_url"];
                var endpoint = baseUrl + "/data/" + oDataEndpoint + ApplyCrossCompanyFilter(filters) ?? "";

                var returnODataObject = await CallOdataEndpoint<T>(endpoint);
                if (returnODataObject.value.Any())
                {
                    DataWriter.TruncateSingleTable(dbTable);
                }
                DataWriter.WriteToTable<T>(returnODataObject.value.GetDataReader<T>(), dbTable);

                while (!string.IsNullOrEmpty(returnODataObject.NextLink))
                {
                    endpoint = returnODataObject.NextLink + ApplyCrossCompanyFilter(filters);
                    returnODataObject = await CallOdataEndpoint<T>(endpoint);
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
            catch(Exception e)
            {
                DataWriter.LogErpActionStep(actionId, dbTable, startTime, false, e.Message, e.StackTrace);
                return new AxBaseException { ApplicationException = e };
            }

        }

        public static async Task<AxBaseException> CallOdataEndpointWithPageSize<T>(string oDataEndpoint, int maxNumber, string dbTable, int actionId)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                var filter = "?$top=" + maxNumber;// 1000 &$top = 1000
                var baseUrl = ConfigurationManager.AppSettings["ax_base_url"];
                var endpoint = baseUrl + "/data/" + oDataEndpoint + ApplyCrossCompanyFilter(filter);

                var returnODataObject = await CallOdataEndpointAsync<T>(endpoint);
<<<<<<< HEAD
=======
                if (returnODataObject.value.Any())
                {
                    DataWriter.TruncateSingleTable(dbTable);
                }
>>>>>>> erp_listener_ax_lss
                DataWriter.WriteToTable<T>(returnODataObject.value.GetDataReader<T>(), dbTable);
                for(int i = 1; returnODataObject.value.Count > 0; i++)
                {
                    filter = "?$skip=" + i * maxNumber +"&$top=" + maxNumber;
                    endpoint = baseUrl + "/data/" + oDataEndpoint + ApplyCrossCompanyFilter(filter);
                    returnODataObject = await CallOdataEndpointAsync<T>(endpoint);
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
        private static async Task<GenericJsonOdata<T>> CallOdataEndpointAsync<T>(string requestUri)
        {
            HttpClient client = new HttpClient();
            string token = Authenticator.GetAdalToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
        private static async Task<GenericJsonOdata<T>> CallOdataEndpoint<T>(string requestUri)
        {            
            var request =(HttpWebRequest)HttpWebRequest.Create(requestUri);
            request.Accept = "application/json;odata.metadata=none";
            string token = Authenticator.GetAdalHeader();
            request.Headers["Authorization"] = token;
            request.Method = "GET";
            request.Timeout = 1000 * 60 * 2; // we set it to 2 minutes.
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
                            //string sanitized = SanitizeJsonString(responseString);
                            result = JsonConvert.DeserializeObject<GenericJsonOdata<T>>(responseString);
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

        private static async Task<GenericJsonOdata<T>> CallAGRServiceArray<T>(string service, string serviceMethod, string postData, string serviceGroup)
        {
            var baseUrl = System.Configuration.ConfigurationManager.AppSettings["ax_base_url"];
            serviceGroup = serviceGroup ?? System.Configuration.ConfigurationManager.AppSettings["StandardServiceGroup"];
            var endpoint = baseUrl + "/api/services/" + serviceGroup + "/" + service + "/" + serviceMethod+ApplyCrossCompanyFilter("");

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = Authenticator.GetAdalHeader();
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

        public static async Task<T> CallAGRServiceScalar<T>(string service, string serviceMethod, string postData, string adalHeader)
        {
            var baseUrl = ConfigurationManager.AppSettings["ax_base_url"];
            var standardServiceGroup = ConfigurationManager.AppSettings["StandardServiceGroup"];
            var endpoint = baseUrl + "/api/services/" + standardServiceGroup + "/" + service + "/" + serviceMethod + ApplyCrossCompanyFilter("");

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = adalHeader;
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

<<<<<<< HEAD
        private static GenericJsonOdata<T> WriteFromService<T>(Int64 recId, Int64 pageSize, string webMethod, string serviceName, string destTable, DateTime minDate, DateTime maxDate, bool useDate = false)
=======
        private static GenericJsonOdata<T> WriteFromService<T>(Int64 recId, Int64 pageSize, string webMethod, string serviceName, DateTime minDate, DateTime maxDate, bool useDate = false)
>>>>>>> erp_listener_ax_lss
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
            var result = ServiceConnector.CallAGRServiceArray<T>(serviceName, webMethod, sb.ToString(), null);

<<<<<<< HEAD
            var reader = result.Result.value.GetDataReader();

            DataWriter.WriteToTable<T>(reader, destTable);
=======
            //var reader = result.Result.value.GetDataReader();

            //DataWriter.WriteToTable<T>(reader, destTable);
>>>>>>> erp_listener_ax_lss

            return result.Result;
        }

        public static AxBaseException CallService<T>(int actionId, string webMethod, string serviceName,  string dbTable, int pageSize)
        {
            DateTime startTime = DateTime.Now;
            try
            {
<<<<<<< HEAD
                long recId = DataWriter.GetMaxRecId(dbTable);
                GenericJsonOdata<T> result = new GenericJsonOdata<T>();
                bool firstRound = true;
                while (firstRound || (result.value.Any() && result.Exception == null))
                {
                    result = ServiceConnector.WriteFromService<T>(recId, pageSize, webMethod, serviceName,  dbTable, DateTime.MinValue, DateTime.MinValue, false);
                    recId = DataWriter.GetMaxRecId(dbTable);
                    firstRound = false;
=======
                long recId = 0;
                GenericJsonOdata<T> result = new GenericJsonOdata<T>();
                result = WriteFromService<T>(recId, pageSize, webMethod, serviceName, DateTime.MinValue, DateTime.MinValue, false);
                if (result.value.Any())
                {
                    DataWriter.TruncateSingleTable(dbTable);
                }                
                while (result.value.Any() && result.Exception == null)
                {
                    result = WriteFromService<T>(recId, pageSize, webMethod, serviceName, DateTime.MinValue, DateTime.MinValue, false);
                    DataWriter.WriteToTable<T>(result.value.GetDataReader(), dbTable);
                    recId = DataWriter.GetMaxRecId(dbTable);                    
>>>>>>> erp_listener_ax_lss
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

        public static AxBaseException CallServiceByDate<T>(DateTime date, int actionId, string webMethod, string serviceName,  string dbTable, Func<DateTime, DateTime> nextPeriod = null )
        {
            if (nextPeriod == null)
            {
                nextPeriod = AddDay;
            }
            DateTime startTime = DateTime.Now;
            try
            {
                GenericJsonOdata<T> result = new GenericJsonOdata<T>();
<<<<<<< HEAD
                for (DateTime d = date.Date; d <= DateTime.Now.Date && result.Exception == null; d = nextPeriod(d))
                {
                    result = ServiceConnector.WriteFromService<T>(0, 10000, webMethod, serviceName,  dbTable, d, nextPeriod(d), true);
=======
                bool firstResult = false;
                for (DateTime d = date.Date; d <= DateTime.Now.Date && result.Exception == null; d = nextPeriod(d))
                {
                    result = ServiceConnector.WriteFromService<T>(0, 10000, webMethod, serviceName, d, nextPeriod(d), true);
                    if (!firstResult && result.value.Any())
                    {
                        DataWriter.TruncateSingleTable(dbTable);
                        firstResult = true;
                    }
                    DataWriter.WriteToTable<T>(result.value.GetDataReader(), dbTable);
>>>>>>> erp_listener_ax_lss
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