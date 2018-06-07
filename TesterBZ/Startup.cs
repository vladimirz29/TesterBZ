using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TesterBZ.Startup))]
namespace TesterBZ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
