using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MakeIt.WebUI.Startup))]
namespace MakeIt.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureApp(app);
        }
    }
}