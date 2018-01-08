using System.Net;
using System.Web.Http;

namespace MediaShop.WebApi.Controllers
{
    public class NotificationController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Ok();
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult Get(ulong id)
        {
            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]ulong userId, [FromBody]string message)
        {
            return this.Content(HttpStatusCode.Created, string.Empty);
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]ulong id, [FromBody]string message)
        {
            return this.Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody]ulong id)
        {
            return this.Ok();
        }
    }
}
