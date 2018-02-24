using System.Web.Http;
using System.Net;
using System;
using System.Threading.Tasks;
using MediaShop.Common.Enums;
using MediaShop.WebApi.Filters;
using Swashbuckle.Swagger.Annotations;
using MediaShop.Common.Exceptions.CartExceptions;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models;
using MediaShop.WebApi.Properties;
using System.Web.Http.Cors;
using System.Web;
using System.Security.Claims;
using System.Linq;

namespace MediaShop.WebApi.Areas.Content.Controllers
{
    [CartExceptionFilter]
    [EnableCors("*", "*", "*")]
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
            var user = HttpContext.Current.User as ClaimsIdentity;
            if (user != null)
            {
                if (!int.TryParse(user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId).Value, out id))
                {
                    throw new InvalidIdException(Resources.IncorrectId);
                }
            }

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
            var user = HttpContext.Current.User as ClaimsIdentity;
            if (user != null)
            {
                if (!int.TryParse(user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId).Value, out id))
                {
                    throw new InvalidIdException(Resources.IncorrectId);
                }
            }

            var cart = await _cartService.GetCartAsync(id);
            return this.Ok(cart);
        }

        /// <summary>
        /// Method for add content in cart
        /// </summary>
        /// <param name="contentId">content id</param>
        /// <returns>IHttpActionResult</returns>
        [HttpGet]
        [Route("add/{contentId}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(ContentCartDto))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public IHttpActionResult AddInCart([FromUri] long contentId)
        {
            var userClaims = HttpContext.Current.User.Identity as ClaimsIdentity ??
                             throw new ArgumentNullException(nameof(HttpContext.Current.User.Identity));
            var id = Convert.ToInt64(userClaims.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId)?.Value);

            if (id < 1 || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            return this.Ok(_cartService.AddInCart(contentId, id));
        }

        /// <summary>
        /// Method for add content in cart
        /// </summary>
        /// <param name="contentId">content id</param>
        /// <returns>Task<IHttpActionResult></IHttpActionResult></returns>
        [HttpGet]
        [Route("addasync/{contentId}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(ContentCartDto))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public async Task<IHttpActionResult> AddInCartAsync([FromUri] long contentId)
        {
            var userClaims = HttpContext.Current.User.Identity as ClaimsIdentity ??
                             throw new ArgumentNullException(nameof(HttpContext.Current.User.Identity));
            var id = Convert.ToInt64(userClaims.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId)?.Value);

            if (id < 1 || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            var result = await _cartService.AddInCartAsync(contentId, 2);
            return this.Ok(result);
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

            var result = _cartService.DeleteContent(data);
            return this.Ok(result);
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

            var result = await _cartService.DeleteContentAsync(data);
            return this.Ok(result);
        }

        /// <summary>
        /// Delete content from Cart
        /// </summary>
        /// <param name="id">ContentId for delete</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletecontentbyidasync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(long))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        public async Task<IHttpActionResult> DeleteAsync([FromBody] long id)
        {
            if (id <= 0)
            {
                return BadRequest(Resources.IncorrectId);
            }

            var result = await _cartService.DeleteContentAsync(id);
            return this.Ok(result);
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

            var result = _cartService.DeleteOfCart(data);
            return this.Ok(result);
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

            var result = await _cartService.DeleteOfCartAsync(data);
            return this.Ok(result);
        }
    }
}
