using System.Collections.Generic;
using System.Web.Http;

namespace ErpConnector.Controllers
{
    [RoutePrefix("sales")]
    public class SalesController : ApiController
    {
        // GET api/items
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
