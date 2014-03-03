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
  public class ResponsesController : Controller
  {
    private NashotelDBContext db = new NashotelDBContext();

    // GET: /Admin/Responses/
    public ActionResult Index(int? id)
    {
      return View(db.Response.OrderByDescending(p => p.ID).ToPagedList(id ?? 1, 10));
    }

    // GET: /Admin/Responses/Edit/5
    public async Task<ActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Response response = await db.Response.FindAsync(id);
      if (response == null)
      {
        return HttpNotFound();
      }
      return View(response);
    }

    // POST: /Admin/Responses/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Mail,Text,Text2,IsVisible")] Response response)
    {
      if (ModelState.IsValid)
      {
        var entry = db.Entry(response);
        entry.State = EntityState.Modified;
        entry.Property(e => e.Date).IsModified = false;
        entry.Property(e => e.IP).IsModified = false;
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }
      return View(response);
    }

    // GET: /Admin/Responses/Delete/5
    public async Task<ActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Response response = await db.Response.FindAsync(id);
      if (response == null)
      {
        return HttpNotFound();
      }
      return View(response);
    }

    // POST: /Admin/Responses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      Response response = await db.Response.FindAsync(id);
      db.Response.Remove(response);
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
