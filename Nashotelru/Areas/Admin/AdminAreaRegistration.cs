using System.Web.Mvc;

namespace Nashotelru.Areas.Admin
{
  public class AdminAreaRegistration : AreaRegistration
  {
    public override string AreaName
    {
      get
      {
        return "Admin";
      }
    }
    public override void RegisterArea(AreaRegistrationContext context)
    {
      context.MapRoute(
          name: "Admin_default",
          url: "{culture}/Admin/{controller}/{action}/{id}",
          defaults: new { culture = Culture.ru.ToString(), controller = "Default", action = "Index", id = UrlParameter.Optional },
          constraints: new { culture = Culture.en.ToString() + "|" + Culture.fr.ToString() },
          namespaces: new[] { "Nashotelru.Areas.Admin.Controllers" }
      ).RouteHandler = new Nashotelru.RouteConfig.MultiCultureMvcRouteHandler();
      context.MapRoute(
            "Admin_default1",
            "Admin/{controller}/{action}/{id}",
            new { culture = Culture.ru.ToString(), controller = "Default", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "Nashotelru.Areas.Admin.Controllers" }
        ).RouteHandler = new Nashotelru.RouteConfig.MultiCultureMvcRouteHandler();
    }
  }
}