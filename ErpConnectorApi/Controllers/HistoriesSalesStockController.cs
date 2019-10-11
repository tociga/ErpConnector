using ErpConnector.Common.Services;
using ErpConnector.Common.DTO;
using ErpConnector.Common.Util;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ErpConnectorApi.Controllers
{
    [RoutePrefix("histories_sales_stock")]
    [Authorize]
    public class HistoriesSalesStockController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new HistoriesSalesStockDTO[1] { new HistoriesSalesStockDTO() });
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]HistoriesSalesStockDTO[] value, CancellationToken token)
        {
            //Exception handling to be done globally
            var service = new DataContractService();
            await service.WriteToDB<HistoriesSalesStockDTO>(value.GetDataReader(), token, "erp.histories_sales_stock_refresh");
            return Ok();

        }

    }
}
