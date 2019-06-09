using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Sam.ToolStock.Web.Handlers;

namespace Sam.ToolStock.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);// TODO похоже что здесь можно поставить глобальный фильтр на отлов ошибок
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure();
            GlobalFilters.Filters.Add(new ExecuteCustomErrorHandler());

            //TODO: сделать сдесь отлов ошибок, все что не попало в ExceptionFilter(он отрабатывает только в action)
        }
    }
}
