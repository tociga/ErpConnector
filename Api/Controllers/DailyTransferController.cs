using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AxConnect;

namespace ErpConnector.Controllers
{
    [RoutePrefix("daily_transfer")]
    public class DailyTransferController : ApiController
    {
        [HttpGet]
        [Route("items")]
        public IHttpActionResult GetItems()
        {
            AXODataConnector ax = new AXODataConnector();
            ax.RunTransfer();
            return Ok();
        }

        [HttpGet]
        [Route("token")]
        public IHttpActionResult GetAuthToken()
        {
            return Ok(AXODataConnector.GetAdalToken());
        }
    }
}
