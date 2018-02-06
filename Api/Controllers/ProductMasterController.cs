using ErpDTO.DTO;
using System.Web.Http;

namespace ErpConnector.Controllers
{
    [RoutePrefix("product_master")]
    public class ProductMasterController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] ProductMasterWriteDTO productMaster)
        {
            var connector = new AxConnect.AXODataConnector();
            //var pm = connector.CreateProductMaster(productMaster);
            //if (pm != null)
            //{
            //    return Ok(pm);
            //}
            return BadRequest();
        }
    }
}
