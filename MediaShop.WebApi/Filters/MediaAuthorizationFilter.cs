using MediaShop.Common.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MediaShop.WebApi.Filters
{
    public class MediaAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public bool AllowMultiple { get; private set; }

        public Permissions Permission { get; set; }

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var currentUserClaims = (HttpContext.Current.User?.Identity as ClaimsIdentity)?.Claims;
            var principal = actionContext.RequestContext.Principal;
            int currentUserPermissions = 0;
            bool parseFlag = false;
            if (!ReferenceEquals(currentUserClaims, null))
            {
                parseFlag = int.TryParse(currentUserClaims?.SingleOrDefault(x => x.Type == "Permission")?.Value, out currentUserPermissions);
            }

            if (!parseFlag || !Permission.HasFlag((Permissions)currentUserPermissions))
            {
                return Task.FromResult<HttpResponseMessage>(actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
            else
            {
                return continuation();
            }
        }
    }
}