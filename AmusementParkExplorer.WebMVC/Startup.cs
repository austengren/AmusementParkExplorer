using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AmusementParkExplorer.WebMVC.Startup))]
namespace AmusementParkExplorer.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
