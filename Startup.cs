using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdmisionPS2019.Startup))]
namespace AdmisionPS2019
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
