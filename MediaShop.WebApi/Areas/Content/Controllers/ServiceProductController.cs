using System.Web.Http;

namespace MediaShop.WebApi.Areas.Content.Controllers
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
