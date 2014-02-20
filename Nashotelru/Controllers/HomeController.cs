using System.Globalization;
using System.Web.Mvc;

namespace Nashotelru.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return RedirectToAction("Index", "Home", new { area = "ru"/*, lang = "ru"*/ });
      //return View();
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }

    public ActionResult SetLanguage(string id)
    {
      Session["Culture"] = new CultureInfo(id);
      if (Request != null && Request.UrlReferrer != null)
      {
        return Redirect(Request.UrlReferrer.ToString());
      }
      return RedirectToAction("Index");
    }
  }
}