using System.Threading.Tasks;
using System.Web.Http;
using ErpConnector.Common.DTO;
using ErpConnector.Common.Services;
using ErpConnector.Common.Util;
using System.Threading;

namespace ErpConnectorApi.Controllers
{
    [RoutePrefix("items")]
    [Authorize]
    public class ItemsController : ApiController
    {
        // GET: api/Items
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new ItemsDTO[1] { new ItemsDTO() });
        }

        // GET: api/Items/5
        //[HttpGet]
        //[Route("{id}")]
        //public ItemsDTO Get(int id)
        //{
        //    return new ItemsDTO();
        //}

        // POST: api/Items
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]ItemsDTO[] value, CancellationToken token)
        {
            //Exception handling to be done globally
            var service = new DataContractService();
            await service.WriteToDB<ItemsDTO>(value.GetDataReader(), token, "erp.items_refresh");
            return Ok();
            
        }

        //// PUT: api/Items/5
        //[HttpPut]
        //[Route("{id}")]
        //public void Put(int id, [FromBody]ItemsDTO value)
        //{
        //}

        //// DELETE: api/Items/5
        //[HttpDelete]
        //[Route("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
