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
    public class HomeController : Controller
    {
        public BazaContext db = new BazaContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Krish()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Krish(string Login, string Parol)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                Userlar user = null;
                using (BazaContext db = new BazaContext())
                {
                    user = db.Userlar.Where(u => u.Login == Login &&
                    u.Parol == Parol).FirstOrDefault();
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(Login, true);
                    var rolee = user.RollarId;
                    BazaContext db = new BazaContext();
                    var nomi = db.Rollar.Where(r => r.Id == rolee).First().Nomi;
                    var useridd = user.Id;

                    switch (nomi)
                    {
                        case "Admin":
                            {
                                //int adminid = db.Ad.Where(b => b.UserlarId == useridd).FirstOrDefault().Id;

                                return RedirectToAction("Adminga", "Admin");
                            } break;
                        case "Qoravul":
                            {
                                //int bemorid = db.Bemor.Where(b => b.UserlarId == useridd).FirstOrDefault().Id;

                                return RedirectToAction("Qvga", "Ish");
                            } break;
                        //case "Admin":
                        //    {
                        //        var d = db.Stomatologiya.Where(s => s.UserlarId == user.Id).FirstOrDefault().Id;

                        //        return RedirectToAction("Index", "Admin", new { Id = d });
                        //    } break;

                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Bunday foydalanuvchi mavjud emas");
                }
            }
            return RedirectToAction("Index", "Home");


        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Open()
        {
            string login = User.Identity.Name;
            var nom = db.Userlar.Include(u=>u.Rollar).Where(u => u.Login == login).FirstOrDefault().Rollar.Nomi;
            switch (nom)
            {
                case "Admin": { return RedirectToAction("Adminga", "Admin"); } break;
                case "Qoravul": { return RedirectToAction("Qvga", "Ish"); } break;


            }
            return View();
        }
    }
}