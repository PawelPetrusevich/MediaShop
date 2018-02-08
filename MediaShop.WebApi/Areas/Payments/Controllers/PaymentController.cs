﻿using System;
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
using MediaShop.Common.Dto.Payment;

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
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(PayPal.Api.Payment))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        public IHttpActionResult ResultPayment([FromBody] PayPal.Api.Payment payment)
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
        [SwaggerResponse(statusCode: HttpStatusCode.Redirect, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public IHttpActionResult PayPalPayment([FromBody] Cart cart)
        {
            try
            {
                string paymentUrl = _paymentService.GetPayment(cart, Url.Request.RequestUri.ToString());
                this.StatusCode(HttpStatusCode.Redirect);
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
        [Route("paypalpayment/executepaypalpayment/{guid}/{paymentid}/{token}")]
        [SwaggerResponse(statusCode: HttpStatusCode.OK, description: "", type: typeof(PayPalPaymentDto))]
        [SwaggerResponse(statusCode: HttpStatusCode.BadRequest, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public IHttpActionResult ExecutePayment(string guid, string paymentId, string token)
        {
            try
            {
                var payment = _paymentService.ExecutePayment(paymentId);
                return Ok(payment);
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
        [Route("paypalpayment/paymentcancelled/{token}")]
        [SwaggerResponse(statusCode: HttpStatusCode.Redirect, description: "", type: typeof(string))]
        [SwaggerResponse(statusCode: HttpStatusCode.InternalServerError, description: "", type: typeof(Exception))]
        public IHttpActionResult PaymentCancelled(string token)
        {
            try
            {
                this.StatusCode(HttpStatusCode.Redirect);
                return Redirect("api/cart/getcart");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
