using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErpConnector.Common.Util;
using System.Threading;
using ErpConnector.Common.DTO;
using ErpConnector.Common.DTO.CustomData;
using ErpConnector.Common.Constants;

namespace ErpConnector.Common.Services
{
    public class DataContractService
    {
        public async Task<SyncDTO> BeginSync(SyncDTO syncDTO)
        {
            if (syncDTO.status != AGRConstants.SYNC_TASK.START)
            {
                syncDTO.message = "The status of the post data object is inconsistent with the expexted value";
                return syncDTO;
            }

            if (syncDTO.id.HasValue && syncDTO.id.Value > 0)
            {
                syncDTO.message = "Expected the value of the id property to not be set, ignoring the value sent in.";
                syncDTO.id = null;
            }
            
            DataWriter.RunNonQueryWithoutParamsStg("erp.truncate_refresh_tables", false);
            return await DataWriter.MergeSyncTask(syncDTO);
            
        }

        public async Task<SyncDTO> EndSync(SyncDTO syncDTO)
        {
            if (syncDTO.status == AGRConstants.SYNC_TASK.START)
            {
                syncDTO.message = "The status of the post data object is inconsistent with the expexted value";
                return syncDTO;
            }
            if (syncDTO.id == null || syncDTO.id.Value <= 0)
            {
                syncDTO.message = "Expected the value of the id property be set, The update failed";
                return syncDTO;
            }

            
            DataWriter.RunNonQueryWithoutParamsStg("erp.merge_refresh_tables", true);
            return await DataWriter.MergeSyncTask(syncDTO);
        }

        public async Task WriteToDB<T>(IGenericDataReader<T> reader, CancellationToken token, string fullTableName)
        {            
            await DataWriter.WriteDTO<T>(reader, token, fullTableName);
        }

        public async Task WriteItemsV2ToDB(List<ItemsV2DTO> itemv2, CancellationToken token)
        {
            var details = new List<ItemDetailsV2DTO>();
            var routes = new List<ItemOrderRoutesV2DTO>();
            var skuAttributes = new List<SKUCustomDataDTO>();
            var prodAttributes = new List<ProductCustomDataDTO>();
            var items = new List<ItemsV2DTO>();
            foreach (var item in itemv2)
            {
                foreach(var detail in item.item_detials)
                {
                    detail.item_no = item.item_no;
                    foreach(var route in detail.item_order_routes)
                    {
                        route.item_no = item.item_no;
                        route.location_no = detail.location_no;
                        routes.Add(route);
                    }
                    foreach(var s in detail.sku_attribute_values)
                    {
                        s.item_no = item.item_no;
                        s.location_no = detail.location_no;
                        skuAttributes.Add(s);
                    }
                    details.Add(detail);
                    var i = item.ShallowCopy();
                    i.location_no = detail.location_no;
                    items.Add(i);
                }
                foreach(var a in item.product_attributes)
                {
                    a.item_no = item.item_no;
                    prodAttributes.Add(a);
                }
            }
            await WriteToDB<ItemsV2DTO>(items.GetDataReader(), token, "erp.items_refresh");
            await WriteToDB<ItemDetailsV2DTO>(details.GetDataReader(), token, "erp.item_details_refresh");
            await WriteToDB<ItemOrderRoutesV2DTO>(routes.GetDataReader(), token, "erp.item_order_routes_refresh");
            await WriteToDB<SKUCustomDataDTO>(skuAttributes.GetDataReader(), token, "erp.sku_product_attributes_refresh");
            await WriteToDB<ProductCustomDataDTO>(prodAttributes.GetDataReader(), token, "erp.product_attributes_refresh");

        }

        public async Task<IList<AGROrderHeaderDTO>> GetAGROrderHeaders(CancellationToken token)
        {            
            var result = await DataWriter.GetAGROrderHeaders(token);
            foreach(var order in result)
            {
                await DataWriter.LogAGROrderAction(new AGROrderResponseDTO
                {
                    agr_order_id = order.agr_order_id,
                    agr_order_type = order.order_type,
                    erp_order_status = AGRConstants.ERP_ORDER_STATUS.COMPLETED
                }, token, "order_transfer_to_erp");
            }
            return result;
        }

        public async Task<IList<AGROrderLineDTO>> GetAGROrderLines(int orderId, CancellationToken token)
        {
            var lines = await DataWriter.GetAGROrderLines(orderId, token);
            // filter out lines with qty<=0
            var results = new List<AGROrderLineDTO>();
            results.AddRange(lines.Where(x => x.qty > 0));
            return results;
        }
       
        public async Task LogAGROrderCallback(AGROrderResponseDTO response, CancellationToken token)
        {
            await DataWriter.LogAGROrderCallback(response, token);
            await DataWriter.UpdateOrderStatus(response, token);
        }
    }


}
