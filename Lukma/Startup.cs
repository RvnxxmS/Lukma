using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lukma.Startup))]
namespace Lukma
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
