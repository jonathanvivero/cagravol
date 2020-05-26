using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(SGE.Cagravol.WebAPI.Startup))]
namespace SGE.Cagravol.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            config.EnableCors();
            
            this.ConfigureOAuth(app);

            WebApiConfig.Register(config);

            app.UseWebApi(config);


        }

       
 
    }
}