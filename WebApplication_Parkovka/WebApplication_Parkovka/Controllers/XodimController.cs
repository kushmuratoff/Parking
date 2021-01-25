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
    public class XodimController : Controller
    {
        private BazaContext db = new BazaContext();

        // GET: /Xodim/
        public ActionResult Index()
        {
            var xodims = db.Xodims.Include(x => x.Joylar).Include(x => x.Userlar);
            return View(xodims.ToList());
        }

        // GET: /Xodim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xodim xodim = db.Xodims.Find(id);
            if (xodim == null)
            {
                return HttpNotFound();
            }
            return View(xodim);
        }

        // GET: /Xodim/Create
        public ActionResult Create()
        {
            ViewBag.JoylarId = new SelectList(db.Joylar, "Id", "Id");
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Login");
            return View();
        }

        // POST: /Xodim/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,FISH,MoshinaR,UserlarId,JoylarId,Holat")] Xodim xodim)
        {
            if (ModelState.IsValid)
            {
                db.Xodims.Add(xodim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JoylarId = new SelectList(db.Joylar, "Id", "Id", xodim.JoylarId);
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Login", xodim.UserlarId);
            return View(xodim);
        }

        // GET: /Xodim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xodim xodim = db.Xodims.Find(id);
            if (xodim == null)
            {
                return HttpNotFound();
            }
            ViewBag.JoylarId = new SelectList(db.Joylar, "Id", "Id", xodim.JoylarId);
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Login", xodim.UserlarId);
            return View(xodim);
        }

        // POST: /Xodim/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,FISH,MoshinaR,UserlarId,JoylarId,Holat")] Xodim xodim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xodim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JoylarId = new SelectList(db.Joylar, "Id", "Id", xodim.JoylarId);
            ViewBag.UserlarId = new SelectList(db.Userlar, "Id", "Login", xodim.UserlarId);
            return View(xodim);
        }

        // GET: /Xodim/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xodim xodim = db.Xodims.Find(id);
            if (xodim == null)
            {
                return HttpNotFound();
            }
            return View(xodim);
        }

        // POST: /Xodim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Xodim xodim = db.Xodims.Find(id);
            db.Xodims.Remove(xodim);
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
