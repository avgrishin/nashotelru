using System.Web.Mvc;

namespace Nashotelru.Areas.ru
{
    public class ruAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ru";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ru_default",
                "ru/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Nashotelru.Areas.ru.Controllers" }
            );
        }
    }
}