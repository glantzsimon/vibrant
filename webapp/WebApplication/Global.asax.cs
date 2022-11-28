using System.Configuration;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using K9.SharedLibrary.Helpers;
using K9.WebApplication.Config;

namespace K9.WebApplication
{
    public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			
			Startup.RegisterTypes();
			Startup.RegisterStaticTypes();

		    var seedConfig = ConfigHelper.GetConfiguration<SeedConfiguration>(ConfigurationManager.AppSettings).Value;
			DataConfig.InitialiseDatabase(seedConfig);
			AuthConfig.InitialiseWebSecurity();
			DataConfig.InitialiseUsersAndRoles(seedConfig);

		    AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;

		    Stripe.StripeConfiguration.SetApiKey(ConfigurationManager.AppSettings["SecretKey"]);
        }
	}
}