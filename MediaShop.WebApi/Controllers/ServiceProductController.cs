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
        [HttpGet]
        [Route("get")]
        public IHttpActionResult Get()
        {
            var account = new Account() { Login = "Login 1" };
            return this.Redirect(new Uri("http://tut.by"));
        }

        [HttpPost]
        [Route("send")]
        public IHttpActionResult Post(Account data)
        {
            return this.Ok();
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]string data)
        {
            return this.Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody]int i)
        {
            return this.Ok();
        }

    }
}
