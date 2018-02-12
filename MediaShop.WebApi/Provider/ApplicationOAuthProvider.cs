using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediaShop.Common.Interfaces.Services;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using MediaShop.WebApi.Properties;
using Microsoft.AspNet.Identity;

namespace MediaShop.WebApi.Provider
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private readonly IAccountService _accountService;

        public ApplicationOAuthProvider(string publicClientId, IAccountService accountService)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
            _accountService = accountService;
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = await _accountService.FindUserAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity authIdentity = new ClaimsIdentity(DefaultAuthenticationTypes.ExternalBearer, ClaimTypes.Name, ClaimTypes.Role);
            authIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), "http://www.w3.org/2001/XMLSchema#string"));
            authIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Login, "http://www.w3.org/2001/XMLSchema#string"));
            authIdentity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"));
            authIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email, "http://www.w3.org/2001/XMLSchema#string"));

            context.Validated(authIdentity);

            context.Request.Context.Authentication.SignIn(authIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}