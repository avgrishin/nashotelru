using Nashotelru.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nashotelru.Controllers
{
  public class HomeController : Controller
  {
    private NashotelDBContext db = new NashotelDBContext();
    public ActionResult Index()
    {
      var vm = new HomeIndexViewModel
      {
        Page = db.Page.Where(p => p.Name == "HomeIndex" && p.Language == System.Threading.Thread.CurrentThread.CurrentUICulture.Name).FirstOrDefault(),
        News = db.News.Where(p => p.IsEnabled).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID).Take(5).ToList(),
        book = new BookingViewModel { departureDate = DateTime.Today.AddDays(1), arrivalDate = DateTime.Today.AddDays(0), rooms = Rooms.one, adults=Adults.one, children=Children.one, promoText=""}
      };
      //return View(db.News.Where(p => p.IsEnabled).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID).Take(5).ToList());
      return View(vm);
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Transfer()
    {
      return View(db.News.Where(p => p.IsEnabled).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID).Take(5).ToList());
    }

    public ActionResult Contact()
    {
      var vm = new HomeIndexViewModel
      {
        Page = db.Page.Where(p => p.Name == "HomeContact" && p.Language == System.Threading.Thread.CurrentThread.CurrentUICulture.Name).FirstOrDefault(),
        News = db.News.Where(p => p.IsEnabled).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID).Take(5).ToList()
      };
      return View(vm);
    }


    public ActionResult SetLanguage(Culture lang, string returnUrl)
    {
      if (returnUrl.Length >= 3)
      {
        if (returnUrl.StartsWith("/" + Culture.ru.ToString(), StringComparison.CurrentCultureIgnoreCase) || returnUrl.StartsWith("/" + Culture.en.ToString(), StringComparison.CurrentCultureIgnoreCase) || returnUrl.StartsWith("/" + Culture.fr.ToString(), StringComparison.CurrentCultureIgnoreCase))
          returnUrl = returnUrl.Substring(3);
      }
      //return RedirectToAction(actionName: routeData.Values["action"].ToString(), controllerName: routeData.Values["controller"].ToString(), routeValues: routeValues);
      return Redirect((lang != Culture.ru ? "/" +  lang.ToString() : "") + returnUrl);
    }
  }
}