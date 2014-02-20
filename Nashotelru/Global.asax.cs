using CaptchaMvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Nashotelru
{
  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

    //protected void Application_EndRequest()
    //{
    //  if (Context.Response.StatusCode == 404)
    //  {
    //    Response.Clear();
    //    var rd = new RouteData();
    //    //rd.DataTokens["area"] = "AreaName"; // In case controller is in another area
    //    rd.Values.Add("controller", "Errors");
    //    rd.Values.Add("action", "NotFound");

    //    IController c = new Nashotelru.Controllers.ErrorsController();
    //    c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
    //  }
    //}

    //protected void Application_AcquireRequestState(Object sender, EventArgs e)
    //{
    //  if (HttpContext.Current.Session != null)
    //  {
    //    var ci = (CultureInfo)this.Session["Culture"];

    //    if (ci == null)
    //    {
    //      ci = Thread.CurrentThread.CurrentCulture;
    //      this.Session["Culture"] = ci;
    //    }

    //    Thread.CurrentThread.CurrentUICulture = ci;
    //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
    //  }
    //}

  }
}
