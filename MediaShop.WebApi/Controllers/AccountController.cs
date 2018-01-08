using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MediaShop.Common.Models.User;

namespace MediaShop.WebApi.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Ok(new List<Account>());
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult GetById(long id)
        {
            return this.Ok(new Account());
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Account account)
        {
            return this.Content<Account>(HttpStatusCode.Created, new Account());
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]Account account)
        {
            return this.Ok(new Account());
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody]long id)
        {
            return this.Ok();
        }
    }
}
