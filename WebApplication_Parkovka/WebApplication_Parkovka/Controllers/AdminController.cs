using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebApplication_Parkovka.Models;
using System.Web.Security;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace WebApplication_Parkovka.Controllers
{
    public class AdminController : Controller
    {
        public BazaContext db = new BazaContext();

        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Adminga()
        {
            ViewBag.foy = db.JadvalFoy.Where(f => f.KelishV.Value.Year == DateTime.Now.Year && f.KelishV.Value.Month == DateTime.Now.Month && f.KelishV.Value.Day == DateTime.Now.Day).Count();
            ViewBag.xodim = db.Xodims.Count();
            ViewBag.p = db.Joylar.Where(j => j.Holati == 0).Count();
            return View();
        }
        public ActionResult oy()
        {
            ViewBag.joy = db.JadvalFoy.Where(f => f.KelishV.Value.Year == DateTime.Now.Year && f.KelishV.Value.Month == DateTime.Now.Month && f.KelishV.Value.Day == DateTime.Now.Day).Count();
            ViewBag.x = db.Xodims.Count();
            //ViewBag.p = int.ViewBag.joy - int.ViewBag.x;
            return View();

        }
        public ActionResult RemoveX(int?Id)
        {
            var jad = db.JadvalXodim.Where(x => x.XodimId == Id).ToList();
            foreach(var t in jad)
            {
                JadvalXodim jadxod = db.JadvalXodim.Find(t.Id);
                db.JadvalXodim.Remove(jadxod);
                db.SaveChanges();
            }
            Xodim xod = db.Xodims.Find(Id);
            db.Xodims.Remove(xod);
            db.SaveChanges();
            return RedirectToAction("Xodimlar", "Admin");
        }
        public ActionResult Xodimlar()
        {
            Sahifa s = new Sahifa();
            s.Xodim = db.Xodims.ToList();
            return View(s);
        }
        public ActionResult Register()
        {
            ViewBag.habar = "";
            return View();
        }
        [HttpPost]
        public ActionResult Register(string Login, string Parol)
        {

            Userlar user = null;
            user = db.Userlar.Where(u => u.Login == Login).FirstOrDefault();
            if (user != null)
            {
                ViewBag.habar = "Bunday loginli foydalanuvchi mavjud";
                return View();
            }
            else
            {
                return RedirectToAction("Saqlash", "Admin", new { Login = Login, Parol = Parol });
            }

        }
        public ActionResult Saqlash(string Login, string Parol)
        {           
            ViewBag.Login = Login;
            ViewBag.Parol = Parol;
            return View();
        }
        [HttpPost]
        public ActionResult Saqlash(string Login, string Parol, string FISH, string MoshinaR)
        {
            Userlar user = new Userlar();
            user.Login = Login;
            user.Parol = Parol;           
            user.RollarId = 3;
            db.Userlar.Add(user);
            db.SaveChanges();
            int Idd = db.Userlar.Where(u => u.Login == Login).FirstOrDefault().Id;
            Xodim xodim = new Xodim();
            xodim.FISH = FISH;
            xodim.MoshinaR = MoshinaR;
            xodim.Holat = 0;
            xodim.UserlarId = Idd;
            db.Xodims.Add(xodim);
            db.SaveChanges();
            //var Id = db.Bemor.Where(b => b.UserlarId == Idd).FirstOrDefault().Id;
            //FormsAuthentication.SetAuthCookie(Login, true);
            return RedirectToAction("Xodimlar", "Admin");
        }
        public ActionResult Xisobot()
        {
            return View();
        }
        public ActionResult Bugun()
        {
            Sahifa s = new Sahifa();
            s.JadvalFoy = db.JadvalFoy.Include(j => j.Joylar.Narx).Include(j => j.Joylar).Where(k => k.KetishV.Value.Day.ToString() == DateTime.Now.Day.ToString() && k.KetishV.Value.Month.ToString() == DateTime.Now.Month.ToString() && k.KetishV.Value.Year.ToString() == DateTime.Now.Year.ToString()).ToList();
            ViewBag.bug = DateTime.Today;
            var sa1 = db.JadvalFoy.Include(j => j.Joylar.Narx).Include(j => j.Joylar).Where(k => k.KetishV.Value.Day.ToString() == DateTime.Now.Day.ToString() && k.KetishV.Value.Month.ToString() == DateTime.Now.Month.ToString() && k.KetishV.Value.Year.ToString() == DateTime.Now.Year.ToString()).ToList();

            var ss = db.JadvalFoy.Include(j => j.Joylar.Narx).Include(j => j.Joylar).Where(k => k.KetishV.Value.Day.ToString() == DateTime.Now.Day.ToString() && k.KetishV.Value.Month.ToString() == DateTime.Now.Month.ToString() && k.KetishV.Value.Year.ToString() == DateTime.Now.Year.ToString()).GroupBy(g => g.Id).Select(g => new baza
            {
                name = g.Key,
                kuni = g.FirstOrDefault().KetishV.Value.Day - g.FirstOrDefault().KelishV.Value.Day + 1,
                kel=g.FirstOrDefault().KelishV.Value,ket=g.FirstOrDefault().KetishV.Value ,
            joyi=g.FirstOrDefault().Joylar.Nomeri,
            narx=g.FirstOrDefault().Joylar.Narx.Summa,
            summa=(g.FirstOrDefault().KetishV.Value.Day-g.FirstOrDefault().KelishV.Value.Day+1)*g.FirstOrDefault().Joylar.Narx.Summa,
            FISH=g.FirstOrDefault().FoyFISH,
            nomer=g.FirstOrDefault().MoshinaN});
            ViewBag.sa = ss;
            return View(s);
        }
        public ActionResult Holat()
        {
            Sahifa s= new Sahifa();
            var sa = db.JadvalFoy.Include(j => j.Joylar.Narx).Include(j => j.Joylar).Where(d => d.KetishV == null).GroupBy(g => g.Id).Select(g => new baza
            {
                name = g.Key,
                kuni = DateTime.Now.Day - g.FirstOrDefault().KelishV.Value.Day + 1,
                kel = g.FirstOrDefault().KelishV.Value,
               
                joyi = g.FirstOrDefault().Joylar.Nomeri,
                narx = g.FirstOrDefault().Joylar.Narx.Summa,
                summa = (DateTime.Now.Day - g.FirstOrDefault().KelishV.Value.Day + 1) * g.FirstOrDefault().Joylar.Narx.Summa,
                FISH = g.FirstOrDefault().FoyFISH,
                nomer = g.FirstOrDefault().MoshinaN
            });
            
            ViewBag.sa = sa;
            var hod = db.JadvalXodim.Include(j => j.Xodim.Joylar.Narx).Include(j => j.Xodim.Joylar).Include(j => j.Xodim).Where(d => d.KetishV == null).GroupBy(g => g.Id).Select(g => new baza
            {
                name = g.Key,
                kuni = DateTime.Now.Day - g.FirstOrDefault().KelishV.Value.Day + 1,
                kel = g.FirstOrDefault().KelishV.Value,

                joyi = g.FirstOrDefault().Xodim.Joylar.Nomeri,
                narx = g.FirstOrDefault().Xodim.Joylar.Narx.Summa,
                summa = (DateTime.Now.Day - g.FirstOrDefault().KelishV.Value.Day + 1) * g.FirstOrDefault().Xodim.Joylar.Narx.Summa,
                FISH = g.FirstOrDefault().Xodim.FISH,
                nomer = g.FirstOrDefault().Xodim.MoshinaR
            });
            ViewBag.hod = hod;
            return View(s);
        }
        public ActionResult Pdfga()
        {
            Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 15);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            Chunk chunk = new Chunk("                 Kunlik xisobot          ", FontFactory.GetFont("Arial", 30, Font.BOLD, BaseColor.BLACK));
            pdfDoc.Add(new Paragraph(chunk));
            var xis = db.JadvalFoy.Include(j => j.Joylar.Narx).Include(j => j.Joylar).Where(k => k.KetishV.Value.Day.ToString() == DateTime.Now.Day.ToString() && k.KetishV.Value.Month.ToString() == DateTime.Now.Month.ToString() && k.KetishV.Value.Year.ToString() == DateTime.Now.Year.ToString()).ToList();
            int summa = 0;
            foreach(var t in xis)
          {
              string s = "";
              s+="FISH:"+t.FoyFISH+"  ";
              s +="Nomeri:"+ t.MoshinaN + "  ";
              s +="Kelgan vaqti:"+ t.KelishV.Value.ToString()+ "  ";
              s += "Ketgan vaqti:" + t.KetishV.Value.ToString() + "  ";
              s += "Joy nomeri:" + t.Joylar.Nomeri + "  ";
              int tt = t.KetishV.Value.Day - t.KelishV.Value.Day;
              s += "Joy narxi:" + t.Joylar.Narx.Summa.ToString() + " ";
              s += "Turgan kuni:" + (tt + 1).ToString() + " ";
              s += "To'lov:" + t.Joylar.Narx.Summa * (tt + 1)+" so'm";
              chunk = new Chunk(s, FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK));
              pdfDoc.Add(new Paragraph(chunk));
              
              summa += db.Joylar.Include(f => f.Narx).Where(f => f.Id == t.JoylarId).FirstOrDefault().Narx.Summa * (tt + 1);
          }
            chunk = new Chunk("Kunlik yig'ilgan pul: "+summa.ToString()+" so'm", FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK));
            pdfDoc.Add(new Paragraph(chunk));
            chunk = new Chunk("Xisobot olingan vaqt: " + DateTime.Now.ToString(), FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK));
            pdfDoc.Add(new Paragraph(chunk));

            pdfWriter.CloseStream = false;
            pdfDoc.Close();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + DateTime.Now.ToString().ToString() + DateTime.Now.Minute.ToString() + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
            return View();
        }

	}
}