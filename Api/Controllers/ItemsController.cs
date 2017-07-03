using System.Web.Http;
using ErpDTO.DTO;
using AxConnect;

namespace ErpConnector.Controllers
{
    [AgrAuthorize]
    [RoutePrefix("items")]
    public class ItemsController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody]ItemDTO item)
        {
            var connector = new AXODataConnector();
            return Ok(connector.CreateItem(item));
            //return BadRequest();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var prod = new ReleasedProductMasterWriteDTO
            {
                SalesChargeProductGroupId = "smoople",
                ItemNumber = "gloob",
                SearchName = "schmeggle glooble"
            };
            var item = new ItemDTO { releasedProductMaster = prod };
            item.id = 20020;
            return Ok(item);
        }
    }
}
