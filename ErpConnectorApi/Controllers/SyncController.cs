using ErpConnector.Common.DTO;
using ErpConnector.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ErpConnectorApi.Controllers
{
    [RoutePrefix("sync")]
    [Authorize]
    public class SyncController : ApiController
    {
        [HttpPost]
        [Route("begin_sync")]
        public async Task<IHttpActionResult> BeginSync([FromBody]SyncDTO syncDTO)
        {
            DataContractService service = new DataContractService();
            return Ok(await service.BeginSync(syncDTO));
        }

        [HttpPost]
        [Route("end_sync")]
        public async Task<IHttpActionResult> EndSync([FromBody]SyncDTO syncDTO)
        {
            DataContractService service = new DataContractService();
            return Ok(await service.EndSync(syncDTO));
        }
    }
}
