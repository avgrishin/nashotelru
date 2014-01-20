using Nashotelru.Areas.ru.Models;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace Nashotelru.Areas.ru.Controllers
{
  public class NewsController : Controller
  {
    private NashotelDBContext db = new NashotelDBContext();
    public ActionResult Index(int? id)
    {
      var q = db.News.Where(p => p.IsEnabled).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID);
      return View(q.ToPagedList(id ?? 1, 3));
    }

    public ActionResult Det(int? id)
    {
      return View(db.News.FirstOrDefault(p => p.IsEnabled && p.ID == id));
    }
  }
}