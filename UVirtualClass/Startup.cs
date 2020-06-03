using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UVirtualClass.Startup))]
namespace UVirtualClass
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
