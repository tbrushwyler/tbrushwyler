using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cumulo.ninja.landing.Startup))]
namespace cumulo.ninja.landing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
