using System.Web.Http;
using ErpDTO.DTO;

namespace ErpConnector.Controllers
{
    [RoutePrefix("items")]
    public class ItemsController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] ItemDTO item)
        {
            var connector = new AxConnect.AXODataConnector();
            connector.CreateItemTest();
            return Ok();
        }
    }
}
