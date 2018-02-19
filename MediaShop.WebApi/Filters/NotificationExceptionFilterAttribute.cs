using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace MediaShop.WebApi.Areas.Messaging.Controllers
{
    public class NotificationExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public bool AllowMultiple { get; }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            actionExecutedContext.Response = actionExecutedContext.Request
                .CreateErrorResponse(HttpStatusCode.InternalServerError, actionExecutedContext.Exception.Message);
            return Task.FromResult<object>(null);
        }
    }
}