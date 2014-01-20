using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nashotelru.Startup))]
namespace Nashotelru
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
