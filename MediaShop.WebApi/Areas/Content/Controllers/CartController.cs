using System.Web.Http;
using System.Net;
using System;
using System.Threading.Tasks;
using MediaShop.Common.Enums;
using Swashbuckle.Swagger.Annotations;
using MediaShop.Common.Exceptions.CartExceptions;
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

        /// <summary>
        /// Get Cart for User
        /// </summary>
        /// <returns>Cart</returns>
        [HttpGet]
        [Route("getcart")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Cart))]
        public IHttpActionResult Get()
        {
            var id = 1; //userId from claim
            var cart = _cartService.GetCart(id);
            return this.Ok(cart);
        }

        /// <summary>
        /// Get Cart for User
        /// </summary>
        /// <returns>Cart</returns>
        [HttpGet]
        [Route("getcartasync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Cart))]
        public async Task<IHttpActionResult> GetAsync()
        {
            var id = 1; //userId from claim
            var cart = await _cartService.GetCartAsync(id);
            return this.Ok(cart);
        }

        /// <summary>
        /// Method for add content in cart
        /// </summary>
        /// <param name="contentId">content id</param>
        /// <returns>IHttpActionResult</returns>
        [HttpPost]
        [Route("add")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(ContentCartDto))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public IHttpActionResult Post(long contentId)
        {
            try
            {
                return this.Ok(_cartService.AddInCart(contentId));
            }
            catch (ArgumentException error)
            {
                return BadRequest(error.Message);
            }
            catch (NotExistProductInDataBaseExceptions error)
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
            catch (Exception error)
            {
                return InternalServerError(error);
            }
        }

        /// <summary>
        /// Method for add content in cart
        /// </summary>
        /// <param name="contentId">content id</param>
        /// <returns>Task<IHttpActionResult></IHttpActionResult></returns>
        [HttpPost]
        [Route("addasync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(ContentCartDto))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public async Task<IHttpActionResult> PostAsync(long contentId)
        {
            try
            {
                var result = await _cartService.AddInCartAsync(contentId);
                return this.Ok(result);
            }
            catch (ArgumentException error)
            {
                return BadRequest(error.Message);
            }
            catch (NotExistProductInDataBaseExceptions error)
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
            catch (Exception error)
            {
                return InternalServerError(error);
            }
        }

        /// <summary>
        /// Delete content from Cart
        /// </summary>
        /// <param name="data">Content for delete</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletecontent")]
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

        /// <summary>
        /// Delete content from Cart
        /// </summary>
        /// <param name="data">Content for delete</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletecontentasync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(ContentCartDto))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        public async Task<IHttpActionResult> DeleteAsync([FromBody] ContentCartDto data)
        {
            if (data == null)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                var result = await _cartService.DeleteContentAsync(data);
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

        /// <summary>
        /// Delete all content from Cart
        /// </summary>
        /// <param name="data">Cart</param>
        /// <returns>Cart</returns>
        [HttpDelete]
        [Route("clearcart")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Cart))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        public IHttpActionResult Delete([FromBody] Cart data)
        {
            if (data == null)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                var result = _cartService.DeleteOfCart(data);
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

        /// <summary>
        /// Delete all content from Cart
        /// </summary>
        /// <param name="data">Cart</param>
        /// <returns>Cart</returns>
        [HttpDelete]
        [Route("clearcartasync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Cart))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        public async Task<IHttpActionResult> DeleteAsync([FromBody] Cart data)
        {
            if (data == null)
            {
                return BadRequest(Resources.EmtyData);
            }

            try
            {
                var result = await _cartService.DeleteOfCartAsync(data);
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
