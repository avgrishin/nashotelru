using Nashotelru.Models;
using System.Linq;
using System.Web.Mvc;

namespace Nashotelru.Areas.Admin.Controllers
{
  [Authorize(Roles = "admin")]
  public class DefaultController : Controller
  {
    public ActionResult Index()
    {
      var db = new ApplicationDbContext();
      var s1 = string.Join(",", db.Users.FirstOrDefault().Roles.Select(s => s.Role.Name));
      return View(db.Users.ToList());
    }
  }
}