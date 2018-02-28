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
using System.Web.WebPages;
using Microsoft.AspNet.Identity;

namespace MediaShop.WebApi.Filters
{
    public class MediaAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public bool AllowMultiple { get; private set; }

        public Permissions Permission { get; set; }

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var currentUserIdentity = HttpContext.Current.User?.Identity as ClaimsIdentity;
            int? currentUserPermissions = currentUserIdentity?.FindFirstValue("Permission")?.AsInt();
            if (ReferenceEquals(currentUserPermissions, null) || !((Permissions)currentUserPermissions).HasFlag(Permission))
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