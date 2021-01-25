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
    public class JadvalFoyController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /JadvalFoy/
        public ActionResult Index()
        {
            var jadvalfoy = db.JadvalFoy.Include(j => j.Joylar);
            return View(jadvalfoy.ToList());
        }

        // GET: /JadvalFoy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JadvalFoy jadvalfoy = db.JadvalFoy.Find(id);
            if (jadvalfoy == null)
            {
                return HttpNotFound();
            }
            return View(jadvalfoy);
        }

        // GET: /JadvalFoy/Create
        public ActionResult Create()
        {
            ViewBag.JoylarId = new SelectList(db.Joylar, "Id", "Id");
            return View();
        }

        // POST: /JadvalFoy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,FoyFISH,MoshinaN,KelishV,KetishV,JoylarId")] JadvalFoy jadvalfoy)
        {
            if (ModelState.IsValid)
            {
                db.JadvalFoy.Add(jadvalfoy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JoylarId = new SelectList(db.Joylar, "Id", "Id", jadvalfoy.JoylarId);
            return View(jadvalfoy);
        }

        // GET: /JadvalFoy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JadvalFoy jadvalfoy = db.JadvalFoy.Find(id);
            if (jadvalfoy == null)
            {
                return HttpNotFound();
            }
            ViewBag.JoylarId = new SelectList(db.Joylar, "Id", "Id", jadvalfoy.JoylarId);
            return View(jadvalfoy);
        }

        // POST: /JadvalFoy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,FoyFISH,MoshinaN,KelishV,KetishV,JoylarId")] JadvalFoy jadvalfoy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jadvalfoy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JoylarId = new SelectList(db.Joylar, "Id", "Id", jadvalfoy.JoylarId);
            return View(jadvalfoy);
        }

        // GET: /JadvalFoy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JadvalFoy jadvalfoy = db.JadvalFoy.Find(id);
            if (jadvalfoy == null)
            {
                return HttpNotFound();
            }
            return View(jadvalfoy);
        }

        // POST: /JadvalFoy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JadvalFoy jadvalfoy = db.JadvalFoy.Find(id);
            db.JadvalFoy.Remove(jadvalfoy);
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
