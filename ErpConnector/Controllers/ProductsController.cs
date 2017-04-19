using System.Collections.Generic;
using System.Web.Http;

namespace ErpConnector.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        // GET api/items
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
