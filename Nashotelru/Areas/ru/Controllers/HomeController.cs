using Nashotelru.Areas.ru.Models;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace Nashotelru.Areas.ru.Controllers
{
  public class HomeController : Controller
  {
    private NashotelDBContext db = new NashotelDBContext();

    public ActionResult Index()
    {
      return View(db.News.Where(p => p.IsEnabled).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID).Take(5).ToList());
    }

    public ActionResult Transfer()
    {
      return View(db.News.Where(p => p.IsEnabled).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID).Take(5).ToList());
    }

    public ActionResult Contact()
    {
      return View();
    }
  }
}