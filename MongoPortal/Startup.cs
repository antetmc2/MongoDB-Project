using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MongoTest.Startup))]
namespace MongoTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
