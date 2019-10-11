using ErpConnector.Common.Services;
using ErpConnector.Common.DTO;
using ErpConnector.Common.Util;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ErpConnectorApi.Controllers
{
    [RoutePrefix("item_group_details")]
    [Authorize]
    public class ItemGroupDetailsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new ItemGroupDetailsDTO[1] { new ItemGroupDetailsDTO() });
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]ItemGroupDetailsDTO[] value, CancellationToken token)
        {
            //Exception handling to be done globally
            var service = new DataContractService();
            await service.WriteToDB<ItemGroupDetailsDTO>(value.GetDataReader(), token, "erp.item_group_details_refresh");
            return Ok();

        }

    }
}
