using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KaraageCounter.Startup))]
namespace KaraageCounter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
