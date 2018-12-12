using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ecommerce.Startup))]
namespace ecommerce
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
