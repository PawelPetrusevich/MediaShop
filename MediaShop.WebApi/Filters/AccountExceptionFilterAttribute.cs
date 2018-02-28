using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.User;

namespace MediaShop.WebApi.Filters
{
    public class AccountExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public bool AllowMultiple { get; }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (actionExecutedContext.Exception != null)
            {
                actionExecutedContext.Response = actionExecutedContext.Request
                 .CreateErrorResponse(HttpStatusCode.InternalServerError, actionExecutedContext.Exception.Message);
            }

            return Task.FromResult<object>(null);
        }
    }
}