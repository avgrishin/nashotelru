using Nashotelru.Models;
using System.Linq;
using System.Web.Mvc;

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
        News = db.News.Where(p => p.IsEnabled).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID).Take(5).ToList()
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
      return View();
    }

    public ActionResult SetLanguage(Culture lang, string returnUrl)
    {
      if (returnUrl.Length >= 3)
      {
        returnUrl = returnUrl.Substring(3);
      }
      return Redirect("/" + lang.ToString() + returnUrl);
    }
  }
}