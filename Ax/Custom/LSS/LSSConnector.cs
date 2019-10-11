using ErpConnector.Ax.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using ErpConnector.Common;
using ErpConnector.Common.Exceptions;
using ErpConnector.Ax.Custom.LSS.Microsoft.Dynamics.DataEntities;
using ErpConnector.Common.Util;
using ErpConnector.Ax.DTO;
using System.Configuration;
using ErpConnector.Ax.Authentication;
using ErpConnector.Ax.Custom.LSS.Modules;

namespace ErpConnector.Ax.Custom.LSS
{
    public class LSSConnector : AxODataConnector
    {
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
                        master.ProductLifecycleStateId = masterLifecycleState;
                        var erpMaster = CreateMaster(new List<ReleasedProductMaster> { master });

                        if (erpMaster.Exception != null)
                        {
                            DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, false, erpMaster.Exception.ErrorMessage, erpMaster.Exception.StackTrace,-1);
                            OnTaskCompleted(this, new ErpTaskCompletedArgs { Exception = erpMaster.Exception, ActionId = actionId, Status = 3 });
                            return actionId;
                        }
                        else if (erpMaster.WriteObject.ProductNumber.ToLower().Trim() != plcPerMaster.First().product_no.ToLower().Trim())
                        {
                            DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, false, null, null,-1);
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

                        DataWriter.LogErpActionStep(actionId, "Item create: write Product Master", startTime, true, null, null,-1);
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
                            ProductLifecycleStateId = variantPlc
                        };
                        startTime = DateTime.Now;
                        //var erpVariants = ServiceConnector.CallOdataEndpointPost<ReleasedProductVariantDTO>("ReleasedProductVariants", null, variant).Result;
                        variantList.Add(variant);
                    }
                    var erpVariants = UpdateVariants(variantList);
                    if (erpVariants.Exception != null)
                    {
                        DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Variant", startTime, false, erpVariants.Exception.ErrorMessage, erpVariants.Exception.StackTrace,-1);
                        OnTaskCompleted(this, new ErpTaskCompletedArgs { Exception = erpVariants.Exception, ActionId = actionId, Status = 3 });
                        return actionId;
                    }
                    DataWriter.LogErpActionStep(actionId, "Item create: write Released Product Variant", startTime, true, null, null,-1);
                    AxDbHandler.UpdateProductLifeCycleState(plcUpdateId, actionId, 2);
                    OnTaskCompleted(this, new ErpTaskCompletedArgs { ActionId = actionId, Status = 2 });
                    return actionId;
                }
            }
            OnTaskCompleted(this, new ErpTaskCompletedArgs { ActionId = actionId, Status = 2 });
            return actionId;
        }

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
            LSSODataContextConnector<ReleasedProductMaster> axConnector = new UpdateProductMaster<ReleasedProductMaster>(oAuthHelper, logMessageHandler: WriteLine, enableCrossCompany: true);
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
            LSSODataContextConnector<ReleasedProductVariant> axConnector = new UpdateReleasedProductVariants<ReleasedProductVariant>(oAuthHelper, logMessageHandler: WriteLine, enableCrossCompany: true);
            axConnector.CreateRecordInAX(dataarea, variants);
            return new GenericWriteObject<ReleasedProductVariant> { Exception = null, WriteObject = variants[0] };
        }

    }
}
