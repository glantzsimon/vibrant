﻿using System.Web.Mvc;
using System.Web.Routing;

namespace K9.WebApplication
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
		    routes.MapMvcAttributeRoutes();
            routes.LowercaseUrls = true;

			routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}