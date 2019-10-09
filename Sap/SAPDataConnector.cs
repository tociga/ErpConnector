using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ErpConnector.Common;
using ErpConnector.Common.AGREntities;
using ErpConnector.Common.DTO;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common.Util;
using ErpConnector.Sap.DB;
using ErpConnector.Sap.DTO;
using Newtonsoft.Json;

namespace ErpConnector.Sap
{
    public class SAPDataConnector : ErpGenericConnector
    {
        private readonly CookieContainer cookieContainer = new CookieContainer();
        private GenericWriteObject<SAPReservationWriteDTO> CreateTO(List<POTOCreate> toCreate, string sapComanyCode)
        {
            if (toCreate.Any())
            {
                var firstLine = toCreate.First();
                var resHeader = new SAPResHeaderDTO();
                //resHeader.ResNo = "0000";
                resHeader.Plant = sapComanyCode;
                resHeader.ResDate = DateTime.Now.ToString("yyyy-MM-dd");//po_to_create.Orders[0].DateUpdated.ToString("dd.MM.yyyy");
                resHeader.MoveType = "311";
                resHeader.CreatedBy = "AGR";
                resHeader.ProfitCtr = "0000916299";
                resHeader.MovePlant = sapComanyCode;
                //resHeader.MoveStloc = oRow.OrderFromSupplierId;
                resHeader.MoveStloc = firstLine.location_no; // supplierid
                List<SAPResLineDTO> resItems = new List<SAPResLineDTO>();
                foreach (var line in toCreate)
                {
                    if (line.unit_qty_chg > 0)
                    {
                        var resItem = new SAPResLineDTO();
                        resItem.Plant = sapComanyCode;
                        resItem.StoreLoc = line.order_from_location_no; // 0200
                                                                        //resItem.StoreLoc = oRow.SupplierId;
                        resItem.Unit = line.unit;
                        //if (row.item_no == "8504")
                        //    resItem.Unit = "M";
                        //else
                        //    resItem.Unit = "ST";
                        resItem.ShortText = line.description;
                        resItem.Quantity = line.unit_qty_chg;
                        resItem.Material = PadWithZeros(line.item_no, 18);
                        resItems.Add(resItem);
                    }
                }
                var writeObject = new SAPReservationWriteDTO { Header = resHeader, Lines = resItems };
                var authData = Authenticator.GetAuthData(Common.ErpTasks.ErpTaskStep.AuthenticationType.SAP);
                var fixedFilter = ConfigurationManager.AppSettings["fixedEndPointFilter"];
                var csrfToken = GetCSRFToken(authData).Result;
                var result = CallOdataEndpointPostStringReturn<SAPReservationWriteDTO>("Reservationcreate", fixedFilter, writeObject, authData, csrfToken).Result;
                return result;
            }
            return null;
        }

