using System.Web.Http;

namespace ErpConnector.Controllers
{
    [RoutePrefix("items")]
    public class ItemsController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create()
        {
            //var connector = new AxConnect.AXODataConnector();
            //var result = connector.CreateItemTest(item);
            //if (result)
            //{
            //    return Ok();
            //}
            return BadRequest();
        }
    }
}
