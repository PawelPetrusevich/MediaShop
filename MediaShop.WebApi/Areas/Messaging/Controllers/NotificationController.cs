using System.Net;
using System.Web.Http;

namespace MediaShop.WebApi.Areas.Messaging.Controllers
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
        public IHttpActionResult Get(long id)
        {
            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]long userId, [FromBody]string message)
        {
            return this.Content(HttpStatusCode.Created, string.Empty);
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]long id, [FromBody]string message)
        {
            return this.Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody]long id)
        {
            return this.Ok();
        }
    }
}
