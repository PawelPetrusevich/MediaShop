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
            return this.Ok(new List<AccountSettings>());
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult GetById(ulong id)
        {
            return this.Ok(new AccountSettings());
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] AccountSettings settings)
        {
            return this.Content<AccountSettings>(HttpStatusCode.Created, new AccountSettings());
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]AccountSettings settings)
        {
            return this.Ok(new AccountSettings());
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody]ulong id)
        {
            return this.Ok();
        }
    }
}
