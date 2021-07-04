using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebProject.StartupOwin))]

namespace WebProject
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
