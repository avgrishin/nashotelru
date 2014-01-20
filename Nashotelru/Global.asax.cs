using CaptchaMvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
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

      //var captchaManager = CaptchaUtils.CaptchaManager;
      //captchaManager.AddAreaRouteValue = false;
      //var defaultCaptchaManager = (DefaultCaptchaManager)captchaManager;
      //defaultCaptchaManager.ImageUrlFactory = (helper, pair) => ImageUrlFactory(defaultCaptchaManager, helper, pair);
      //defaultCaptchaManager.RefreshUrlFactory = RefreshUrlFactory;

    }
  }
}
