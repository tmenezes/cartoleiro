using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cartoleiro.Web.Startup))]
namespace Cartoleiro.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
