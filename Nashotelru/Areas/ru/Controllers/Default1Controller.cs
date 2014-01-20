using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nashotelru.Areas.ru.Models;

namespace Nashotelru.Areas.ru.Controllers
{
    public class Default1Controller : Controller
    {
        private NashotelDBContext db = new NashotelDBContext();

        // GET: /ru/Default1/
        public async Task<ActionResult> Index()
        {
            return View(await db.Response.ToListAsync());
        }

        // GET: /ru/Default1/Details/5
        public async Task<ActionResult> Details(int? id)
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

        // GET: /ru/Default1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ru/Default1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ID,Date,Name,Mail,Text,Text2,IP,IsVisible")] Response response)
        {
            if (ModelState.IsValid)
            {
                db.Response.Add(response);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(response);
        }

        // GET: /ru/Default1/Edit/5
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

        // POST: /ru/Default1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ID,Date,Name,Mail,Text,Text2,IP,IsVisible")] Response response)
        {
            if (ModelState.IsValid)
            {
                db.Entry(response).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(response);
        }

        // GET: /ru/Default1/Delete/5
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

        // POST: /ru/Default1/Delete/5
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
