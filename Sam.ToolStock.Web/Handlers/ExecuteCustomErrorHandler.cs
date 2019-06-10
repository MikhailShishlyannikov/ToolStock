using System;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace Sam.ToolStock.Web.Handlers
{
    public class ExecuteCustomErrorHandler : ActionFilterAttribute, IExceptionFilter
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MvcApplication));

        public void OnException(ExceptionContext filterContext)
        {
            Log.Error("Unhandled exception logged in Application." + Environment.NewLine +
                      //"User : " + AuthenticationHelper.GetCurrentAuthenticatedUserName() + Environment.NewLine +
                      "Page : " + HttpContext.Current.Request.Url.AbsoluteUri, filterContext.Exception);

        }
    }
}