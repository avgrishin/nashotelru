using Nashotelru.Areas.ru.Models;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace Nashotelru.Controllers
{
  [Authorize]
  public class AdminController : Controller
  {
    private NashotelDBContext db = new NashotelDBContext();
    public ActionResult Index()
    {
      return View();
    }
    public ActionResult Responses(int? id)
    {
      var q = db.Response.Where(p => p.IsVisible).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID);
      return View(q.ToPagedList(id ?? 1, 3));
    }
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}