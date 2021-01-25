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
    public class JadvalXodimController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /JadvalXodim/
        public ActionResult Index()
        {
            var jadvalxodim = db.JadvalXodim.Include(j => j.Xodim);
            return View(jadvalxodim.ToList());
        }

        // GET: /JadvalXodim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JadvalXodim jadvalxodim = db.JadvalXodim.Find(id);
            if (jadvalxodim == null)
            {
                return HttpNotFound();
            }
            return View(jadvalxodim);
        }

        // GET: /JadvalXodim/Create
        public ActionResult Create()
        {
            ViewBag.XodimId = new SelectList(db.Xodims, "Id", "FISH");
            return View();
        }

        // POST: /JadvalXodim/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,XodimId,KelishV,KetishV")] JadvalXodim jadvalxodim)
        {
            if (ModelState.IsValid)
            {
                db.JadvalXodim.Add(jadvalxodim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.XodimId = new SelectList(db.Xodims, "Id", "FISH", jadvalxodim.XodimId);
            return View(jadvalxodim);
        }

        // GET: /JadvalXodim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JadvalXodim jadvalxodim = db.JadvalXodim.Find(id);
            if (jadvalxodim == null)
            {
                return HttpNotFound();
            }
            ViewBag.XodimId = new SelectList(db.Xodims, "Id", "FISH", jadvalxodim.XodimId);
            return View(jadvalxodim);
        }

        // POST: /JadvalXodim/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,XodimId,KelishV,KetishV")] JadvalXodim jadvalxodim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jadvalxodim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.XodimId = new SelectList(db.Xodims, "Id", "FISH", jadvalxodim.XodimId);
            return View(jadvalxodim);
        }

        // GET: /JadvalXodim/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JadvalXodim jadvalxodim = db.JadvalXodim.Find(id);
            if (jadvalxodim == null)
            {
                return HttpNotFound();
            }
            return View(jadvalxodim);
        }

        // POST: /JadvalXodim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JadvalXodim jadvalxodim = db.JadvalXodim.Find(id);
            db.JadvalXodim.Remove(jadvalxodim);
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
