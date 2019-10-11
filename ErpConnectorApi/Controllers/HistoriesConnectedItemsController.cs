using ErpConnector.Common.Services;
using ErpConnector.Common.DTO;
using ErpConnector.Common.Util;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ErpConnectorApi.Controllers
{
    [RoutePrefix("histories_connected_items")]
    [Authorize]
    public class HistoriesConnectedItemsController : ApiController
    {
        [HttpGet]        
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new HistoriesConnectedItemsDTO[1] { new HistoriesConnectedItemsDTO() });
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]HistoriesConnectedItemsDTO[] value, CancellationToken token)
        {
            //Exception handling to be done globally
            var service = new DataContractService();
            await service.WriteToDB<HistoriesConnectedItemsDTO>(value.GetDataReader(), token, "erp.histories_connected_items_refresh");
            return Ok();

        }

    }
}
