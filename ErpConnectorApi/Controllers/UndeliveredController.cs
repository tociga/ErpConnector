using System;
using ErpConnector.Common.Services;
using ErpConnector.Common.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using ErpConnector.Common.Util;

namespace ErpConnectorApi.Controllers
{
    [RoutePrefix("undelivered")]
    [Authorize]
    public class UndeliveredController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new UndeliveredDTO[1] { new UndeliveredDTO() });
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]UndeliveredDTO[] value, CancellationToken token)
        {
            //Exception handling to be done globally
            var service = new DataContractService();
            await service.WriteToDB<UndeliveredDTO>(value.GetDataReader(), token, "erp.undelivered_refresh");
            return Ok();

        }

    }
}
