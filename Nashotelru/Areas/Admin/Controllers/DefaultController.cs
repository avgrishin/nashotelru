using Microsoft.AspNet.Identity.Owin;
using Nashotelru.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nashotelru.Areas.Admin.Controllers
{
  [Authorize(Roles = "admin")]
  public class DefaultController : Controller
  {
    public DefaultController()
    {
    }
    public DefaultController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
    {
      UserManager = userManager;
      RoleManager = roleManager;
    }
    private ApplicationUserManager _userManager;
    public ApplicationUserManager UserManager
    {
      get
      {
        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
      private set
      {
        _userManager = value;
      }
    }

    private ApplicationRoleManager _roleManager;
    public ApplicationRoleManager RoleManager
    {
      get
      {
        return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
      }
      private set
      {
        _roleManager = value;
      }
    }
    public ActionResult Index()
    {
      var q = new List<UserRolesViewModel>();

      foreach(var item in UserManager.Users.ToList())
      {
        List<string> Roles = new List<string>();
        foreach(var itm in item.Roles)
        {
          Roles.Add(RoleManager.Roles.FirstOrDefault(p=>p.Id == itm.RoleId).Name);
        }
        q.Add(new UserRolesViewModel { UserName = item.UserName, Roles = string.Join(",", Roles) });
      }
      //var Users = UserManager.Users.ToList().Select(u => new UserRolesViewModel { UserName = u.UserName, Roles = string.Join(",", await UserManager.GetRolesAsync(u.Id)) }).ToList();
      return View(q);
      //return View(UserManager.Users.Select(u => new { UserName = u.UserName, Email = u.Email, Roles = string.Join(",", UserManager.GetRolesAsync(u.Id)) }).ToList());
    }
  }
}