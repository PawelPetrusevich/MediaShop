using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MediaShop.WebApi.Controllers
{
    public class NotificationController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Ok(new List<Notification>());
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult Get(ulong id)
        {
            return this.Ok(new Notification());
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]ulong userId, [FromBody]string message)
        {
            return this.Content<Notification>(HttpStatusCode.Created, new Notification());
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]ulong id, [FromBody]string message)
        {
            return this.Ok(new Notification());
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody]ulong id)
        {
            return this.Ok(new Notification());
        }
    }
}
