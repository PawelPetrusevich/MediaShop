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
            return this.Ok(new List<AccountProfile>());
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult GetById(ulong id)
        {
            return this.Ok(new AccountProfile());
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] AccountProfile profile)
        {
            return this.Content<AccountProfile>(HttpStatusCode.Created, new AccountProfile());
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]AccountProfile profile)
        {
            return this.Ok(new AccountProfile());
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody]ulong? id)
        {
            return this.Ok();
        }
    }
}
