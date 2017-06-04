using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(BlogSystem.Web.Startup))]
namespace BlogSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}