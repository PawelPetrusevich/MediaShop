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
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public bool AllowMultiple { get; private set; }

        public Permissions Permission { get; set; }

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var principal = actionContext.RequestContext.Principal;
            var currentUserClaims = (principal?.Identity as ClaimsIdentity).Claims?.SingleOrDefault(x => x.Type == "Permission")?.Value;
            int currentUserPermissions = 0;
            bool parseFlag = false;
            if (!ReferenceEquals(currentUserClaims, null))
            {
                parseFlag = int.TryParse(currentUserClaims, out currentUserPermissions);
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