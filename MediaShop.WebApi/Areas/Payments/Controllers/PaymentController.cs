using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Threading.Tasks;
using MediaShop.WebApi.Filters;
using Newtonsoft.Json;
using Swashbuckle.Swagger.Annotations;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.PaymentModel;
using MediaShop.Common.Exceptions.PaymentExceptions;
using MediaShop.Common.Models;
using MediaShop.Common.Exceptions.PaymentExceptions;
using MediaShop.Common.Exceptions.CartExceptions;
using MediaShop.Common.Dto.Payment;
using System.Web.Http.Cors;
using MediaShop.WebApi.Properties;
using System.Web;
using System.Security.Claims;
using MediaShop.Common.Models.User;

namespace MediaShop.WebApi.Areas.Payments.Controllers
{
    [PayPalPaymentExceptionFilter]
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/payment")]
    public class PaymentController : ApiController
    {
        private readonly IPayPalPaymentService _paymentService;

        public PaymentController(IPayPalPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        /// <summary>
        /// Create paypal payment by Cart
        /// </summary>
        /// <param name="cart">user Cart</param>
        /// <returns>url for payment</returns>
        [HttpPost]
        [Route("paypalpayment")]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public IHttpActionResult PayPalPayment([FromBody] Cart cart)
        {
            var url = this.Request.Headers.Referrer.Scheme + $"://" + this.Request.Headers.Referrer.Host + ":" + this.Request.Headers.Referrer.Port;
            string paymentUrl = _paymentService.GetPayment(cart, url);
            return Ok(paymentUrl);
        }

        /// <summary>
        /// Execute of payment
        /// </summary>
        /// <param name="paymentId">payment id</param>
        /// <param name="token">token</param>
        /// <returns>payment Info</returns>
        [HttpGet]
        [Route("paypalpayment/executepaypalpayment")]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(PayPalPaymentDto))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public IHttpActionResult ExecutePayment(string paymentId, string token)
        {
            var userClaims = HttpContext.Current.User.Identity as ClaimsIdentity ??
                             throw new ArgumentNullException(nameof(HttpContext.Current.User.Identity));
            var id = Convert.ToInt64(userClaims.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId)?.Value);

            if (id < 1 || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            var payment = _paymentService.ExecutePayment(paymentId, id);
            return Ok(payment);
        }

        /// <summary>
        /// Execute of payment
        /// </summary>
        /// <param name="paymentId">payment id</param>
        /// <param name="token">token</param>
        /// <returns>payment Info</returns>
        [HttpGet]
        [Route("paypalpayment/executepaypalpaymentasync")]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(PayPalPaymentDto))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public async Task<IHttpActionResult> ExecutePaymentAsync(string paymentId, string token)
        {
            var userClaims = HttpContext.Current.User.Identity as ClaimsIdentity ??
                             throw new ArgumentNullException(nameof(HttpContext.Current.User.Identity));
            var id = Convert.ToInt64(userClaims.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId)?.Value);

            if (id < 1 || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmtyData);
            }

            var payment = await _paymentService.ExecutePaymentAsync(paymentId, id);
            return Ok(payment);
        }

        /// <summary>
        /// Cancelling paypal payment
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>redirect to Cart</returns>
        [HttpGet]
        [Route("paypalpayment/paymentcancelled/{token}")]
        [SwaggerResponse(statusCode: HttpStatusCode.Redirect, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public IHttpActionResult PaymentCancelled(string token)
        {
            this.StatusCode(HttpStatusCode.Redirect);
            return Redirect("api/cart/getcart");
        }
    }
}
