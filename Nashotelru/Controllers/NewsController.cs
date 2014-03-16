using Nashotelru.Models;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace Nashotelru.Controllers
{
  public class NewsController : Controller
  {
    private NashotelDBContext db = new NashotelDBContext();

    public ActionResult Det(int? id)
    {
      var news = db.News.FirstOrDefault(p => p.IsEnabled && p.ID == id);
      if (news == null)
      {
        return HttpNotFound();
      }
      return View(news);
    }
  }
}