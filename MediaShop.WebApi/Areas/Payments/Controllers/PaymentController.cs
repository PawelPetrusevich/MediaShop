using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.PaymentModel;

namespace MediaShop.WebApi.Areas.Payments.Controllers
{
    [RoutePrefix("api/payment")]
    public class PaymentController : ApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        /// <summary>
        /// Get all transaction
        /// </summary>
        /// <returns>collection transactions</returns>
        [HttpGet]
        [Route("getall")]
        public IHttpActionResult Get()
        {
            return this.Ok();
        }

        /// <summary>
        /// Get all transaction by account
        /// </summary>
        /// <param name="id">account`s identificator</param>
        /// <returns>IHttpActionResult</returns>
        [HttpGet]
        [Route("gettransactions")]
        public IHttpActionResult Get([FromUri] long id)
        {
            return this.Ok();
        }

        /// <summary>
        /// Method of payment for the content by the buyer
        /// </summary>
        /// <param name="contentId">contents identifier</param>
        /// <returns>transaction`s identificator</returns>
        [HttpPost]
        [Route("paymentbuyer")]
        [SwaggerResponseRemoveDefaults]
        public IHttpActionResult Post([FromUri] long contentId)
        {
                return this.Ok();
        }

        /// <summary>
        /// Update transaction
        /// </summary>
        /// <param name="id">transaction`s identificator</param>
        /// <returns>IHttpActionResult</returns>
        [HttpPut]
        [Route("updatetransaction")]
        [SwaggerResponseRemoveDefaults]
        public IHttpActionResult Put([FromUri] long id)
        {
                return this.Ok();
        }

        /// <summary>
        /// Delete transaction
        /// </summary>
        /// <param name="id">transaction`s identificator</param>
        /// <returns>IHttpActionResult</returns>
        [HttpDelete]
        [Route("deletetransaction")]
        public IHttpActionResult Delete([FromUri] long id)
        {
                return this.Ok();
        }
    }
}
