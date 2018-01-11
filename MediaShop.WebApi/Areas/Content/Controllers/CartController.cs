using System.Web.Http;
using System.Net;
using System;
using MediaShop.Common.Enums;
using Swashbuckle.Swagger.Annotations;
using MediaShop.Common.Exceptions.CartExseptions;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models;
using MediaShop.WebApi.Properties;

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

        [HttpGet]
        [Route("GetCart")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Cart))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(string))]
        public IHttpActionResult Get([FromUri] long id) //id User
        {
            var cart = _cartService.GetCart(id);
            if (cart == null)
            {
                return InternalServerError();
            }

            return this.Ok(cart);
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
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(ContentCartDto))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        public IHttpActionResult Delete([FromBody] ContentCartDto data)
        {
            if (data == null)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                var result = _cartService.DeleteContent(data);
                return this.Ok(result);
            }
            catch (DeleteContentInCartExseptions ex)
            {
                return InternalServerError(ex);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
