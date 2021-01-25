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
    public class JoylarController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Joylar/
        public ActionResult Index()
        {
            var joylar = db.Joylar.Include(j => j.Narx);
            return View(joylar.ToList());
        }

        // GET: /Joylar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Joylar joylar = db.Joylar.Find(id);
            if (joylar == null)
            {
                return HttpNotFound();
            }
            return View(joylar);
        }

        // GET: /Joylar/Create
        public ActionResult Create()
        {
            ViewBag.NarxId = new SelectList(db.Narx, "Id", "Id");
            return View();
        }

        // POST: /Joylar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Nomeri,NarxId")] Joylar joylar)
        {
            if (ModelState.IsValid)
            {
                db.Joylar.Add(joylar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NarxId = new SelectList(db.Narx, "Id", "Id", joylar.NarxId);
            return View(joylar);
        }

        // GET: /Joylar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Joylar joylar = db.Joylar.Find(id);
            if (joylar == null)
            {
                return HttpNotFound();
            }
            ViewBag.NarxId = new SelectList(db.Narx, "Id", "Id", joylar.NarxId);
            return View(joylar);
        }

        // POST: /Joylar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nomeri,NarxId")] Joylar joylar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(joylar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NarxId = new SelectList(db.Narx, "Id", "Id", joylar.NarxId);
            return View(joylar);
        }

        // GET: /Joylar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Joylar joylar = db.Joylar.Find(id);
            if (joylar == null)
            {
                return HttpNotFound();
            }
            return View(joylar);
        }

        // POST: /Joylar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Joylar joylar = db.Joylar.Find(id);
            db.Joylar.Remove(joylar);
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
