using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankProject.Startup))]
namespace BankProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
