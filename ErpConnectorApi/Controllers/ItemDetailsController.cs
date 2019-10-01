using ErpConnector.Common.Services;
using ErpConnector.Common.DTO;
using ErpConnector.Common.Util;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ErpConnectorApi.Controllers
{
    [RoutePrefix("item_details")]
    [Authorize]
    public class ItemDetailsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new ItemDetailsDTO[1] { new ItemDetailsDTO() });
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]ItemDetailsDTO[] value, CancellationToken token)
        {
            //Exception handling to be done globally
            var service = new DataContractService();
            await service.WriteToDB<ItemDetailsDTO>(value.GetDataReader(), token, "erp.item_details_refresh");
            return Ok();

        }

    }
}
