using ErpConnector.Common.Services;
using ErpConnector.Common.DTO;
using ErpConnector.Common.Util;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ErpConnectorApi.Controllers
{
    [RoutePrefix("reserved")]
    [Authorize]
    public class ReservedController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new ReservedDTO[1] { new ReservedDTO() });
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]ReservedDTO[] value, CancellationToken token)
        {
            //Exception handling to be done globally
            var service = new DataContractService();
            await service.WriteToDB<ReservedDTO>(value.GetDataReader(), token, "erp.reserved_refresh");
            return Ok();

        }

    }
}
