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

        // [HttpPost]
        // [Route("paypalpayment")]
        // [SwaggerResponse(statusCode: HttpStatusCode.Redirect, description: "", type: typeof(string))]
        // [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        // [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        // public IHttpActionResult PayPalPayment([FromBody] Cart cart)
        // {
        //    string paymentUrl = _paymentService.GetPayment(cart, Url.Request.RequestUri.ToString());
        //    this.StatusCode(HttpStatusCode.Redirect);

        //    // Request.Headers.Add("Access-Control-Allow-Origin", "*");
        //    return Redirect(paymentUrl);
        // }

        [HttpPost]
        [Route("paypalpayment")]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public IHttpActionResult PayPalPayment([FromBody] Cart cart)
        {
            var url = this.Request.Headers.Referrer.Scheme + $"://" + this.Request.Headers.Referrer.Host + ":" + this.Request.Headers.Referrer.Port;
            string paymentUrl = _paymentService.GetPayment(cart, url); // получать url из Header Origin
            return Ok(paymentUrl);
        }

        [HttpGet]
        [Route("paypalpayment/executepaypalpayment")]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(PayPalPaymentDto))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public IHttpActionResult ExecutePayment(string paymentId, string token)
        {
            var payment = _paymentService.ExecutePayment(paymentId);
            return Ok(payment);
        }

        [HttpGet]
        [Route("paypalpayment/executepaypalpaymentasync")]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(PayPalPaymentDto))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public async Task<IHttpActionResult> ExecutePaymentAsync(string paymentId, string token)
        {
            var payment = await _paymentService.ExecutePaymentAsync(paymentId);
            return Ok(payment);
        }

        [HttpGet]
        [Route("paypalpayment/paymentcancelled/{token}")]
        [SwaggerResponse(statusCode: HttpStatusCode.Redirect, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public IHttpActionResult PaymentCancelled(string token)
        {
            this.StatusCode(HttpStatusCode.Redirect);
            return Redirect("api/cart/getcart");
        }
    }
}
