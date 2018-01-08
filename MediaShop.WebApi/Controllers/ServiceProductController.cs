using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MediaShop.WebApi.Controllers
{
    public class ServiceProductController : ApiController
    {
        public IHttpActionResult Get()
        {
            return this.Ok();
        }

        public IHttpActionResult Post()
        {
            return this.Ok();
        }

        [HttpPut]
        public IHttpActionResult Put()
        {
            return this.Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete()
        {
            return this.Ok();
        }
    }
}
