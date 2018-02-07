using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Swashbuckle.Swagger.Annotations;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.PaymentModel;
using MediaShop.Common.Exceptions.PaymentExceptions;
using MediaShop.Common.Models;
using MediaShop.Common.Exceptions.PaymentExceptions;
using MediaShop.Common.Exceptions.CartExceptions;

namespace MediaShop.WebApi.Areas.Payments.Controllers
{
    [RoutePrefix("api/payment")]
    public class PaymentController : ApiController
    {
        private readonly IPayPalPaymentService _paymentService;

        public PaymentController(IPayPalPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        /// <summary>
        /// Method Payment controller
        /// </summary>
        /// <param name="payment">deserialize object Payment</param>
        /// <returns>statusCode</returns>
        [HttpPost]
        [Route("resultpayment")]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(PayPalPayment))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        public IHttpActionResult ResultPayment([FromBody] PayPalPayment payment)
        {
            try
            {
                return Ok(_paymentService.AddPayment(payment));
            }
            catch (InvalideDecerializableExceptions error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPost]
        [Route("paypalpayment")]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public IHttpActionResult PayPalPayment([FromBody] Cart cart)
        {
            try
            {
                string paymentUrl = _paymentService.GetPayment(cart, Url.ToString());
                return Redirect(paymentUrl);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EmptyCartException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ContentCartPriceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("executepaypalpayment/{guid}/{paymentid}/{token}/{payerid}")]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(PayPalPayment))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public IHttpActionResult ExecutePayment(string guid, string paymentId, string token, string PayerID)
        {
            try
            {
                var payment = _paymentService.ExecutePayment(paymentId, PayerID);
                return Ok();
            }
            catch (PayPalException ex)
            {
                return InternalServerError(ex);
            }
            catch (PaymentsException ex)
            {
                return InternalServerError(ex);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("paymentcancelled/{guid}/{paymentid}/{token}/{payerid}")]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(Cart))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public IHttpActionResult PaymentCancelled(string token)
        {
            try
            {
                return Redirect("api/cart/getcart");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
