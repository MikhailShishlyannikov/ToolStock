using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sam.ToolStock.Web.Startup))]
namespace Sam.ToolStock.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
