using Nashotelru.Models;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nashotelru.Areas.Admin.Controllers
{
  [Authorize(Roles = "admin")]
  public class NewsController : Controller
  {
    private NashotelDBContext db = new NashotelDBContext();

    // GET: /Admin/News/
    public ActionResult Index(int? id)
    {
      return View(db.News.OrderByDescending(p => p.ID).Select(p => new NewsViewModel { ID = p.ID, Title = p.Title, Date = p.Date, Text = p.Text, Description = p.Description, IsEnabled = p.IsEnabled }).ToPagedList(id ?? 1, 10));
    }

    // GET: /Admin/News/Create
    public ActionResult Create()
    {
      ViewBag.Title = "Создание новости";
      return View("Edit");
    }

    // POST: /Admin/News/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind(Include = "Title,Description,Date,Text,IsEnabled")] NewsViewModel news)
    {
      if (ModelState.IsValid)
      {
        var n = new News { ID = news.ID, Date = news.Date, Title = news.Title, Description = news.Description, Text = news.Text, IsEnabled = news.IsEnabled };

        db.News.Add(n);
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }

      return View("Edit", news);
    }

    // GET: /Admin/News/Edit/5
    public async Task<ActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      News news = await db.News.FindAsync(id);
      if (news == null)
      {
        return HttpNotFound();
      }
      ViewBag.Title = "Редактирование новости";
      var nvm = new NewsViewModel { ID = news.ID, Date = news.Date, Title = news.Title, Description = news.Description, Text = news.Text, IsEnabled = news.IsEnabled };
      return View(nvm);
    }

    // POST: /Admin/News/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "ID,Title,Description,Date,Text,IsEnabled")] NewsViewModel news)
    {
      if (ModelState.IsValid)
      {
        var n = new News { ID = news.ID, Date = news.Date, Title = news.Title, Description = news.Description, Text = news.Text, IsEnabled = news.IsEnabled };
        db.Entry(n).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }
      return View(news);
    }

    // GET: /Admin/News/Delete/5
    public async Task<ActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      News news = await db.News.FindAsync(id);
      if (news == null)
      {
        return HttpNotFound();
      }
      return View(news);
    }

    // POST: /Admin/News/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      News news = await db.News.FindAsync(id);
      db.News.Remove(news);
      await db.SaveChangesAsync();
      return RedirectToAction("Index");
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
