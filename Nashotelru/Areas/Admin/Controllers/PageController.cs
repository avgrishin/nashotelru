using Nashotelru.Models;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Nashotelru.Areas.Admin.Controllers
{
  public class PageController : Controller
  {
    private NashotelDBContext db = new NashotelDBContext();

    // GET: /Admin/Page/
    public ActionResult Index(int? id)
    {
      ViewBag.p = id;
      return View(db.Page.OrderBy(p => p.Name).Select(p => new PageViewModel { ID = p.ID, Language = p.Language, Name = p.Name }).ToPagedList(id ?? 1, 10));
    }

    // GET: /Admin/Page/Create
    public ActionResult Create(int? p)
    {
      ViewBag.p = p;
      return View();
    }

    // POST: /Admin/Page/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Language,Name,Content")] Page page, int? p)
    {
      if (ModelState.IsValid)
      {
        db.Page.Add(page);
        db.SaveChanges();
        return RedirectToAction("Index", new { id = p });
      }

      return View(page);
    }

    // GET: /Admin/Page/Edit/5
    public ActionResult Edit(int? id, int? p)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Page page = db.Page.Find(id);
      if (page == null)
      {
        return HttpNotFound();
      }
      ViewBag.p = p;
      return View(page);
    }

    // POST: /Admin/Page/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "ID,Language,Name,Content")] Page page, int? p)
    {
      if (ModelState.IsValid)
      {
        db.Entry(page).State = EntityState.Modified;
        db.SaveChanges();
        //return Redirect(returnUrl);
        return RedirectToAction("Index", new { id = p });
      }
      return View(page);
    }

    // GET: /Admin/Page/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Page page = db.Page.Find(id);
      if (page == null)
      {
        return HttpNotFound();
      }
      return View(page);
    }

    // POST: /Admin/Page/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Page page = db.Page.Find(id);
      db.Page.Remove(page);
      db.SaveChanges();
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