        private GenericWriteObject<List<SAPRequisitionDTO>> CreatePO(List<POTOCreate> poCreate, string sapCompanyCode)
        {
            if (poCreate != null)
            {
                var authData = Authenticator.GetAuthData(Common.ErpTasks.ErpTaskStep.AuthenticationType.SAP);
                var fixedFilter = ConfigurationManager.AppSettings["fixedEndPointFilter"];
                var csrfToken = GetCSRFToken(authData).Result;
                var reqItems = new List<SAPRequisitionDTO>();
                foreach(var po in poCreate)
                {
                    if (po.unit_qty_chg > 0)
                    {
                        SAPRequisitionDTO reqItem = new SAPRequisitionDTO();
                        //reqItem.PreqItem = "0000";
                        reqItem.CreatedBy = "AGR";//po_to_create.Orders[0].UserId;
                        reqItem.DocType = "NB";
                        reqItem.PreqName = "AGR";
                        reqItem.ShortText = po.description;
                        reqItem.Material = PadWithZeros(po.item_no, 18);//row.ProductId;
                        reqItem.Plant = sapCompanyCode;// po_to_create.Orders[0].OrderFromSupplierId;                        
                        reqItem.StoreLoc = po.location_no;
                        //reqItem.MatGrp = row.ProductGroup;
                        reqItem.Quantity = (decimal)(double)po.unit_qty_chg;
                        reqItem.Unit = po.unit;
                        //if (row.item_no == "8504")
                        //    reqItem.Unit = "M";
                        //else
                        //    reqItem.Unit = "ST";
                        //0000062095
                        reqItem.DelivDate = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");//po_to_create.Orders[0].EstDelivDate.ToString("dd.MM.yyyy");
                        reqItem.GrPrTime = 340m;
                        reqItem.CAmtBapi = 0m;
                        reqItem.PriceUnit = 0m;// (decimal)row.PriceUnit; 
                        reqItems.Add(reqItem);
                    }
                }
                var result = CallOdataEndpointPostStringReturn<List<SAPRequisitionDTO>>("Requisitioncreate", fixedFilter, reqItems, authData, csrfToken).Result;
                return result;
            }
            return null;
        }
        public override int CreatePoTo(List<POTOCreate> po_to_create, int actionId)
        {
            try
            {
                DateTime start = DateTime.Now;
                var SapComanyCode = "1000";// DataWriter.GetSetting("SapCompanyCode");
                if (po_to_create.Any())
                {
                    AxBaseException result = null;
                    var first = po_to_create.First(x=>x.unit_qty_chg > 0);
                    if (first.order_id > 0)
                    {
                        try
                        {

                            //DataWriter.LogError("Processing order nr " + oRow.id, this.ToString(), SapToStgWriter.ERROR_CODE.PROD_TO_SAP,
                            //        "", "");
                            if (first.vendor_location_type.ToLower() != "vendor")
                            {
                                var res = CreateTO(po_to_create, SapComanyCode);
                                if (res.Exception == null && !string.IsNullOrEmpty(res.SimpleResult))
                                {
                                    SAPDbHandler.SetAGR5OrderAsTransfered(first.order_id, res.SimpleResult, "Res");
                                }
                                else
                                {
                                    result = res.Exception;
                                }
                            }
                            else
                            {
                                var req = CreatePO(po_to_create, SapComanyCode);
                                if (req != null && req.Exception == null && !string.IsNullOrEmpty(req.SimpleResult))
                                {
                                    SAPDbHandler.SetAGR5OrderAsTransfered(first.order_id, req.SimpleResult, "Req");
                                }
                                else
                                {
                                    result = req.Exception;
                                }                                        
                            }
                            if (result == null)
                            {
                                DataWriter.LogErpActionStep(actionId, "Create PO or TO orderid = " + first.order_id, start, true, null, null,-1);
                                DataWriter.UpdateOrderStatus(first.order_id);
                            }
                            else
                            {
                                DataWriter.LogErpActionStep(actionId, "Create PO or TO orderid = " + first.order_id, start, false, result.ErrorMessage, result.StackTrace,-1);
                            }

                        }
                        catch (Exception e)
                        {
                            DataWriter.LogError("Error on transfer", e.StackTrace, this, e.HResult);
                        }
                        //SapToStgWriter.SetOrderAsTransfered(orderId);
                    }
                    else
                    {
                        DataWriter.LogErpActionStep(actionId, "No items to transfer for order_id = " + first.order_id, start, false, null, null,-1);
                    }
                }

            }
            catch (Exception e)
            {
                DataWriter.LogError("Error in Prod to Sap Transfer", e.StackTrace, this, e.HResult);
            }
            return actionId;
        }

        private string PadWithZeros(string str, int count)
        {
            while (str.Length < count)
            {
                str = "0" + str;
            }
            return str;
        }

        private async Task<string> GetCSRFToken(ServiceData authData)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.CookieContainer = cookieContainer;
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authData.AuthMethod, authData.AuthToken);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("x-csrf-token", "Fetch");

                    string result = null;
                    using (var response = await client.GetAsync(authData.BaseUrl))
                    {
                        var headers = response.Headers;
                        if (headers.Contains("x-csrf-token"))
                        {
                            result = headers.GetValues("x-csrf-token").First();
                        }
                    }
                    return result;
                }
            }
        }

        public async Task<GenericWriteObject<T>> CallOdataEndpointPostStringReturn<T>(string oDataEndpoint, string filters, T postDataObject, 
            ServiceData authData, string csrfToken)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.CookieContainer = this.cookieContainer;
                using (HttpClient client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authData.AuthMethod, authData.AuthToken);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (!string.IsNullOrEmpty(csrfToken))
                    {
                        client.DefaultRequestHeaders.Add("x-csrf-token", csrfToken);
                    }
                    client.Timeout = new TimeSpan(0, 3, 0);

                    string endpoint = authData.BaseUrl + authData.OdataUrlPostFix + oDataEndpoint + filters ?? "";
                    var postData = JsonConvert.SerializeObject(postDataObject);
                    var content = new StringContent(postData, Encoding.UTF8, "application/json");
                    var result = new GenericWriteObject<T>();
                    try
                    {
                        using (var response = await client.PostAsync(endpoint, content))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                result.SimpleResult = await response.Content.ReadAsStringAsync();
                            }
                            else
                            {
                                var errorResult = await response.Content.ReadAsStringAsync();
                                result.Exception = new AxBaseException { ApplicationException = new Exception("Error when sending post request json = " + postData + " " + errorResult) };
                            }
                            return result;
                        }
                    }
                    catch (Exception e)
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
            }
        }

    }
}
