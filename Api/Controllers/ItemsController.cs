using AxConnect.DTO;
using System.Web.Http;

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
            var result = connector.CreateItem(item);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
