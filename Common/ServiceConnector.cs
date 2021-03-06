﻿using Newtonsoft.Json;
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
using System.Dynamic;
using ErpConnector.Common.ErpTasks;
using System.Reflection;
using System.Collections;

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

        public static async Task<AxBaseException> CallOdataEndpointPut(string oDataEndpoint, string filters, string putDataStr, ServiceData authData)
        {
            string baseUrl = authData.BaseUrl;
            string endpoint = baseUrl + authData.OdataUrlPostFix + oDataEndpoint;

            var request = HttpWebRequest.Create(endpoint);
            request.Headers["Authorization"] = authData.AuthHeader;
            //request.Headers["Accept"] = "application/json;odata.metadata=none";
            //request.Headers["Content-Type"] = "application/json";

            request.Method = "PUT";           
            //request.ContentLength = postData != null ? postData.Length : 0;
            request.ContentType = "application/json";

            try
            {
                using (var requestStream = await request.GetRequestStreamAsync())
                {
                    using (var writer = new StreamWriter(requestStream))
                    {
                        writer.Write(putDataStr);
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
                            
                            return null;
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

                        // TODO: Need to log error;
                        string error = reader.ReadToEnd();
                        return JsonConvert.DeserializeObject<AxBaseException>(error);
                    }
                }
            }
            catch (Exception e)
            {
                if (e is AggregateException)
                {
                    return new AxBaseException { ApplicationException = e.InnerException };
                }
                else
                {
                    return new AxBaseException { ApplicationException = e };
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

        public static async Task<AxBaseException> CallOdataEndpoint<T,Y>(ErpTaskStep step, int actionId, ServiceData authData) where T: GenericJsonOdata<Y>
        {
            DateTime startTime = DateTime.Now;
            try
            {
                var baseUrl = authData.BaseUrl;
                var endpoint = baseUrl + authData.OdataUrlPostFix + step.EndPoint + ApplyCrossCompanyFilter(step.EndpointFilter) ?? "";

                var returnODataObject = await CallOdataEndpointAsync<Y>(endpoint, authData);
				if (returnODataObject.value.Any())
                {
                    DataWriter.TruncateSingleTable(step.DbTable);
                }
                DataWriter.WriteToTable<Y>(returnODataObject.value.GetDataReader<Y>(), step.DbTable, authData.InjectionPropertyValue, authData.InjectionPropertyName);
                string nextLinkEndpoint = null;
                while (!string.IsNullOrEmpty(returnODataObject.NextLink))
                {
                    if (returnODataObject.appendNextLink)
                    {
                        nextLinkEndpoint = endpoint + returnODataObject.NextLink;
                    }
                    else
                    {
                        nextLinkEndpoint = returnODataObject.NextLink;
                    }
                    returnODataObject = await CallOdataEndpointAsync<Y>(nextLinkEndpoint, authData);
                    DataWriter.WriteToTable<Y>(returnODataObject.value.GetDataReader<Y>(), step.DbTable, authData.InjectionPropertyValue, authData.InjectionPropertyName);
                    if (returnODataObject.Exception != null)
                    {
                        break;
                    }
                }
                if (returnODataObject.Exception != null)
                {
                    DataWriter.LogErpActionStep(actionId, step.DbTable, startTime, false, returnODataObject.Exception.ErrorMessage, returnODataObject.Exception.StackTrace, step.Id);
                }
                else
                {
                    DataWriter.LogErpActionStep(actionId, step.DbTable, startTime, true, null, null,step.Id);
                }
                return returnODataObject.Exception;
            }
            catch(Exception e)
            {
                DataWriter.LogErpActionStep(actionId, step.DbTable, startTime, false, e.Message, e.StackTrace, step.Id);
                return new AxBaseException { ApplicationException = e };
            }

        }

        public static async Task<AxBaseException> CallOdataEndpointWithPageSize<T>(ErpTaskStep step, int actionId, ServiceData authData)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                var filter = "?$top=" + step.MaxPageSize;// 1000 &$top = 1000
                var baseUrl = authData.BaseUrl;
                var endpoint = baseUrl + "/data/" + step.EndPoint + ApplyCrossCompanyFilter(filter);

                var returnODataObject = await CallOdataEndpointAsync<T>(endpoint, authData);
                if (returnODataObject.value.Any())
                {
                    DataWriter.TruncateSingleTable(step.DbTable);
                }
                DataWriter.WriteToTable<T>(returnODataObject.value.GetDataReader<T>(), step.DbTable);
                for(int i = 1; returnODataObject.value.Count > 0; i++)
                {
                    filter = "?$skip=" + i * step.MaxPageSize +"&$top=" + step.MaxPageSize;
                    endpoint = baseUrl + "/data/" + step.EndPoint + ApplyCrossCompanyFilter(filter);
                    returnODataObject = await CallOdataEndpointAsync<T>(endpoint, authData);
                    DataWriter.WriteToTable<T>(returnODataObject.value.GetDataReader<T>(), step.DbTable, authData.InjectionPropertyValue, authData.InjectionPropertyName);
                    if (returnODataObject.Exception != null)
                    {
                        break;
                    }
                }
                if (returnODataObject.Exception != null)
                {
                    DataWriter.LogErpActionStep(actionId, step.DbTable, startTime, false, returnODataObject.Exception.ErrorMessage, returnODataObject.Exception.StackTrace, step.Id);
                }
                else
                {
                    DataWriter.LogErpActionStep(actionId, step.DbTable, startTime, true, null, null, step.Id);
                }
                return returnODataObject.Exception;
            }
            catch (Exception e)
            {
                DataWriter.LogErpActionStep(actionId, step.DbTable, startTime, false, e.Message, e.StackTrace, step.Id);
                return new AxBaseException { ApplicationException = e };
            }

        }
        private static async Task<GenericJsonOdata<T>> CallOdataEndpointAsync<T>(string requestUri, ServiceData authData)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authData.AuthMethod, authData.AuthToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(0, 3, 0);
            var result = new GenericJsonOdata<T>();
            try
            {
                using (var response = await client.GetAsync(requestUri))
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        if (authData.CustomJsonConverter != null)
                        {
                            result = JsonConvert.DeserializeObject<GenericJsonOdata<T>>(responseString, authData.CustomJsonConverter);
                        }
                        else
                        {
                            result = JsonConvert.DeserializeObject<GenericJsonOdata<T>>(responseString);
                        }
                        return result;
                    }
                    else
                    {
                        return new GenericJsonOdata<T>
                        {
                            Exception = new AxBaseException { ApplicationException = new ApplicationException(responseString) }
                        };
                    }
                }
            }
            catch (ArgumentNullException ne)
            {
                return new GenericJsonOdata<T>
                {
                    Exception = new AxBaseException { ApplicationException = ne }
                };
            }
            catch (TaskCanceledException)
            {
                return new GenericJsonOdata<T>
                {
                    Exception = new AxBaseException { ApplicationException = new ApplicationException("Timeout Expired for " + requestUri) }
                };
            }
            catch (Exception e)
            {
                return new GenericJsonOdata<T>
                {
                    Exception = new AxBaseException { ApplicationException = e }
                };
            }

        }
        //private static async Task<GenericJsonOdata<T>> CallOdataEndpoint<T>(string requestUri)
        //{            
        //    var request =(HttpWebRequest)HttpWebRequest.Create(requestUri);
        //    request.Accept = "application/json;odata.metadata=none";
        //    string token = Authenticator.GetAdalHeader();
        //    request.Headers["Authorization"] = token;
        //    request.Method = "GET";
        //    request.Timeout = 1;
        //    //request.Timeout = 1000 * 60 * 2; // we set it to 2 minutes.
        //    var result = new GenericJsonOdata<T>();
        //    try
        //    {
        //        using (var response = (HttpWebResponse)(await request.GetResponseAsync()))
        //        {
        //            using (var responseStream = response.GetResponseStream())
        //            {
        //                using (var streamReader = new StreamReader(responseStream))
        //                {
        //                    var responseString = streamReader.ReadToEnd();
        //                    //string sanitized = SanitizeJsonString(responseString);
        //                    result = JsonConvert.DeserializeObject<GenericJsonOdata<T>>(responseString);
        //                    return result;
        //                }
        //            }
        //        }
        //    }
        //    catch(WebException e)
        //    {
        //        using (var rStream = e.Response.GetResponseStream())
        //        {
        //            using (var reader = new StreamReader(rStream))
        //            {
        //                result.Exception = JsonConvert.DeserializeObject<AxBaseException>(reader.ReadToEnd());
        //                // TODO: Need to log error;
        //                return result;
        //            }
        //        }
        //    }
        //}
        private static async Task<GenericJsonOdata<T>> CallAGRServiceArray<T>(string service, string serviceMethod, string postData, string serviceGroup, ServiceData authData)
        {
            var baseUrl = authData.BaseUrl;
            serviceGroup = serviceGroup ?? System.Configuration.ConfigurationManager.AppSettings["StandardServiceGroup"];
            var endpoint = baseUrl + "/api/services/" + serviceGroup + "/" + service + "/" + serviceMethod+ApplyCrossCompanyFilter("");

            HttpClient client = new HttpClient();
            string token = Authenticator.GetAuthData(Common.ErpTasks.ErpTaskStep.AuthenticationType.D365).AuthToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));           
            client.Timeout = new TimeSpan(0, 3, 0);

            System.Diagnostics.Debug.Write("Endpoint :" + endpoint + " ");
            System.Diagnostics.Debug.WriteLine("postData: " + postData);
            var result = new GenericJsonOdata<T>();
            try
            {
                using (var response = await client.PostAsync(endpoint, new StringContent(postData)))
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        result.value = JsonConvert.DeserializeObject<List<T>>(responseString);
                        return result;
                    }
                    else
                    {
                        return new GenericJsonOdata<T>
                        {
                            Exception = new AxBaseException { ApplicationException = new ApplicationException(responseString) }
                        };
                    }
                }
            }
            catch (ArgumentNullException ne)
            {
                return new GenericJsonOdata<T>
                {
                    Exception = new AxBaseException { ApplicationException = ne }
                };
            }
            catch (TaskCanceledException)
            {
                return new GenericJsonOdata<T>
                {
                    Exception = new AxBaseException { ApplicationException = new ApplicationException("Timeout Expired for " + endpoint) }
                };
            }
            catch(Exception e)
            {
                return new GenericJsonOdata<T>
                {
                    Exception = new AxBaseException { ApplicationException = e }
                };
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

        private static async Task<GenericJsonOdata<T>> WriteFromService<T>(Int64 recId, Int64 pageSize, string webMethod, string serviceName, string destTable, 
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
            var result = await ServiceConnector.CallAGRServiceArray<T>(serviceName, webMethod, sb.ToString(), null, authData);

            //var reader = result.value.GetDataReader();

            //DataWriter.WriteToTable<T>(reader, destTable, authData.InjectionPropertyValue, authData.InjectionPropertyName);

            return result;
        }

        public static async Task<AxBaseException> CallService<T>(int actionId, ErpTaskStep step, ServiceData authData)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                long recId = 0;
                GenericJsonOdata<T> result = new GenericJsonOdata<T>();
                result = await WriteFromService<T>(0, 500, step.ServiceMethod, step.ServiceName, step.DbTable, DateTime.MinValue, DateTime.MinValue, authData, false);
                if (result.value.Any())
                {
                    DataWriter.TruncateSingleTable(step.DbTable);
                }                
                while (result.value.Any() && result.Exception == null)
                {
                    result = await WriteFromService<T>(recId, step.MaxPageSize.Value, step.ServiceMethod, step.ServiceName, step.DbTable, DateTime.MinValue, DateTime.MinValue, authData, false);
                    DataWriter.WriteToTable<T>(result.value.GetDataReader(), step.DbTable);
                    recId = DataWriter.GetMaxRecId(step.DbTable);                    
                }

                if (result.Exception == null)
                {
                    DataWriter.LogErpActionStep(actionId, step.DbTable, startTime, true, null, null, step.Id);
                }
                else
                {
                    DataWriter.LogErpActionStep(actionId, step.DbTable, startTime, false, result.Exception.ErrorMessage, result.Exception.StackTrace, step.Id);
                }
                return result.Exception;
            }
            catch (Exception e)
            {
                DataWriter.LogErpActionStep(actionId, step.DbTable, startTime, false, e.Message, e.StackTrace, step.Id);
                return new AxBaseException { ApplicationException = e };
            }
        }

        public static async Task<AxBaseException> CallServiceByDate<T>(DateTime date, int actionId, ErpTaskStep step, ServiceData authData, Func<DateTime, DateTime> nextPeriod = null )
        {
            if (nextPeriod == null)
            {
                nextPeriod = AddDay;
            }
            DateTime startTime = DateTime.Now;
            try
            {
                GenericJsonOdata<T> result = new GenericJsonOdata<T>();
                bool firstResult = false;
                for (DateTime d = date.Date; d < DateTime.Now.Date && result.Exception == null; d = nextPeriod(d))
                {
                    result = await WriteFromService<T>(0, 10000, step.ServiceMethod, step.ServiceName, step.DbTable, d, nextPeriod(d), authData, true);
                    if (!firstResult && result.value.Any())
                    {
                        DataWriter.TruncateSingleTable(step.DbTable);
                        firstResult = true;
                    }
                    DataWriter.WriteToTable<T>(result.value.GetDataReader(), step.DbTable);
                }
                if (result.Exception == null)
                {
                    DataWriter.LogErpActionStep(actionId, step.DbTable, startTime, true, null, null, step.Id);
                }
                else
                {
                    DataWriter.LogErpActionStep(actionId, step.DbTable, startTime, false, result.Exception.ErrorMessage, result.Exception.StackTrace, step.Id);
                }
                return result.Exception;
            }
            catch (Exception e)
            {
                DataWriter.LogErpActionStep(actionId, step.DbTable, startTime, false, e.Message, e.StackTrace, step.Id);
                return new AxBaseException { ApplicationException = e };
            }

        }

        public static DateTime AddDay(DateTime d)
        {
            return d.AddDays(1);
        }

        public static async Task<AxBaseException> CallOdataEndpointComplex<T, Y>(ErpTaskStep step, int actionId, ServiceData authData) where T : GenericJsonOdata<Y>
        {
            DateTime startTime = DateTime.Now;
            try
            {
                var baseUrl = authData.BaseUrl;
                var endpoint = baseUrl + authData.OdataUrlPostFix + step.EndPoint;

                var returnODataObject = await CallOdataEndpointAsyncComplex<Y>(endpoint, authData);
                var results = returnODataObject.WriteObject;
                foreach (var stepDetail in step.Details)
                {
                    if (results != null && results.GetType().GetProperty(stepDetail.nested_property_name) != null)
                    {
                        DataWriter.TruncateSingleTable(stepDetail.db_table);
                        Type genericType = typeof(List<>).MakeGenericType(stepDetail.GetReturnType());
                        MethodInfo method = typeof(DataWriter).GetMethod("WriteToTable", new Type[] { typeof(IList), typeof(string), typeof(object), typeof(string) });
                        MethodInfo generic = method.MakeGenericMethod(stepDetail.GetReturnType());

                        Object[] parameters = new Object[5];
                        IList list = (IList)results.GetType().GetProperty(stepDetail.nested_property_name).GetValue(results);
                        if (list != null)
                        {
                            //var list = Convert.ChangeType(results[stepDetail.PropertyName], genericType);
                            parameters = new object[] { list, stepDetail.db_table, authData.InjectionPropertyValue, authData.InjectionPropertyName };
                            generic.Invoke(null, parameters);
                        }


                        //DataWriter.WriteToTable(((List<dynamic>)results[stepDetail.PropertyName]).GetDataReader(), stepDetail.DbTable,
                        //    authData.InjectionPropertyValue, authData.InjectionPropertyName);
                    }
                }
                
                //string nextLinkEndpoint = null;
                //while (!string.IsNullOrEmpty(returnODataObject.NextLink))
                //{
                //    if (returnODataObject.appendNextLink)
                //    {
                //        nextLinkEndpoint = endpoint + returnODataObject.NextLink;
                //    }
                //    else
                //    {
                //        nextLinkEndpoint = returnODataObject.NextLink + ApplyCrossCompanyFilter(filters);
                //    }
                //    returnODataObject = await CallOdataEndpointAsyncDynamic(nextLinkEndpoint, authData);
                //    DataWriter.WriteToTable(returnODataObject.value.GetDataReader(), dbTable, authData.InjectionPropertyValue, authData.InjectionPropertyName);
                //    if (returnODataObject.Exception != null)
                //    {
                //        break;
                //    }
                //}
                if (returnODataObject.Exception != null)
                {                    
                    DataWriter.LogErpActionStep(actionId, step.StepName, startTime, false, returnODataObject.Exception.ErrorMessage, returnODataObject.Exception.StackTrace, step.Id);
                }
                else
                {
                    DataWriter.LogErpActionStep(actionId, step.StepName, startTime, true, null, null, step.Id);
                }
                return returnODataObject.Exception;
            }
            catch (Exception e)
            {
                DataWriter.LogErpActionStep(actionId, step.StepName, startTime, false, e.Message, e.StackTrace, step.Id);
                return new AxBaseException { ApplicationException = e };
            }

        }
        public static async Task<AxBaseException> CallOdataEndpointComplexByDate<T, Y>(ErpTaskStep step, string filters, List<ErpTaskStepDetails> resultProperties, int actionId, ServiceData authData,
            DateTime startDate) where T : GenericJsonOdata<Y>
        {
            DateTime startTime = DateTime.Now;
            try
            {
                var baseUrl = authData.BaseUrl;
                var endpoint = baseUrl + authData.OdataUrlPostFix + step.EndPoint;
                DateTime firstDayOfMonth = new DateTime(startDate.Year, startDate.Month, 1);
                var returnODataObject = new GenericWriteObject<Y>();
                bool isFirstIteration = true;
                for (DateTime d = firstDayOfMonth; d <= DateTime.Now.Date; d = d.AddMonths(1))
                {
                    returnODataObject = await CallOdataEndpointAsyncComplexByDate<Y>(endpoint, authData, d, d.AddMonths(1).AddDays(-1));
                    var results = returnODataObject.WriteObject;                    
                    foreach (var stepDetail in resultProperties)
                    {
                        if (results!= null && results.GetType().GetProperty(stepDetail.nested_property_name) != null)
                        {
                            if (isFirstIteration)
                            {
                                DataWriter.TruncateSingleTable(stepDetail.db_table);
                                isFirstIteration = false;
                            }
                            Type genericType = typeof(List<>).MakeGenericType(stepDetail.GetReturnType());
                            MethodInfo method = typeof(DataWriter).GetMethod("WriteToTable", new Type[] { typeof(IList), typeof(string), typeof(object), typeof(string) });
                            MethodInfo generic = method.MakeGenericMethod(stepDetail.GetReturnType());

                            Object[] parameters = new Object[5];
                            IList list = (IList)results.GetType().GetProperty(stepDetail.nested_property_name).GetValue(results);
                            if (list != null)
                            {
                                //var list = Convert.ChangeType(results[stepDetail.PropertyName], genericType);
                                parameters = new object[] {list , stepDetail.db_table, authData.InjectionPropertyValue, authData.InjectionPropertyName };
                                generic.Invoke(null, parameters);
                            }

                            //DataWriter.WriteToTable(((List<dynamic>)results[stepDetail.PropertyName]).GetDataReader(), stepDetail.DbTable,
                            //    authData.InjectionPropertyValue, authData.InjectionPropertyName);
                        }
                    }
                }
                if (returnODataObject.Exception != null)
                {
                    DataWriter.LogErpActionStep(actionId, step.StepName, startTime, false, returnODataObject.Exception.ErrorMessage, returnODataObject.Exception.StackTrace, step.Id);
                }
                else
                {
                    DataWriter.LogErpActionStep(actionId, step.StepName, startTime, true, null, null, step.Id);
                }
                return returnODataObject.Exception;
            }
            catch (Exception e)
            {
                DataWriter.LogErpActionStep(actionId, step.StepName, startTime, false, e.Message, e.StackTrace, step.Id);
                return new AxBaseException { ApplicationException = e };
            }

        }
        private static async Task<GenericWriteObject<T>> CallOdataEndpointAsyncComplex<T>(string requestUri, ServiceData authData)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authData.AuthMethod, authData.AuthToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(0, 3, 0);
            var result = new GenericWriteObject<T>();
            try
            {
                using (var response = await client.GetAsync(requestUri))
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        result.WriteObject = JsonConvert.DeserializeObject<T>(responseString);
                        return result;
                    }
                    else
                    {                        
                        result.Exception = new AxBaseException { ApplicationException = new ApplicationException(responseString) };
                        return result;
                    }
                }
            }
            catch (ArgumentNullException ne)
            {
                result.Exception = new AxBaseException { ApplicationException = ne };
                return result;
            }
            catch (TaskCanceledException)
            {
                result.Exception = new AxBaseException { ApplicationException = new ApplicationException("Timeout Expired for " + requestUri) };                
                return result;
            }
            catch (Exception e)
            {
                result.Exception = new AxBaseException { ApplicationException = e };
                return result;
            }

        }

        private static async Task<GenericWriteObject<T>> CallOdataEndpointAsyncComplexByDate<T>(string requestUri, ServiceData authData, DateTime startDate, DateTime endDate)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authData.AuthMethod, authData.AuthToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(0, 3, 0);
            string endPoint = requestUri + "/" + startDate.Year + "?budatf=" + startDate.ToString("yyyyMMdd") + "&budatt=" + endDate.ToString("yyyyMMdd");
            var result = new GenericWriteObject<T>();
            try
            {
                using (var response = await client.GetAsync(endPoint))
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        result.WriteObject = JsonConvert.DeserializeObject<T>(responseString);
                        return result;
                    }
                    else
                    {
                        result.Exception = new AxBaseException { ApplicationException = new ApplicationException(responseString) };
                        return result;
                    }
                }
            }
            catch (ArgumentNullException ne)
            {
                result.Exception = new AxBaseException { ApplicationException = ne };
                return result;
            }
            catch (TaskCanceledException)
            {
                result.Exception = new AxBaseException { ApplicationException = new ApplicationException("Timeout Expired for " + requestUri) };
                return result;
            }
            catch (Exception e)
            {
                result.Exception = new AxBaseException { ApplicationException = e };
                return result;
            }

        }

    }
}