using FlickrNet;
using Newtonsoft.Json;
using Omgevingsboek.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Omgevingsboek
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static Flickr flickr { get; set; }
        protected void Application_Start()
        {
            flickr = FlickrApiManager.GetInstance();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            };

            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
