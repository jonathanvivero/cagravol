using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SGE.Cagravol.Presentation.Web.Startup))]
namespace SGE.Cagravol.Presentation.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
