using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using ErpConnector.Ax.Modules;
using ErpConnector.Ax.Microsoft.Dynamics.DataEntities;
using ErpConnector.Ax.DTO;
using System.Diagnostics;
using System.Configuration;
using ErpConnector.Common;
using ErpConnector.Ax.Utils;
using System.Collections.Generic;
using ErpConnector.Ax.Authentication;
using System.Collections;
using ErpConnector.Common.AGREntities;
using System.Linq;
using ErpConnector.Common.Exceptions;
using ErpConnector.Common.ErpTasks;
using System.Reflection;
using ErpConnector.Ax.DB;
using ErpConnector.Common.Util;

namespace ErpConnector.Ax
{
    public class AxODataConnector : ErpGenericConnector
    {
        #region Initialization
        private Resources _context;
        private Resources Context
        {
            get
            {
                if (_context == null)
                {
                    var axBaseUrl = ConfigurationManager.AppSettings["base_url"];
                    _context = new Resources(new Uri(axBaseUrl + "/data"));
                    _context.SendingRequest2 += Context_SendingRequest2;
                    return _context;
                }
                return _context;
            }
        }
        private bool includesFashion;
        private bool includeB_M;
        public AxODataConnector()
        {
			Boolean.TryParse(ConfigurationManager.AppSettings["includesFashion"], out includesFashion);
            Boolean.TryParse(ConfigurationManager.AppSettings["includeBAndM"], out includeB_M);
        }



        private void Context_SendingRequest2(object sender, global::Microsoft.OData.Client.SendingRequest2EventArgs e)
        {
            e.RequestMessage.SetHeader("Authorization", Common.Util.Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365).AuthHeader);
        }
        #endregion

        #region Public Interface
        public string GetDBScript(string entity)
        {
            return Common.Util.ScriptGeneratorModule.GenerateScript(entity);
        }
        //public override void TaskList(int actionId, ErpTask erpTasks, DateTime date, int? noParallelProcess)
        //{
        //    //DataWriter.TruncateTables(erpTasks.truncate_items, erpTasks.truncate_sales_trans_dump, erpTasks.truncate_sales_trans_refresh, erpTasks.truncate_locations_and_vendors,
        //    //    erpTasks.truncate_lookup_info, erpTasks.truncate_bom, erpTasks.truncate_po_to, erpTasks.truncate_price, erpTasks.truncate_attribute_refresh);
        //    TaskExecute exec = new TaskExecute(erpTasks.Steps, noParallelProcess.HasValue ? noParallelProcess.Value : 4 , actionId, date);
        //    exec.Execute();

        //    //foreach (var erpStep in erpTasks.Steps)
        //    //{
        //    //    ExecuteTask(actionId, erpStep, date); // possible to do some parallel processing.
        //    //}
        //    return;
        //}

        #endregion
        public GenericWriteObject<ReleasedProductMaster> CreateMaster(List<ReleasedProductMaster> masters)
        {
            string axBaseUrl = ConfigurationManager.AppSettings["base_url"];
            var clientconfig = new ClientConfiguration(axBaseUrl + "/data",
                                                       ConfigurationManager.AppSettings["client_secret"],
                                                       axBaseUrl,
                                                       ConfigurationManager.AppSettings["ax_oauth_token_url"],
                                                       ConfigurationManager.AppSettings["client_key"]);
            var oAuthHelper = new OAuthHelper(clientconfig);
            string dataarea = ConfigurationManager.AppSettings["DataAreaId"];
            AXODataContextConnector<ReleasedProductMaster> axConnector = new UpdateProductMaster<ReleasedProductMaster>(oAuthHelper, logMessageHandler: WriteLine, enableCrossCompany: true);
            axConnector.CreateRecordInAX(dataarea, masters);

            return new GenericWriteObject<ReleasedProductMaster> { Exception = null, WriteObject = masters[0] };
        }
        public GenericWriteObject<ReleasedProductVariant> UpdateVariants(List<ReleasedProductVariant> variants)
        {
            string axBaseUrl = ConfigurationManager.AppSettings["base_url"];
            var clientconfig = new ClientConfiguration(axBaseUrl + "/data",
                                                       ConfigurationManager.AppSettings["client_secret"],
                                                       axBaseUrl,
                                                       ConfigurationManager.AppSettings["ax_oauth_token_url"],
                                                       ConfigurationManager.AppSettings["client_key"]);
            var oAuthHelper = new OAuthHelper(clientconfig);
            string dataarea = ConfigurationManager.AppSettings["DataAreaId"];
            AXODataContextConnector<ReleasedProductVariant> axConnector = new UpdateReleasedProductVariants<ReleasedProductVariant>(oAuthHelper, logMessageHandler: WriteLine, enableCrossCompany: true);
            axConnector.CreateRecordInAX(dataarea, variants);
            return new GenericWriteObject<ReleasedProductVariant> { Exception = null, WriteObject = variants[0] };
        }

