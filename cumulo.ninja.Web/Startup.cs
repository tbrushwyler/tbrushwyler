using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cumulo.ninja.Web.Startup))]
namespace cumulo.ninja.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
