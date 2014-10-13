using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Medlars.Website.Startup))]

namespace Medlars.Website
{
    using System.Drawing;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var dependency = new DependencyInjectionStartup();
        }
    }
}
