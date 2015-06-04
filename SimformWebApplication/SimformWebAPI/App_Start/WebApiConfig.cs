using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SimformWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "ApiAction",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = System.Web.Mvc.UrlParameter.Optional }
            );
        }
    }
}
