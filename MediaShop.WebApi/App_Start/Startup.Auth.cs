using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MediaShop.Common.Interfaces.Services;
using MediaShop.WebApi.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(MediaShop.WebApi.Startup))]

namespace MediaShop.WebApi
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; } 

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            // Configure the db context and user manager to use a single instance per request

            PublicClientId = "MediaShop";

            var accountService = DependencyResolver.Current.GetService<IAccountService>();

            // Configure the application for OAuth based flow
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, accountService),

               // AuthorizeEndpointPath = new PathString("/api/account/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),

                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Token Generation
            app.UseOAuthBearerTokens(OAuthOptions);    
        }
    }
}
