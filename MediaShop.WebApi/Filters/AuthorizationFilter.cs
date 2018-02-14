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
            var currentUserClaims = principal?.Identity as ClaimsIdentity;
            int currentUserPermissions;
            var parseFlag = int.TryParse(currentUserClaims?.Claims.SingleOrDefault(x => x.Type == "Permission").Value, out currentUserPermissions);                        
            if (!ReferenceEquals(principal, null) && !parseFlag && !Permission.HasFlag((Permissions)currentUserPermissions))
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