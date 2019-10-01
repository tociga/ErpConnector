using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using ErpConnector.Common.DTO;
using ErpConnector.Common.Services;
using ErpConnector.Common.Util;

namespace ErpConnectorApi.Controllers
{
    [RoutePrefix("vendors")]
    [Authorize]
    public class VendorsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new VendorsDTO[1] { new VendorsDTO() });
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]VendorsDTO[] value, CancellationToken token)
        {
            //Exception handling to be done globally
            var service = new DataContractService();
            await service.WriteToDB<VendorsDTO>(value.GetDataReader(), token, "erp.vendors_refresh");
            return Ok();

        }

    }
}
