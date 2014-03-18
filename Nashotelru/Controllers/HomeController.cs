using CaptchaMvc.Attributes;
using Nashotelru.Helpers;
using Nashotelru.Models;
using PagedList;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace Nashotelru.Controllers
{
  public class HomeController : Controller
  {
    private NashotelDBContext db = new NashotelDBContext();
    public ActionResult Index()
    {
      var vm = new
      {
        Page = db.Page.Where(p => p.Name == "HomeIndex" && p.Language == System.Threading.Thread.CurrentThread.CurrentUICulture.Name).FirstOrDefault(),
        News = db.News.Where(p => p.IsEnabled).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID).Select(p => new NewsViewModel { ID = p.ID, Title = p.Title, Date = p.Date, Text = p.Text, Description = p.Description, IsEnabled = p.IsEnabled }).Take(5).ToList(),
        book = new BookingViewModel { departureDate = DateTime.Today.AddDays(1), arrivalDate = DateTime.Today.AddDays(0), rooms = Nashotelru.Models.Rooms.one, adults = Adults.one, children = Children.one, promoText = "" }
      }.ToExpando();
      //return View(db.News.Where(p => p.IsEnabled).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID).Take(5).ToList());
      return View(vm);
    }

    public ActionResult About()
    {
      return View();
    }

    public ActionResult Transfer()
    {
      return View(db.News.Where(p => p.IsEnabled).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID).Select(p => new NewsViewModel { ID = p.ID, Title = p.Title, Date = p.Date, Text = p.Text, Description = p.Description, IsEnabled = p.IsEnabled }).Take(5).ToList());
    }

    public ActionResult Contact()
    {
      var vm = new
      {
        Page = db.Page.Where(p => p.Name == "HomeContact" && p.Language == System.Threading.Thread.CurrentThread.CurrentUICulture.Name).FirstOrDefault(),
        book = new BookingViewModel { departureDate = DateTime.Today.AddDays(1), arrivalDate = DateTime.Today.AddDays(0), rooms = Models.Rooms.one, adults = Adults.one, children = Children.one, promoText = "" }
      }.ToExpando();
      return View(vm);
    }

    public ActionResult Career()
    {
      return View(new
      {
        Page = db.Page.Where(p => p.Name == "HomeCareer" && p.Language == System.Threading.Thread.CurrentThread.CurrentUICulture.Name).FirstOrDefault()
      }.ToExpando());
    }
    public ActionResult SetLanguage(Culture lang, string returnUrl)
    {
      if (returnUrl.Length >= 3)
      {
        if (returnUrl.StartsWith("/" + Culture.ru.ToString(), StringComparison.CurrentCultureIgnoreCase) || returnUrl.StartsWith("/" + Culture.en.ToString(), StringComparison.CurrentCultureIgnoreCase) || returnUrl.StartsWith("/" + Culture.fr.ToString(), StringComparison.CurrentCultureIgnoreCase))
          returnUrl = returnUrl.Substring(3);
      }
      return Redirect((lang != Culture.ru ? "/" + lang.ToString() : "") + returnUrl);
    }

    public ActionResult Gallery(int? id)
    {
      var Name = string.Format("Gallery{0}", id);
      var vm = new
      {
        Page = db.Page.Where(p => p.Name == Name && p.Language == System.Threading.Thread.CurrentThread.CurrentUICulture.Name).FirstOrDefault()
      }.ToExpando();
      return View(vm);
    }

    public ActionResult Restaurant()
    {
      return View(db.Page.Where(p => p.Name == "Restaurant" && p.Language == System.Threading.Thread.CurrentThread.CurrentUICulture.Name).FirstOrDefault());
    }

    public ActionResult Rooms(int? id)
    {
      var Name = string.Format("Rooms{0}", id);
      var vm = new
      {
        Page = db.Page.Where(p => p.Name == Name && p.Language == System.Threading.Thread.CurrentThread.CurrentUICulture.Name).FirstOrDefault()
      }.ToExpando();
      return View(vm);
    }
    public ActionResult News(int? id)
    {
      var q = db.News.Where(p => p.IsEnabled).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID).Select(p => new NewsViewModel { ID = p.ID, Title = p.Title, Date = p.Date, Text = p.Text, Description = p.Description, IsEnabled = p.IsEnabled });
      return View(q.ToPagedList(id ?? 1, 3));
    }
    public ActionResult NewsDetail(int? id)
    {
      var news = db.News.Select(p => new NewsViewModel { ID = p.ID, Title = p.Title, Date = p.Date, Text = p.Text, Description = p.Description, IsEnabled = p.IsEnabled }).FirstOrDefault(p => p.IsEnabled && p.ID == id);
      if (news == null)
      {
        return HttpNotFound();
      }
      return View("NewsDetail", news);
    }
    public ActionResult Response(int? id)
    {
      var q = db.Response.Where(p => p.IsVisible).OrderByDescending(p => p.Date).ThenByDescending(p => p.ID);
      return View(q.ToPagedList(id ?? 1, 3));
    }
    public ActionResult ResponseCreated(int? id)
    {
      var q = db.Response.Where(p => p.ID == id).FirstOrDefault();
      return View("Created", q);
    }

    public ActionResult ResponseNew()
    {
      return View();
    }

    [CaptchaVerify("Captcha result is not valid.")]
    //[Recaptcha.RecaptchaControlMvc.CaptchaValidator]
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<ActionResult> ResponseNew([Bind(Include = "Name,Mail,Text")]Response response/*, bool captchaValid*/)
    {
      /*if (!captchaValid && false)
      {
        ModelState.AddModelError("", "Неверное значение");
      }
      else */
      if (ModelState.IsValid)
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
        return View("ResponseCreated", response);
      }
      return View(response);
    }

    public ActionResult Events()
    {
      return View();
    }
  }
}