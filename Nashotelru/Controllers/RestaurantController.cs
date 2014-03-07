using Nashotelru.Models;
using System.Linq;
using System.Web.Mvc;

namespace Nashotelru.Controllers
{
  public class RestaurantController : Controller
  {
    private NashotelDBContext db = new NashotelDBContext();
    public ActionResult Index()
    {
      return View(db.Page.Where(p => p.Name == "Restaurant" && p.Language == System.Threading.Thread.CurrentThread.CurrentUICulture.Name).FirstOrDefault());
    }
  }
}