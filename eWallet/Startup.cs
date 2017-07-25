using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eWallet.Startup))]
namespace eWallet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
