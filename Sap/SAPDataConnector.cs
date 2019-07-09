using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErpConnector.Common;
using ErpConnector.Common.AGREntities;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common.Util;
using ErpConnector.Sap.DTO;

namespace ErpConnector.Sap
{
    public class SAPDataConnector : ErpGenericConnector
    {
        private AxBaseException CreateTO(List<POTOCreate> toCreate, string sapComanyCode)
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
                foreach(var line in toCreate)
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

            }
            return null;
        }

        private AxBaseException CreatePO(List<POTOCreate> poCreate, string sapCompanyCode)
        {
            if (poCreate.Any())
            {
                //SapCreateRequisition.ZWS_AGR_requisition_createClient reqClient = new SapCreateRequisition.ZWS_AGR_requisition_createClient();
                //SetCredentials(reqClient.ClientCredentials.UserName);
                List<SAPRequisitionDTO> reqItems = new List<SAPRequisitionDTO>();
                //SapCreateRequisition.Bapiparex[] extension = new SapCreateRequisition.Bapiparex[0];
                //SapCreateRequisition.Bapiebkn[] accountAssignment = new SapCreateRequisition.Bapiebkn[0];
                //SapCreateRequisition.Bapimerqaddrdelivery[] addressDeliv = new SapCreateRequisition.Bapimerqaddrdelivery[0];
                //SapCreateRequisition.Bapiesucc[] contractLimits = new SapCreateRequisition.Bapiesucc[0];
                //SapCreateRequisition.Bapiebantx[] reqText = new SapCreateRequisition.Bapiebantx[0];
                //SapCreateRequisition.Bapiesuhc[] reqLimit = new SapCreateRequisition.Bapiesuhc[0];
                //SapCreateRequisition.Bapiesllc[] reqService = new SapCreateRequisition.Bapiesllc[0];
                //SapCreateRequisition.Bapieslltx[] reqServiceText = new SapCreateRequisition.Bapieslltx[0];
                //SapCreateRequisition.Bapiesklc[] reqAccass = new SapCreateRequisition.Bapiesklc[0];

                //foreach (OrderlinesDs.OrderLinesRow row in olDs.OrderLines.Rows)
                foreach (var row in poCreate)
                {
                    if (row.unit_qty_chg > 0)
                    {
                        SAPRequisitionDTO reqItem = new SAPRequisitionDTO();
                        //reqItem.PreqItem = "0000";
                        reqItem.CreatedBy = "AGR";//po_to_create.Orders[0].UserId;
                        reqItem.DocType = "NB";
                        reqItem.PreqName = "AGR";
                        reqItem.ShortText = row.description;
                        reqItem.Material = PadWithZeros(row.item_no, 18);//row.ProductId;
                        reqItem.Plant = sapCompanyCode;// po_to_create.Orders[0].OrderFromSupplierId;                        
                        reqItem.StoreLoc = row.location_no;
                        //reqItem.MatGrp = row.ProductGroup;
                        reqItem.Quantity = (decimal)(double)row.unit_qty_chg;
                        reqItem.Unit = row.unit;
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

                //SapCreateRequisition.Bapireturn[] reqReturn = new SapCreateRequisition.Bapireturn[0];
                //SapCreateRequisition.Bapiebanc[] reqItemArray = reqItems.ToArray();
                //if (reqItemArray.Length > 0)
                //{
                //    string reqSapNr = reqClient.RequisitionCreate("X", ref extension, ref accountAssignment, ref addressDeliv, ref contractLimits,
                //        ref reqText, ref reqItemArray, ref reqLimit, ref reqService, ref reqServiceText, ref reqAccass, ref reqReturn, null);
                //    if (ValidateReq(reqSapNr))
                //    {
                //        SapToStgWriter.SetAGR5OrderAsTransfered(oRow.id, reqSapNr, "Req");
                //    }
                //    else
                //    {
                //        SapToStgWriter.LogError("Error in Prod to Sap CreateRequisition.", this.ToString(), SapToStgWriter.ERROR_CODE.PROD_TO_SAP,
                //            "Following is error message from Sap:", SapMessageToString(reqReturn));
                //    }
                //}
            }
            return null;
        }
        public override AxBaseException CreatePoTo(List<POTOCreate> po_to_create, int actionId)
        {
            try
            {
                var SapComanyCode = DataWriter.GetSetting("SapCompanyCode");
                //SapToStgWriter db = new SapToStgWriter();
                //string reqResult = "test";
                //OrdersDs po_to_create = db.GetOrder();                

                //SapToStgWriter.LogError("Processing " + po_to_create.Orders.Count + " orders", this.ToString(), SapToStgWriter.ERROR_CODE.PROD_TO_SAP,
                //                    "", "");
                //SapToStgWriter.LogError("Processing " + oDs.orders.Count + " orders", this.ToString(), SapToStgWriter.ERROR_CODE.PROD_TO_SAP,
                  //                  "", "");
                
                foreach (var line in po_to_create)
                {

                    if (line.order_id > 0)
                    {
                        try
                        {
                                                       
                            //DataWriter.LogError("Processing order nr " + oRow.id, this.ToString(), SapToStgWriter.ERROR_CODE.PROD_TO_SAP,
                            //        "", "");
                            if (line.vendor_location_type.ToLower() != "vendor")
                            {
                                //SapCreateReservation.ZWS_AGR_RESERVATION_CREATEClient resClient = new SapCreateReservation.ZWS_AGR_RESERVATION_CREATEClient();
                                //SetCredentials(resClient.ClientCredentials.UserName);
                                //List<SapCreateReservation.Bapiresbc> resItems = new List<SapCreateReservation.Bapiresbc>();
                                //SapCreateReservation.Bapirkpfc resHeader = new SapCreateReservation.Bapirkpfc();
                                var resHeader = new SAPResHeaderDTO();
                                //resHeader.ResNo = "0000";
                                resHeader.Plant = SapComanyCode;
                                resHeader.ResDate = DateTime.Now.ToString("yyyy-MM-dd");//po_to_create.Orders[0].DateUpdated.ToString("dd.MM.yyyy");
                                resHeader.MoveType = "311";
                                resHeader.CreatedBy = "AGR";
                                resHeader.ProfitCtr = "0000916299";
                                resHeader.MovePlant = SapComanyCode;
                                //resHeader.MoveStloc = oRow.OrderFromSupplierId;
                                resHeader.MoveStloc = line.location_no; // supplierid


                                //foreach (OrderlinesDs.OrderLinesRow row in olDs.OrderLines.Rows)
                              //  foreach (OrderlinesAGR5Ds.order_linesRow row in olDs.order_lines.Rows)
                                //{
                                  //  if (row.unit_qty_chg > 0)
                                   // {
                                   //     SapCreateReservation.Bapiresbc resItem = new SapCreateReservation.Bapiresbc();
                                //        resItem.Plant = SapComanyCode;
                                //        resItem.StoreLoc = oRow.order_from_location_no; // 0200
                                //        //resItem.StoreLoc = oRow.SupplierId;
                                //        resItem.Unit = row.meins;
                                //        //if (row.item_no == "8504")
                                //        //    resItem.Unit = "M";
                                //        //else
                                //        //    resItem.Unit = "ST";
                                //        resItem.ShortText = row.description;
                                //        resItem.Quantity = (decimal)(double)row.unit_qty_chg;
                                //        resItem.Material = PadWithZeros(row.item_no, 18);
                                //        resItems.Add(resItem);
                                //    }
                                //}
                                //SapCreateReservation.Bapireturn[] returnValues = new SapCreateReservation.Bapireturn[0];

                                //SapCreateReservation.Bapiresbc[] resItemArray = resItems.ToArray();
                                //resHeader.ResNo = resClient.ReservationCreate("X", null, resHeader, ref resItemArray, ref returnValues);
                                //if (returnValues.Length == 0)
                                //{
                                //    SapToStgWriter.SetAGR5OrderAsTransfered(oRow.id, resHeader.ResNo, "Res");
                                //}
                                //else
                                //{
                                //    SapToStgWriter.LogError("Error in Prod to Sap CreateReservation.", this.ToString(), SapToStgWriter.ERROR_CODE.PROD_TO_SAP,
                                //        "Following is error message from Sap:", SapMessageToString(returnValues));
                                //}
                            }
                            else
                            {
                            }

                        }
                        catch (Exception e)
                        {
                            DataWriter.LogError("Error on transfer", e.StackTrace, this, e.HResult);
                        }
                        //SapToStgWriter.SetOrderAsTransfered(orderId);
                    }
                }

            }
            catch (Exception e)
            {
                DataWriter.LogError("Error in Prod to Sap Transfer", e.StackTrace, this, e.HResult);
            }
            return null;
        }

        private string PadWithZeros(string str, int count)
        {
            while (str.Length < count)
            {
                str = "0" + str;
            }
            return str;
        }

    }
}
