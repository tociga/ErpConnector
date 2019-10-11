using ErpConnector.Common.DTO;
using ErpConnector.Common.Services;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ErpConnectorApi.Controllers
{
    [RoutePrefix("agr_order")]
    [Authorize]
    public class AGROrderController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(CancellationToken token)
        {
            var service = new DataContractService();
            return Ok(await service.GetAGROrderHeaders(token));
        }

        [HttpGet]
        [Route("{id:int}/lines")]
        public async Task<IHttpActionResult> GetLines([FromUri]int id, CancellationToken token)
        {
            var service = new DataContractService();
            return Ok(await service.GetAGROrderLines(id, token));
        }

        [HttpPost]
        [Route("agr_order_call_back")]
        public async Task<IHttpActionResult> AGROrderCallBack([FromBody]AGROrderResponseDTO response, CancellationToken token)
        {
            if (response == null)
            {
                return BadRequest("The response was not deserialized correctly.");
            }
            var service = new DataContractService();
            await service.LogAGROrderCallback(response, token);
            return Ok();
        }

        [HttpPost]
        [Route("agr_order_call_backs")]
        public async Task<IHttpActionResult> AGROrderCallBacks([FromBody]AGROrderResponseDTO[] response, CancellationToken token)
        {
            if (response == null)
            {
                return BadRequest("The response was not deserialized correctly.");
            }
            var service = new DataContractService();
            foreach (var r in response)
            {
                await service.LogAGROrderCallback(r, token);
            }
            return Ok();
        }
    }

}
