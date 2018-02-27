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
using MediaShop.Common.Models.User;

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
        [AllowAnonymous]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Cart))]
        public IHttpActionResult Get()
        {
            long id = 0; //userId from claim
            var user = this.RequestContext.Principal.Identity as ClaimsIdentity;
            if (user != null && user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId) != null)
            {
                if (!long.TryParse(user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId).Value, out id))
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
        [AllowAnonymous]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Cart))]
        public async Task<IHttpActionResult> GetAsync()
        {
            long id = 0; //userId from claim
            var user = this.RequestContext.Principal.Identity as ClaimsIdentity;
            if (user != null && user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId) != null)
            {
                if (!long.TryParse(user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId).Value, out id))
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
        [HttpPost]
        [Route("add/{contentId}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(ContentCartDto))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
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
        [HttpPost]
        [Route("addasync/{contentId}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(ContentCartDto))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public async Task<IHttpActionResult> AddInCartAsync([FromUri] long contentId)
        {
            var userClaims = HttpContext.Current.User.Identity as ClaimsIdentity ??
                             throw new ArgumentNullException(nameof(HttpContext.Current.User.Identity));
            var id = Convert.ToInt64(userClaims.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId)?.Value);

            if (id < 1 || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            var result = await _cartService.AddInCartAsync(contentId, id);
                return this.Ok(result);
        }

        /// <summary>
        /// Delete content from Cart
        /// </summary>
        /// <param name="data">Content for delete</param>
        /// <returns>Deleted content</returns>
        [HttpDelete]
        [Route("deletecontent")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(ContentCartDto))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public IHttpActionResult DeleteContent([FromBody] ContentCartDto data)
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
        /// <returns>Deleted content</returns>
        [HttpDelete]
        [Route("deletecontentasync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(ContentCartDto))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public async Task<IHttpActionResult> DeleteContentAsync([FromBody] ContentCartDto data)
        {
            if (data == null)
            {
                return BadRequest(Resources.EmtyData);
            }

            var result = await _cartService.DeleteContentAsync(data);
            return this.Ok(result);
        }

        /// <summary>
        /// Delete content from Cart by content Id
        /// </summary>
        /// <param name="id">ContentId for delete</param>
        /// <returns>Deleted content</returns>
        [HttpDelete]
        [Route("deletecontentbyidasync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(long))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public async Task<IHttpActionResult> DeleteContentAsync([FromUri] long id)
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
        /// <param name="userId">user Id</param>
        /// <returns>Cart</returns>
        [HttpDelete]
        [Route("clearcart")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Return cart of user", typeof(Cart))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public IHttpActionResult Delete([FromUri] long userId)
        {
            if (userId <= 0)
            {
                return BadRequest(Resources.IncorrectId);
            }

            var result = _cartService.DeleteOfCart(userId);
            return this.Ok(result);
        }

        /// <summary>
        /// Delete all content from Cart
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Cart</returns>
        [HttpDelete]
        [Route("clearcartasync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(Cart))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "", typeof(Exception))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "", typeof(string))]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public async Task<IHttpActionResult> DeleteAsync([FromUri] long userId)
        {
            if (userId <= 0)
            {
                return BadRequest(Resources.IncorrectId);
            }

            var result = await _cartService.DeleteOfCartAsync(userId);
            return this.Ok(result);
        }
    }
}
