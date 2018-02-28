using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using MediaShop.Common.Exceptions.CartExceptions;
using NLog;

namespace MediaShop.WebApi.Filters
{
    /// <summary>
    /// Filter Exceptions for CartController
    /// </summary>
    public class CartExceptionFilterAttribute : Attribute, IExceptionFilter
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

                    // Exception: if controllers argument is not valid
                    case ArgumentException error:
                         actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, error.Message);
                        break;

                    case InvalidIdException error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, error.Message);
                        break;

                    // Exception: if product is not exist in database
                    case NotExistProductInDataBaseExceptions error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, error.Message);
                        break;

                    // Exception: if content already exist in database
                    case ExistContentInCartExceptions error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.BadRequest, error.Message);
                        break;

                    // Exception: if content do not add in database
                    case AddContentInCartExceptions error:
                        actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                            HttpStatusCode.InternalServerError, error.Message);
                        break;

                    // Exception: if content do not delete from database
                    case DeleteContentInCartExceptions error:
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