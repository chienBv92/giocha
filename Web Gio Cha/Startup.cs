using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web_Gio_Cha.Startup))]
namespace Web_Gio_Cha
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
