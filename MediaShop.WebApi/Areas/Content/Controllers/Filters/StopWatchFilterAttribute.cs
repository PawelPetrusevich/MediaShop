using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
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
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _watch.Stop();
            Debug.WriteLine($"executed: {_watch.ElapsedMilliseconds} ms");
        }
    }
}