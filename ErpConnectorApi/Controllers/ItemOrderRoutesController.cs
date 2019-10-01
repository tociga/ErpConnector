using ErpConnector.Common.Services;
using ErpConnector.Common.DTO;
using ErpConnector.Common.Util;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ErpConnectorApi.Controllers
{
    [RoutePrefix("item_order_routes")]
    [Authorize]
    public class ItemOrderRoutesController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new ItemOrderRoutesDTO[1] { new ItemOrderRoutesDTO() });
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]ItemOrderRoutesDTO[] value, CancellationToken token)
        {
            //Exception handling to be done globally
            var service = new DataContractService();
            await service.WriteToDB<ItemOrderRoutesDTO>(value.GetDataReader(), token, "erp.item_order_routes_refresh");
            return Ok();

        }
    }
}
