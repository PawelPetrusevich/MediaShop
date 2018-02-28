using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediaShop.Common;
using MediaShop.Common.Dto.User;
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

        public static AuthenticationProperties CreateProperties(string userName, string userId)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName },
                { "userId", userId }
            };
            return new AuthenticationProperties(data);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = await _accountService.FindUserAsync(context.UserName, context.Password.GetHash());

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var userNumberPermission = (int)user.Permissions;
            var account = _accountService.Login(new LoginDto { Login = user.Login, Password = user.Password });          
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(Resources.ClaimTypeId, user.Id.ToString()),
                new Claim(Resources.ClaimTypePermission, userNumberPermission.ToString())
            };

            var oauthIdentity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user.Login, user.Id.ToString());
            AuthenticationTicket ticket = new AuthenticationTicket(oauthIdentity, properties);
            context.Validated(ticket);
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