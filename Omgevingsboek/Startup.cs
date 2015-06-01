using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Omgevingsboek.Startup))]
namespace Omgevingsboek
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
