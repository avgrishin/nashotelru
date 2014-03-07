using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nashotelru
{
  public class RouteConfig
  {
    public class MultiCultureMvcRouteHandler : MvcRouteHandler
    {
      protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
      {
        var culture = requestContext.RouteData.Values["culture"].ToString();
        var ci = new CultureInfo(culture);
        System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
        return base.GetHttpHandler(requestContext);
      }
    }
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      
      //routes.MapRoute(
      //  name: "New",
      //  url: "{lang}/{controller}/{action}/{id}",
      //  defaults: new { lang = "ru", controller = "Home", action = "Index", id = UrlParameter.Optional },
      //  constraints: new { lang = "ru|en||fr" },
      //  namespaces: new[] { "Nashotelru.Controllers" });

      //routes.MapRoute(
      //  name: "Account",
      //  url: "Account/{action}/{id}",
      //  defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional },
      //  namespaces: new[] { "Nashotelru.Controllers" }
      //);

      //routes.MapRoute(
      //  name: "Default",
      //  url: "{controller}/{action}/{id}",
      //  defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
      //  namespaces: new[] { "Nashotelru.Controllers" });

      routes.MapRoute(
        name: "Default",
        url: "{culture}/{controller}/{action}/{id}",
        defaults: new { culture = Culture.ru.ToString(), controller = "Home", action = "Index", id = UrlParameter.Optional },
        constraints: new { culture = Culture.en.ToString() + "|" + Culture.fr.ToString() },
        namespaces: new[] { "Nashotelru.Controllers" }
      ).RouteHandler = new MultiCultureMvcRouteHandler();

      routes.MapRoute(
        name: "Default1",
        url: "{controller}/{action}/{id}",
        defaults: new { culture = Culture.ru.ToString(), controller = "Home", action = "Index", id = UrlParameter.Optional },
        namespaces: new[] { "Nashotelru.Controllers" }
      ).RouteHandler = new MultiCultureMvcRouteHandler();

      //foreach (Route r in routes)
      //{
      //  r.RouteHandler = new MultiCultureMvcRouteHandler();
      //  r.Url = "{culture}/" + r.Url;
      //  //Adding default culture 
      //  if (r.Defaults == null)
      //  {
      //    r.Defaults = new RouteValueDictionary();
      //  }
      //  r.Defaults.Add("culture", Culture.ru.ToString());

      //  //Adding constraint for culture param
      //  if (r.Constraints == null)
      //  {
      //    r.Constraints = new RouteValueDictionary();
      //  }
      //  r.Constraints.Add("culture", new CultureConstraint(Culture.en.ToString(), Culture.ru.ToString(), Culture.fr.ToString()));
      //}
    }
  }

  public class CultureConstraint : IRouteConstraint
  {
    private string[] _values;
    public CultureConstraint(params string[] values)
    {
      this._values = values;
    }

    public bool Match(HttpContextBase httpContext, Route route, string parameterName,
                        RouteValueDictionary values, RouteDirection routeDirection)
    {
      // Get the value called "parameterName" from the 
      // RouteValueDictionary called "value"
      string value = values[parameterName].ToString();
      // Return true is the list of allowed values contains 
      // this value.
      return _values.Contains(value);
    }
  }
  public enum Culture
  {
    ru = 1,
    en = 2,
    fr = 3
  }
}