        public override int CreateItems(int tempId, int actionId)
        {
            List<ItemToCreate> itemsToCreate = AxDbHandler.GetItemsToCreate(tempId);
            DateTime startTime = DateTime.Now;
            var authData = Authenticator.GetAuthData(ErpTaskStep.AuthenticationType.D365);
            if (itemsToCreate.Any())
            {
                var masterData = itemsToCreate.First();
                if (masterData.master_status < 2)
                {
                    var master = new ProductMasterWriteDTO();
                    master.ProductDimensionGroupName = "CS";
                    master.ProductNumber = masterData.product_no;
                    master.ProductName = masterData.product_name;
                    master.ProductSearchName = masterData.product_name.Trim();
                    master.ProductSizeGroupId = masterData.size_group_no;
                    master.ProductColorGroupId = masterData.color_group_no; // possible to use color_group_no
                    //master.RetailProductCategoryName = masterData.sup_department;
                    master.ProductDescription = masterData.description;

                    //var erpMaster = CreateMaster(master);
                    var erpMaster = ServiceConnector.CallOdataEndpointPost<ProductMasterWriteDTO, EnumConverter>("ProductMasters", null, master,
                        authData).Result;

                    if (erpMaster.Exception != null)
                    {
                        DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, false, erpMaster.Exception.ErrorMessage, erpMaster.Exception.StackTrace);
                        OnTaskCompleted(this, new ErpTaskCompletedArgs { Exception = erpMaster.Exception, ActionId = actionId, Status = 3 });
                        return actionId;
                    }
                    else if (erpMaster.WriteObject.ProductNumber.ToLower().Trim() != masterData.product_no.ToLower().Trim())
                    {
                        DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, false, null, null);
                        OnTaskCompleted(this, new ErpTaskCompletedArgs
                        {
                            Exception =
                            new AxBaseException
                            {
                                ApplicationException = new ApplicationException(
                                "The product number for Product Master does not match the returned number, AX value = " + erpMaster.WriteObject.ProductNumber + " AGR number = " + masterData.product_no)
                            },
                            ActionId = actionId,
                            Status = 3
                        });
                        return actionId;
                    }

                    DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, true, null, null);
                    startTime = DateTime.Now;
                    var releasedMaster = new ReleasedProductMasterWriteDTO(master.ProductNumber, master.ProductSearchName,
                        masterData.primar_vendor_no, masterData.sale_price, masterData.cost_price);
                    var erpReleasedMaster = ServiceConnector.CallOdataEndpointPost<ReleasedProductMasterWriteDTO,EnumConverter>("ReleasedProductMasters", null, releasedMaster, authData).Result;

