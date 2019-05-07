using System.Diagnostics.CodeAnalysis;
using System.Web;

namespace Sam.ToolStock.Web.Handlers
{
    public class RedirectHandler : IHttpHandler
    {
        private readonly string _newUrl;

        [SuppressMessage(category: "Microsoft.Design", checkId: "CA1054:UriParametersShouldNotBeStrings",
            Justification = "We just use string since HttpResponse.Redirect only accept as string parameter.")]
        public RedirectHandler(string newUrl)
        {
            _newUrl = newUrl;
        }

        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Redirect(_newUrl);
        }
    }
}