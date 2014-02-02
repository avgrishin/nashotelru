using CaptchaMvc.Attributes;
using Nashotelru.Areas.ru.Models;
using PagedList;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Nashotelru.Areas.ru.Controllers
{
  public class ResponseController : Controller
  {
    private NashotelDBContext db = new NashotelDBContext();
    public ActionResult Index(int? id)
    {
      var q = db.Response.Where(p => p.IsVisible).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID);
      return View(q.ToPagedList(id ?? 1, 3));
    }
    public ActionResult Created(int? id)
    {
      var q = db.Response.Where(p => p.ID == id).FirstOrDefault();
      return View("Created", q);
    }

    public ActionResult Create()
    {
      return View();
    }

    [CaptchaVerify("Captcha result is not valid.")]
    //[Recaptcha.RecaptchaControlMvc.CaptchaValidator]
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<ActionResult> Create([Bind(Include = "Name,Mail,Text")]Response response/*, bool captchaValid*/)
    {
      /*if (!captchaValid && false)
      {
        ModelState.AddModelError("", "Неверное значение");
      }
      else */if (ModelState.IsValid)
      {
        response.Date = DateTime.Now;
        response.IsVisible = false;
        response.IP = Request.ServerVariables["REMOTE_ADDR"];
        StringBuilder sb = new StringBuilder(HttpUtility.HtmlEncode(response.Text));
        sb.Replace("&lt;b&gt;", "<b>");
        sb.Replace("&lt;/b&gt;", "</b>");
        sb.Replace("&lt;i&gt;", "<i>");
        sb.Replace("&lt;/i&gt;", "</i>");
        sb.Replace("&lt;br&gt;", "<br>");
        sb.Replace("\r\n", "<br>");
        response.Text = sb.ToString();
        db.Response.Add(response);
        await db.SaveChangesAsync();
        return View("Created", response);
      }
      return View(response);
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