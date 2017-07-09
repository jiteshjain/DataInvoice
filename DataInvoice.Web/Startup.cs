using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DataInvoice.Web.Startup))]
namespace DataInvoice.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
