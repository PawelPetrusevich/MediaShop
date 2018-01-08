using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MediaShop.Common.Models.User;

namespace MediaShop.WebApi.Controllers
{
    [RoutePrefix("api/settings")]
    public class SettingsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Ok(new List<Settings>());
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult GetById(long id)
        {
            return this.Ok(new Settings());
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Settings settings)
        {
            return this.Content<Settings>(HttpStatusCode.Created, new Settings());
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]Settings settings)
        {
            return this.Ok(new Settings());
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody]long id)
        {
            return this.Ok();
        }
    }
}
