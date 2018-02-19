using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace MediaShop.WebApi.Filters
{
    public class ProductExeptionFilterAttribute : Attribute, IExceptionFilter
    {
        public bool AllowMultiple { get; }

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (actionExecutedContext.Exception != null)
            {
                switch (actionExecutedContext.Exception)
                {
                    case InvalidOperationException error:
                        actionExecutedContext.Response =
                            actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, error.Message);
                        break;
                    case ArgumentNullException error:
                        actionExecutedContext.Response =
                            actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, error.Message);
                        break;
                    case ArgumentException error:
                        actionExecutedContext.Response =
                            actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, error.Message);
                        break;
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}