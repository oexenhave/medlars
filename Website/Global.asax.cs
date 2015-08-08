namespace Medlars.Website
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using log4net;
    using TastyDomainDriven.MsSql;
    using System.Configuration;

    public class WebApiApplication : HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(WebApiApplication));

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            new SqlAppendOnlyStore(ConfigurationManager.ConnectionStrings["events"].ConnectionString).Initialize();

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Server.MapPath("~/log4net.config")));
            if (Logger.IsInfoEnabled)
            {
                Logger.Info("Application started");
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            var ex = HttpContext.Current.Server.GetLastError();
            if (Logger.IsErrorEnabled)
            {
                Logger.Error("Application exception", ex);
            }
        }
    }
}
