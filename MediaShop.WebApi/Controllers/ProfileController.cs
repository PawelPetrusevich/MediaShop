using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using MediaShop.Common.Models.User;

namespace MediaShop.WebApi.Controllers
{
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Ok(new List<Profile>());
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult GetById(long id)
        {
            return this.Ok(new Profile());
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Profile profile)
        {
            return this.Content<Profile>(HttpStatusCode.Created, new Profile());
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]Profile profile)
        {
            return this.Ok(new Profile());
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody]long? id)
        {
            return this.Ok();
        }
    }
}
