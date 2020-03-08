using Hangfire;
using Owin;
using Umbraco.Web;

namespace ThuanTanUmbraco
{
    public class Startup : UmbracoDefaultOwinStartup
    {
        public override void Configuration(IAppBuilder app)
        {
            base.Configuration(app);

            // Configure the database where Hangfire is going 
            // to create its tables
            var cs = Umbraco.Core.ApplicationContext.Current
                .DatabaseContext.ConnectionString;
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(cs);

            //// Configure Hangfire's dashboard with the default options
            //app.UseHangfireDashboard();

            //// Create a default Hangfire server
            //app.UseHangfireServer();
            var dashboardOptions = new DashboardOptions
            {
                Authorization = new[]
                {
                    new UmbracoAuthorizationFilter()
                }
            };

            app.UseHangfireDashboard("/hangfire", dashboardOptions);
            app.UseHangfireServer();
        }
    }
}