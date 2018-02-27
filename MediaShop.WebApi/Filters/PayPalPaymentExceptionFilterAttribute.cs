using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using MediaShop.Common.Exceptions.PaymentExceptions;
using MediaShop.Common.Exceptions.CartExceptions;
using NLog;

namespace MediaShop.WebApi.Filters
{
    /// <summary>
    /// Filter Exception for PaymentController
    /// </summary>
    public class PayPalPaymentExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public bool AllowMultiple { get; }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (actionExecutedContext.Exception != null)
            {
                _logger.Error(actionExecutedContext.Exception.ToString());
                switch (actionExecutedContext.Exception)
                {
                    // Exception: if controllers or methods argument is null
                    case ArgumentNullException error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.BadRequest, error.Message);
                        break;

                    // Exception: if controllers or methods argument is not valid
                    case ArgumentException error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.BadRequest, error.Message);
                        break;

                    // Exception: if content do not update in database
                    case UpdateContentInCartExseptions error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.InternalServerError, error.Message);
                        break;

                    // Exception: if decerializable data is not valide
                    case InvalideDecerializableExceptions error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.BadRequest, error.Message);
                        break;

                    // Exception: if payment already exist in database
                    case ExistPaymentException error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.BadRequest, error.Message);
                        break;

                    // Exception: if operation payment is only status created or failed
                    case OperationPaymentException error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.BadRequest, error.Message);
                        break;

                    // Exception: if payment do not add in database
                    case AddPaymentException error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.InternalServerError, error.Message);
                        break;

                    // Exception: if defrayal do not add in database
                    case AddDefrayalException error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.InternalServerError, error.Message);
                        break;

                    // Exception: if defrayal do not delete from database
                    case DeleteDefrayalException error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.InternalServerError, error.Message);
                        break;

                    // Exception: exception transformations in enum type
                    case OverflowException error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.BadRequest, error.Message);
                        break;

                    case EmptyCartException error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.BadRequest, error.Message);
                        break;

                    case ContentCartPriceException error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.BadRequest, error.Message);
                        break;

                    case PayPalException error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.InternalServerError, error.Message);
                        break;

                    case PaymentsException error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                           HttpStatusCode.InternalServerError, error.Message);
                        break;

                    // Exception: not counted exceptions
                    default:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.InternalServerError, actionExecutedContext.Exception);
                        break;
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}