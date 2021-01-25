using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebApplication_Parkovka.Models;
using System.Web.Security;
namespace WebApplication_Parkovka.Controllers
{
    public class IshController : Controller
    {
        public BazaContext db = new BazaContext();
        //
        // GET: /Ish/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Qvga()
        {
            var t = db.Xodims.Where(x => x.Holat == 0).Count();
            ViewBag.soni = t;
            return View();
        }
        public ActionResult Narxlar()
        {
            Sahifa s = new Sahifa();
            s.Narx = db.Narx.ToList();
            return View(s);
        }
        public ActionResult N_qush()
        {
            return View();
        }
        [HttpPost]
        public ActionResult N_qush(string Sum)
        {
            Narx n = new Narx();
            n.Summa = Convert.ToInt32(Sum);
            db.Narx.Add(n);
            db.SaveChanges();
            return RedirectToAction("Narxlar", "Ish");
        }
        public ActionResult Joy()
        {
            Sahifa s = new Sahifa();
            s.Joylar = db.Joylar.Include(f=>f.Narx).ToList();
            return View(s);
        }
        public ActionResult Joy_qush()
        {

            Sahifa s = new Sahifa();
            s.Narx = db.Narx.ToList();
            return View(s);
        }
        [HttpPost]
        public ActionResult Joy_qush(int Nomeri,int?NarxId)
        {
            Joylar n = new Joylar();
            n.Nomeri = Nomeri;
            n.NarxId = NarxId;
            n.Holati = 0;
            db.Joylar.Add(n);
            db.SaveChanges();
            return RedirectToAction("Joy", "Ish");
        }
        public ActionResult Xodimlar()
        {
            Sahifa s = new Sahifa();
            s.Xodim = db.Xodims.Where(x=>x.Holat==0).ToList();
            return View(s);
        }
        public ActionResult JoyBerish(int? Id)
        {
            ViewBag.Id = Id;
            Sahifa s = new Sahifa();
            s.Narx = db.Narx.ToList();
            return View(s);
        }
        [HttpPost]
        public ActionResult JoyBel(int? XodimId,int?NarxId)
        {
            ViewBag.Id = XodimId;
            Sahifa s = new Sahifa();
            s.Joylar = db.Joylar.Where(j => j.NarxId == NarxId && j.Holati == 0).ToList();
            return View(s);
        }
        [HttpPost]
        public ActionResult JoySaqlash(int? XodimId, int? JoyId)
        {
            Xodim x = db.Xodims.Find(XodimId);
            x.JoylarId = JoyId;
            x.Holat = 1;
            db.SaveChanges();
            Joylar j = db.Joylar.Find(JoyId);
            j.Holati = 1;
            db.SaveChanges();
           
            return View();
        }
        public ActionResult JoyBer(int? Id)
        {
            ViewBag.Id = Id;
            Sahifa s = new Sahifa();
            s.Narx = db.Narx.ToList();
            return View(s);
        }
        public JsonResult getJoy(int id)
        {//.Where(y => y.NarxId == id && y.Holati==0)
            return Json(db.Joylar);
        }
        public ActionResult Mehmon()
        {
            return View();
        }
        public ActionResult FoyKel()
        {
            Sahifa s = new Sahifa();
            s.Joylar = db.Joylar.Include(f=>f.Narx).Where(f => f.Holati == 0).ToList();
            return View(s);
        }
        [HttpPost]
        public ActionResult FoyKel(string FISH,string MoshinaR,int? JoylarId)
        {
            JadvalFoy jf = new JadvalFoy();
            jf.FoyFISH = FISH;
            jf.MoshinaN = MoshinaR;
            jf.JoylarId = JoylarId;
            jf.KelishV = DateTime.Now;
            db.JadvalFoy.Add(jf);
            db.SaveChanges();
            Joylar j = db.Joylar.Find(JoylarId);
            j.Holati = 2;
            db.SaveChanges();
           
          //  s.Joylar = db.Joylar.Include(f => f.Narx).Where(f => f.Holati == 0).ToList();
            return RedirectToAction("Qvga", "Ish");
        }
        public ActionResult FoyKet()
        {
            Sahifa s = new Sahifa();
            s.JadvalFoy = db.JadvalFoy.Include(f => f.Joylar).Where(f => f.KetishV==null).ToList();
            return View(s);
        }
        [HttpPost]
        public ActionResult FoyKet(int? JoylarId)
        {
            JadvalFoy jf = db.JadvalFoy.Find(JoylarId);
            jf.KetishV = DateTime.Now;
            db.SaveChanges();
            var idd = db.JadvalFoy.Where(d => d.Id == JoylarId).FirstOrDefault().JoylarId;
            Joylar j = db.Joylar.Find(idd);
            j.Holati = 0;
            db.SaveChanges();

            //  s.Joylar = db.Joylar.Include(f => f.Narx).Where(f => f.Holati == 0).ToList();
            return RedirectToAction("Ketish", "Ish", new { Id=JoylarId});
        }
        public ActionResult Ketish(int Id)
        {
            Sahifa s = new Sahifa();
            JadvalFoy jj=new JadvalFoy();
            jj = db.JadvalFoy.Include(f => f.Joylar).Where(f => f.Id == Id).FirstOrDefault();
            s.JadvalFoy = db.JadvalFoy.Include(f => f.Joylar).Where(f => f.Id == Id).ToList();
            int t = jj.KetishV.Value.Day - jj.KelishV.Value.Day;
            int sum = db.Joylar.Include(f=>f.Narx).Where(f => f.Id == jj.JoylarId).FirstOrDefault().Narx.Summa;
            ViewBag.vaqt = (t+1)*sum;
            return View(s);
        }
        public ActionResult Ishchi()
        {
            return View();
        }
        public ActionResult XodKel()
        {
            Sahifa s = new Sahifa();
            s.Xodim = db.Xodims.Include(f => f.Joylar).Where(f => f.Holat == 1).ToList();
            return View(s);
        }
        [HttpPost]
        public ActionResult XodKel( int? XodimId)
        {
            JadvalXodim jx = new JadvalXodim();
            jx.XodimId = XodimId;
            jx.KelishV = DateTime.Now;
            db.JadvalXodim.Add(jx);
            db.SaveChanges();

            //  s.Joylar = db.Joylar.Include(f => f.Narx).Where(f => f.Holati == 0).ToList();
            return RedirectToAction("Qvga", "Ish");
        }
        public ActionResult XodKet()
        {
            Sahifa s = new Sahifa();
            s.JadvalXodim = db.JadvalXodim.Include(x=>x.Xodim).Where(f => f.KetishV == null).ToList();
            return View(s);
        }
        [HttpPost]
        public ActionResult XodKet(int? JoylarId)
        {
            JadvalXodim jf = db.JadvalXodim.Find(JoylarId);
            jf.KetishV = DateTime.Now;
            db.SaveChanges();
           

            //  s.Joylar = db.Joylar.Include(f => f.Narx).Where(f => f.Holati == 0).ToList();
            return RedirectToAction("XodimKetish", "Ish", new { Id = JoylarId });
        }
        public ActionResult XodimKetish(int Id)
        {
            Sahifa s = new Sahifa();

            s.JadvalXodim = db.JadvalXodim.Include(f => f.Xodim.Joylar).Include(f => f.Xodim).Where(f => f.Id == Id).ToList();
            
            return View(s);
        }
	}
}