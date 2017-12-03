using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace UBranchLocator
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //Convert to json output
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
              name: "getBestBranch",
              routeTemplate: "api/BranchLocator/getBestBranch/{latitude}/{longitude}",
              defaults: new { controller = "BranchLocator", action = "getBestBranch" }
            );
            config.Routes.MapHttpRoute(
              name: "searchBank",
              routeTemplate: "api/BranchLocator/searchBranch/{searchValue}/{latitude}/{longitude}",
              defaults: new { controller = "BranchLocator", action = "searchBranch" }
            );
        }
    }
}
