using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SklepGryPlanszowe.Startup))]
namespace SklepGryPlanszowe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
