using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using MediaShop.Common.Interfaces.Services;
using MediaShop.WebApi.Provider;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
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
            app.UseCors(CorsOptions.AllowAll);

            var accountService = DependencyResolver.Current.GetService<IAccountService>();

            // Configure the application for OAuth based flow
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, accountService),

                AuthorizeEndpointPath = new PathString("/api/account/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),

                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Token Generation
            app.UseOAuthBearerTokens(OAuthOptions);
           
            var config = new HttpConfiguration();

            app.UseWebApi(config);
            app.Map(
                "/signalr",
                map =>
                {
                    map.UseCors(CorsOptions.AllowAll);
                    map.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
                    {
                        Provider = new QueryStringOAuthBearerProvider()
                    });
                    var resolver = GlobalHost.DependencyResolver;
                    resolver.Register(typeof(IUserIdProvider), () => new SignalRUserIdProvider());
                    var hubConfiguration = new HubConfiguration()
                    {
                        Resolver = resolver
                    };
                    map.RunSignalR(hubConfiguration);
                });
        }
    }
}