using System.Web.Mvc;

namespace Nashotelru.Areas.Admin.Controllers
{
  [Authorize(Roles = "admin")]
  public class DefaultController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}