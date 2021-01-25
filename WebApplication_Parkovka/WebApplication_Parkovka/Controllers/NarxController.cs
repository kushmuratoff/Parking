using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication_Parkovka.Models;

namespace WebApplication_Parkovka.Controllers
{
    public class NarxController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Narx/
        public ActionResult Index()
        {
            return View(db.Narx.ToList());
        }

        // GET: /Narx/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narx narx = db.Narx.Find(id);
            if (narx == null)
            {
                return HttpNotFound();
            }
            return View(narx);
        }

        // GET: /Narx/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Narx/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Summa")] Narx narx)
        {
            if (ModelState.IsValid)
            {
                db.Narx.Add(narx);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(narx);
        }

        // GET: /Narx/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narx narx = db.Narx.Find(id);
            if (narx == null)
            {
                return HttpNotFound();
            }
            return View(narx);
        }

        // POST: /Narx/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Summa")] Narx narx)
        {
            if (ModelState.IsValid)
            {
                db.Entry(narx).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(narx);
        }

        // GET: /Narx/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Narx narx = db.Narx.Find(id);
            if (narx == null)
            {
                return HttpNotFound();
            }
            return View(narx);
        }

        // POST: /Narx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Narx narx = db.Narx.Find(id);
            db.Narx.Remove(narx);
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
