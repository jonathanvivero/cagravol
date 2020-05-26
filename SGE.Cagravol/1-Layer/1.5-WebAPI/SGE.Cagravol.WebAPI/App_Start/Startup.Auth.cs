using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SGE.Cagravol.WebAPI.App_Start;
using SGE.Cagravol.WebAPI.SecurityProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGE.Cagravol.WebAPI
{
    public partial class Startup
    {
        public void ConfigureOAuth(IAppBuilder app)
        {            
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

    }
}