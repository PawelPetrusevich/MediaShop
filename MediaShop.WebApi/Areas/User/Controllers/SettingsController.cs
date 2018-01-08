using System.Collections.Generic;
using System.Net;
using System.Resources;
using System.Web.Http;
using MediaShop.Common.Models.User;
using MediaShop.WebApi.Properties;

namespace MediaShop.WebApi.Areas.User.Controllers
{
    [RoutePrefix("api/settings")]
    public class SettingsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Ok(new List<Settings>());
        }

        [HttpPost]
        [Route("modify")]
        public IHttpActionResult ModifySettings([FromBody] Settings settings)
        {
            if (settings == null || ModelState.IsValid)
            {
                return BadRequest(Resources.EmptyRegisterDate);
            }

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
