using System.Web.Http;
using System.Net;
using System;
using MediaShop.Common.Enums;
using Swashbuckle.Swagger.Annotations;
using MediaShop.Common.Exceptions.CartExseptions;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models;

namespace MediaShop.WebApi.Areas.Content.Controllers
{
    [RoutePrefix("api/cart")]
    public class CartController : ApiController
    {
        private readonly ICartService<ContentCartDto> _cartService;

        public CartController(ICartService<ContentCartDto> cartService)
        {
            this._cartService = cartService;
        }

        public IHttpActionResult Get()
        {
            return this.Ok();
        }

        [HttpPost]
        [Route("addContent")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(ContentCart))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public IHttpActionResult Post(long contentId, string categoryName)
        {
            try
            {
                return this.Ok(_cartService.AddInCart(contentId, categoryName));
            }
            catch (FormatException error)
            {
                return BadRequest(error.Message);
            }
            catch (ExistContentInCartExceptions error)
            {
                return BadRequest(error.Message);
            }
            catch (AddContentInCartExceptions error)
            {
                return InternalServerError(error);
            }
        }

        [HttpPut]
        [Route("changeStateContent")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(ContentCart))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public IHttpActionResult Put(long contentId, CartEnums.StateCartContent contentState)
        {
            try
            {
                return this.Ok(_cartService.SetState(contentId, contentState));
            }
            catch (ExistContentInCartExceptions error)
            {
                return BadRequest(error.Message);
            }
            catch (UpdateContentInCartExseptions error)
            {
                return InternalServerError(error);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete()
        {
            return this.Ok();
        }
    }
}