                    if (erpReleasedMaster.Exception != null)
                    {
                        DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Master", startTime, false, erpReleasedMaster.Exception.ErrorMessage, erpReleasedMaster.Exception.StackTrace);
                        OnTaskCompleted(this, new ErpTaskCompletedArgs { Exception = erpReleasedMaster.Exception, ActionId = actionId, Status = 3 });
                    }
                    else if (erpReleasedMaster.WriteObject.ItemNumber.ToLower().Trim() != master.ProductNumber.ToLower().Trim())
                    {
                        DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Master", startTime, false, null, null);
                        OnTaskCompleted(this, new ErpTaskCompletedArgs
                        {
                            Exception =
                            new AxBaseException
                            {
                                ApplicationException = new ApplicationException(
                                "The item number for Released Product Master does not match the returned number, AX value = " + erpReleasedMaster.WriteObject.ItemNumber + " AGR number = " + masterData.product_no)
                            },
                            ActionId = actionId,
                            Status = 3
                        });
                        return actionId;
                    }
                    DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Master", startTime, true, null, null);
                }

                foreach (var item in itemsToCreate)
                {
                    var variant = new ReleasedProductVariantDTO
                    {
                        ItemNumber = item.product_no,
                        //ProductColorId = item.color_group_no,
                        ProductColorId = item.color_no,
                        ProductSizeId = item.size_no,
                        ProductConfigurationId = "",
                        ProductStyleId = "",
                        ProductDescription = item.description,
                        ProductName = item.product_name + "_" + item.color_no + "_" + item.size,
                        ProductSearchName = (item.product_name + "_" + item.color_no + "_" + item.size).Trim(),
                        ProductMasterNumber = item.product_no
                    };
                    startTime = DateTime.Now;
                    var erpVariants = ServiceConnector.CallOdataEndpointPost<ReleasedProductVariantDTO, EnumConverter>("ReleasedProductVariants", null, variant, authData).Result;

                    if (erpVariants.Exception != null)
                    {
                        DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Variant", startTime, false, erpVariants.Exception.ErrorMessage, erpVariants.Exception.StackTrace);
                        OnTaskCompleted(this, new ErpTaskCompletedArgs { Exception = erpVariants.Exception, ActionId = actionId, Status = 3 });
                        return actionId;
                    }
                    DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Variant", startTime, true, null, null);
                }
            }
            OnTaskCompleted(this, new ErpTaskCompletedArgs { ActionId = actionId, Status = 2 });
            return actionId;
        }

        public override int CreatePoTo(List<POTOCreate> po_to_create, int actionId)
        {
            try
            {
                string axBaseUrl = ConfigurationManager.AppSettings["base_url"];
                var clientconfig = new ClientConfiguration(axBaseUrl + "/data",
                                                           ConfigurationManager.AppSettings["client_secret"],
                                                           axBaseUrl,
                                                           ConfigurationManager.AppSettings["ax_oauth_token_url"],
                                                           ConfigurationManager.AppSettings["client_key"]);
                var oAuthHelper = new OAuthHelper(clientconfig);

                List<AGROrderDTO> a = new List<AGROrderDTO>();

                string dataarea = ConfigurationManager.AppSettings["DataAreaId"];

                AXODataContextConnector<AGROrderDTO> axConnector = new CreateOrder<AGROrderDTO>(oAuthHelper, logMessageHandler: WriteLine, enableCrossCompany: true);
                if (po_to_create.Any())
                {
                    var o = po_to_create.First();

                    AGROrderDTO order = new AGROrderDTO();
                    order.ARGId = o.order_id.ToString();
                    order.OrderFrom = o.order_from_location_no; //"1004"; //vendor number;
                    order.OrderTo = o.location_no; //"DC"; //warehouse;
                    order.OrderType = o.vendor_location_type.ToLower() == "vendor" ? AGROrderType.PO : AGROrderType.TO;
                    order.ReceiveDate = o.est_delivery_date < DateTime.Now.Date ? DateTime.Now.Date : o.est_delivery_date;
                    order.OrderStatus = AGROrderStatus.Created;

                    ArrayList orderline = new ArrayList();

                    for (int i = 0; i < po_to_create.Count; i++)
                    {
                        AGROrderLineDTO line = new AGROrderLineDTO();
                        line.ARGId = order.ARGId;
                        line.Color = po_to_create[i].color;
                        line.Config = "";
                        line.ItemId = po_to_create[i].item_no;
                        line.LineNum = i + 1;
                        line.Qty = po_to_create[i].unit_qty_chg;
                        line.Size = po_to_create[i].size;
                        line.Style = po_to_create[i].style;
                        line.OrderTo = po_to_create[i].location_no;
                        orderline.Add(line);
                    }

                    order.ArgOrderLine = orderline;

                    a.Add(order);

                    // Send the data file to the connector object:
                    var createOrder = axConnector.CreateRecordInAX(dataarea, a);
                    if(createOrder != null)
                    {
                        OnTaskCompleted(this, new ErpTaskCompletedArgs { Exception = createOrder, ActionId = actionId, Status = 3 });
                        return actionId;
                    }

                    order.OrderStatus = AGROrderStatus.Ready;
                    order.ArgOrderLine.Clear();
                    a = new List<AGROrderDTO>();
                    a.Add(order);

                    // Send the data file to the connector object:
                    createOrder = axConnector.CreateRecordInAX(dataarea, a);
                    OnTaskCompleted(this, new ErpTaskCompletedArgs { Exception = createOrder, ActionId = actionId, Status = 3 });
                    return actionId;
                }
            }
            catch (Exception e)
            {
                OnTaskCompleted(this, new ErpTaskCompletedArgs { Exception = new AxBaseException { ApplicationException = e }, ActionId = actionId, Status = 3 } );
                return actionId;
            }
            return actionId;
        }

        static void WriteLine(string msg, ConsoleColor color = ConsoleColor.Green, bool leadingLine = true)
        {
            Console.ForegroundColor = color;

            if (leadingLine) Console.WriteLine();
            Console.WriteLine(msg);

            //Log.Info(msg);
        }

        public override int UpdateProductLifecycleState(int plcUpdateId, int actionId)
        {
            DateTime startTime = DateTime.Now;
            AxDbHandler.UpdateProductLifeCycleState(plcUpdateId, actionId, 1);
            var plc = AxDbHandler.GetProductLifeCycleStateUpdates(plcUpdateId);
            if (plc.Any())
            {
                var distinctMaster = plc.DistinctBy(x => x.product_no).Select(y => y.product_no);
                foreach (var m in distinctMaster)
                {
                    var plcPerMaster = plc.Where(x => x.product_no == m);
                    string masterLifecycleState = "";
                    if (plcPerMaster.Where(x => x.lifecycle_status.ToLower() == "confirmed").Any())
                    {
                        masterLifecycleState = "Confirmed";
                    }
                    else if (plcPerMaster.Count(x => x.lifecycle_status.ToLower() == "delete") == plcPerMaster.Count())
                    {
                        masterLifecycleState = "Delete";
                    }
                    else if (plcPerMaster.Count(x => x.lifecycle_status.ToLower() == "shortlist") == plcPerMaster.Count())
                    {
                        masterLifecycleState = "Shortlist";
                    }
                    else
                    {
                        OnTaskCompleted(this, new ErpTaskCompletedArgs
                        {
                            Exception =
                            new AxBaseException
                            {
                                ApplicationException = new Exception(string.Format("Plc update batch = {0} contains an invalid Product Lifecycle State in D365",
                                    plcPerMaster.First().product_lifecycle_state_update_id))
                            },
                            ActionId = actionId,
                            Status = 3
                        });
                        return actionId;
                    }

                    if (plcPerMaster.Any())
                    {
                        var master = new ReleasedProductMaster();
                        master.ProductNumber = plcPerMaster.First().product_no;
                        //master.ProductLifecycleStateId = masterLifecycleState;
                        var erpMaster = CreateMaster(new List<ReleasedProductMaster> { master });

                        if (erpMaster.Exception != null)
                        {
                            DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, false, erpMaster.Exception.ErrorMessage, erpMaster.Exception.StackTrace);
                            OnTaskCompleted(this, new ErpTaskCompletedArgs { Exception = erpMaster.Exception, ActionId = actionId, Status = 3 });
                            return actionId;
                        }
                        else if (erpMaster.WriteObject.ProductNumber.ToLower().Trim() != plcPerMaster.First().product_no.ToLower().Trim())
                        {
                            DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, false, null, null);
                            OnTaskCompleted(this, new ErpTaskCompletedArgs
                            {
                                Exception =
                                new AxBaseException
                                {
                                    ApplicationException = new ApplicationException(
                                    "The product number for Product Master does not match the returned number, AX value = " + erpMaster.WriteObject.ProductNumber + " AGR number = "
                                    + plcPerMaster.First().product_no)
                                },
                                ActionId = actionId,
                                Status = 3
                            });
                            return actionId;
                        }

                        DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, true, null, null);
                        startTime = DateTime.Now;
                    }
                    List<ReleasedProductVariant> variantList = new List<ReleasedProductVariant>();
                    string variantPlc;
                    foreach (var item in plcPerMaster)
                    {
                        if (item.lifecycle_status.ToLower() == "confirmed")
                        {
                            variantPlc = "Confirmed";
                        }
                        else if (item.lifecycle_status.ToLower() == "delete")
                        {
                            variantPlc = "Delete";
                        }
                        else if (item.lifecycle_status.ToLower() == "shortlist")
                        {
                            variantPlc = "Shortlist";
                        }
                        else
                        {
                            OnTaskCompleted(this, new ErpTaskCompletedArgs
                            {
                                Exception =
                                new AxBaseException
                                {
                                    ApplicationException = new Exception(string.Format("Plc update batch = {0} {1} {2} {3} {4} contains an invalid Product Lifecycle State {5} in D365",
                                        item.product_no, item.product_color_id, item.product_size_id, item.product_style_id, item.product_config_id, item.lifecycle_status))
                                },
                                ActionId = actionId,
                                Status = 3
                            });
                            return actionId;
                        }
                        var variant = new ReleasedProductVariant
                        {
                            ItemNumber = item.product_no,
                            ProductMasterNumber = item.product_no,
                            ProductSizeId = item.product_size_id,
                            ProductColorId = item.product_color_id,
                            ProductStyleId = item.product_style_id,
                            ProductConfigurationId = item.product_config_id,
                            //ProductLifecycleStateId = variantPlc
                        };
                        startTime = DateTime.Now;
                        //var erpVariants = ServiceConnector.CallOdataEndpointPost<ReleasedProductVariantDTO>("ReleasedProductVariants", null, variant).Result;
                        variantList.Add(variant);
                    }
                    var erpVariants = UpdateVariants(variantList);
                    if (erpVariants.Exception != null)
                    {
                        DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Variant", startTime, false, erpVariants.Exception.ErrorMessage, erpVariants.Exception.StackTrace);
                        OnTaskCompleted(this, new ErpTaskCompletedArgs { Exception = erpVariants.Exception, ActionId = actionId, Status = 3 });
                        return actionId;
                    }
                    DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Variant", startTime, true, null, null);
                    AxDbHandler.UpdateProductLifeCycleState(plcUpdateId, actionId, 2);
                    OnTaskCompleted(this, new ErpTaskCompletedArgs { ActionId = actionId, Status = 2 });
                    return actionId;
                }
            }
            OnTaskCompleted(this, new ErpTaskCompletedArgs { ActionId = actionId, Status = 2 });
            return actionId;
        }
    }
}
