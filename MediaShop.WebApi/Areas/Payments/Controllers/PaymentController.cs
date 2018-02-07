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
    }
}
