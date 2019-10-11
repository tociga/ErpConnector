using ErpConnector.Common.DTO;
using ErpConnector.Common.DTO.CustomData;
using ErpConnector.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ErpConnectorApi.Controllers
{
    [RoutePrefix("items_v2")]
    [Authorize]
    public class ItemsV2Controller : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var itemsV2 = new ItemsV2DTO();            
            itemsV2.product_attributes.Add(new ProductCustomDataDTO());
            itemsV2.item_detials.Add(new ItemDetailsV2DTO());
            itemsV2.item_detials[0].sku_attribute_values.Add(new SKUCustomDataDTO());
            itemsV2.item_detials[0].item_order_routes.Add(new ItemOrderRoutesV2DTO());
            return Ok(new ItemsV2DTO[1] { itemsV2 });
        }


        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]ItemsV2DTO[] value, CancellationToken token)
        {
            var service = new DataContractService();
            await service.WriteItemsV2ToDB(value.ToList(), token);
            return Ok();
        }

    }
}
