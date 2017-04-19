using System.Collections.Generic;
using System.Web.Http;

namespace ErpConnector.Controllers
{
    [RoutePrefix("locations")]
    public class LocationsController : ApiController
    {
        // GET api/items
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
