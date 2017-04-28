using ErpDTO.DTO;
using System.Web.Http;

namespace ErpConnector.Controllers
{
    [RoutePrefix("distinct_product")]
    public class DistinctProductController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] DistinctProductDTO distinctProduct)
        {
            var connector = new AxConnect.AXODataConnector();
            var dp = connector.CreateDistinctProduct(distinctProduct).Result;
            if (dp != null)
            {
                return Ok(dp);
            }
            return BadRequest();
        }
    }
}
