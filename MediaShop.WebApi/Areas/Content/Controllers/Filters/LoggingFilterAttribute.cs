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
using NLog;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace MediaShop.WebApi.Areas.Content.Controllers.Filters
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
#if DEBUG
        private Stopwatch _watch = new Stopwatch();
        private Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _watch.Reset();
            _watch.Start();

            logger.Trace($"method name: {actionContext.ActionDescriptor.ActionName}, OS: {Environment.OSVersion}");
            logger.Trace($"request: {actionContext.Request.RequestUri}, {actionContext.Request.Headers}");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _watch.Stop();

            logger.Trace($"executed: {_watch.ElapsedMilliseconds} ms, response: { actionExecutedContext.Response.StatusCode}");
        }
#endif
    }
}