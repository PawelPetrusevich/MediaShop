using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace MediaShop.WebApi.Areas.Content.Controllers.Filters
{
    public class StopWatchFilterAttribute : ActionFilterAttribute
    {
        private Stopwatch _watch = new Stopwatch();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _watch.Reset();
            _watch.Start();
            Debug.WriteLine($"method name: {actionContext.ActionDescriptor.ActionName}, request: {actionContext.Request}");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _watch.Stop();
            Debug.WriteLine($"executed: {_watch.ElapsedMilliseconds} ms");
            Debug.WriteLine($"frguments count: {actionExecutedContext.ActionContext.ActionArguments}, response: {actionExecutedContext.Response}");
        }
    }
}