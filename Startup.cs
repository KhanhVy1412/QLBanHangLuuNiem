using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLBanHangLuuNiem.Startup))]
namespace QLBanHangLuuNiem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
