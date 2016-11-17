using AutoMapper;
using BlogSystem.Data.Models;
using BlogSystem.Web;
using BlogSystem.Web.ViewModels.Home;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace BlogSystem.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}