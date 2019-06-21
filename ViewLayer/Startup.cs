using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ViewLayer.Startup))]
namespace ViewLayer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
