using AxConnect.Modules;
using System.Collections.Generic;
using System.Web.Http;

namespace ErpConnector.Controllers
{
    [RoutePrefix("items")]
    public class ItemsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult Create()
        {
            var connector = new AxConnect.AXODataConnector();
            connector.CreateItemTest();
            return Ok();
        }
    }

    public class Item
    {
        public int id { get; set; }
    }
}
