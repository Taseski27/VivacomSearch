using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(VivacomSearch.Startup))]
namespace VivacomSearch
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}