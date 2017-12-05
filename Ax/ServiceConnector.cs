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

namespace ErpConnector.Ax
{
    public class ServiceConnector
    {
        public static async Task<GenericWriteObject<T>> CallOdataEndpointPost<T>(string oDataEndpoint, string filters, T postDataObject)
        {
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["ax_base_url"];
            string endpoint = baseUrl + "/data/" + oDataEndpoint + filters ?? "";

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = Authenticator.GetAdalToken();
            //request.Headers["Accept"] = "application/json;odata.metadata=none";
            //request.Headers["Content-Type"] = "application/json";

            request.Method = "POST";
            var postData = JsonConvert.SerializeObject(postDataObject, new EnumConverter());
            request.ContentLength = postData != null ? postData.Length : 0;
            request.ContentType = "application/json";

            GenericWriteObject<T> result = new GenericWriteObject<T>();
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
        public static async Task<AxBaseException> CallOdataEndpoint<T>(string oDataEndpoint, string filters, string dbTable, int actionId)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                var baseUrl = ConfigurationManager.AppSettings["ax_base_url"];
                var endpoint = baseUrl + "/data/" + oDataEndpoint + filters ?? "";

                var returnODataObject = await CallOdataEndpoint<T>(endpoint);
                DataWriter.WriteToTable<T>(returnODataObject.value.GetDataReader<T>(), dbTable);

                while (!string.IsNullOrEmpty(returnODataObject.NextLink))
                {
                    returnODataObject = await CallOdataEndpoint<T>(returnODataObject.NextLink);
                    DataWriter.WriteToTable<T>(returnODataObject.value.GetDataReader<T>(), dbTable);
                    if (returnODataObject.Exception != null)
                    {
                        break;
                    }
                }
                DataWriter.LogErpActionStep(actionId, dbTable, startTime, true);
                return returnODataObject.Exception;
            }
            catch(Exception e)
            {
                DataWriter.LogErpActionStep(actionId, dbTable, startTime, false);
                return new AxBaseException { ApplicationException = e };
            }

        }

        public static async Task<AxBaseException> CallOdataEndpoint<T>(string oDataEndpoint, int maxNumber, string dbTable, int actionId)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                var filter = "?$top=" + maxNumber;// 1000 &$top = 1000
                var baseUrl = ConfigurationManager.AppSettings["ax_base_url"];
                var endpoint = baseUrl + "/data/" + oDataEndpoint + filter;

                var returnODataObject = await CallOdataEndpoint<T>(endpoint);
                DataWriter.WriteToTable<T>(returnODataObject.value.GetDataReader<T>(), dbTable);
                for(int i = 1; returnODataObject.value.Count > 0; i++)
                {
                    filter = "?$skip=" + i * maxNumber +"&$top=" + maxNumber;
                    endpoint = baseUrl + "/data/" + oDataEndpoint + filter;
                    returnODataObject = await CallOdataEndpoint<T>(endpoint);
                    DataWriter.WriteToTable<T>(returnODataObject.value.GetDataReader<T>(), dbTable);
                    if (returnODataObject.Exception != null)
                    {
                        break;
                    }
                }
                DataWriter.LogErpActionStep(actionId, dbTable, startTime, true);
                return returnODataObject.Exception;
            }
            catch (Exception e)
            {
                DataWriter.LogErpActionStep(actionId, dbTable, startTime, false);
                return new AxBaseException { ApplicationException = e };
            }

        }
        private static async Task<GenericJsonOdata<T>> CallOdataEndpoint<T>(string requestUri)
        {            
            var request =(HttpWebRequest)HttpWebRequest.Create(requestUri);
            request.Accept = "application/json;odata.metadata=none";
            string token = Authenticator.GetAdalToken();
            request.Headers["Authorization"] = token;
            request.Method = "GET";
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
            var endpoint = baseUrl + "/api/services/" + serviceGroup + "/" + service + "/" + serviceMethod;

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = Authenticator.GetAdalToken();
            //request.Headers["Content-Type"] = "application/json";
            request.Method = "POST";
            request.ContentLength = postData != null ? postData.Length : 0;
            System.Diagnostics.Debug.Write("Endpoint :" + endpoint+ " ");
            System.Diagnostics.Debug.WriteLine("postData: " + postData);

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

            var result = new GenericJsonOdata<T>();
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
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

        private static GenericJsonOdata<T> WriteFromService<T>(Int64 recId, Int64 pageSize, string webMethod, string serviceName, string dbSchema, string destTable, DateTime minDate, DateTime maxDate, bool useDate = false)
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

            var reader = result.Result.value.GetDataReader();

            DataWriter.WriteToTable<T>(reader, dbSchema+ "." + destTable);

            return result.Result;
        }

        public static AxBaseException CallService<T>(int actionId, string webMethod, string serviceName, string dbSchema, string dbTable, int pageSize)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                long recId = DataWriter.GetMaxRecId(dbSchema, dbTable);
                GenericJsonOdata<T> result = new GenericJsonOdata<T>();
                bool firstRound = true;
                while (firstRound || (result.value.Any() && result.Exception == null))
                {
                    result = ServiceConnector.WriteFromService<T>(recId, pageSize, webMethod, serviceName, dbSchema, dbTable, DateTime.MinValue, DateTime.MinValue, false);
                    recId = DataWriter.GetMaxRecId(dbSchema, dbTable);
                    firstRound = false;
                }

                if (result.Exception == null)
                {
                    DataWriter.LogErpActionStep(actionId, dbSchema + "." + dbTable, startTime, true);
                }
                else
                {
                    DataWriter.LogErpActionStep(actionId, dbSchema + "." + dbTable, startTime, false);
                }
                return result.Exception;
            }
            catch (Exception e)
            {
                DataWriter.LogErpActionStep(actionId, dbSchema + "." + dbTable, startTime, false);
                return new AxBaseException { ApplicationException = e };
            }
        }

        public static AxBaseException CallServiceByDate<T>(DateTime date, int actionId, string webMethod, string serviceName, string dbSchema, string dbTable, Func<DateTime, DateTime> nextPeriod = null )
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
                    result = ServiceConnector.WriteFromService<T>(0, 10000, webMethod, serviceName, dbSchema, dbTable, d, nextPeriod(d), true);
                }
                if (result.Exception == null)
                {
                    DataWriter.LogErpActionStep(actionId, "[ax].[PurchLine_Increment]", startTime, true);
                }
                else
                {
                    DataWriter.LogErpActionStep(actionId, "[ax].[PurchLine_Increment]", startTime, false);
                }
                return result.Exception;
            }
            catch (Exception e)
            {
                DataWriter.LogErpActionStep(actionId, "[ax].[PurchLine_Increment]", startTime, false);
                return new AxBaseException { ApplicationException = e };
            }

        }

        public static DateTime AddDay(DateTime d)
        {
            return d.AddDays(1);
        }


    }
}