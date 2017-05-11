using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AxConnect;
using System.Net.Http.Headers;

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
        [Route("bom")]
        public IHttpActionResult GetBom()
        {
            AXODataConnector ax = new AXODataConnector();
            ax.GetBom();
            return Ok();
        }

        [Route("po_to")]
        public IHttpActionResult GetPoTo()
        {
            AXODataConnector ax = new AXODataConnector();
            ax.GetPoTo();
            return Ok();
        }

        [HttpGet]
        [Route("token")]
        public IHttpActionResult GetAuthToken()
        {
            return Ok(AXODataConnector.GetAdalToken());
        }

        [HttpGet]
        [Route("db_scripts")]
        public HttpResponseMessage GetDBScript([FromUri]string entity)
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent(AXODataConnector.GetDBScript(entity));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
