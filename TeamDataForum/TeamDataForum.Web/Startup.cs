using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TeamDataForum.Web.Startup))]
namespace TeamDataForum.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}