using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    [AllowAnonymous] //Authorize bu sınıfta hariç hale getirdi...
    public class LoginController : Controller
    {
        Context context = new Context();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Partial1(Cariler cari)
        {
            context.Carilers.Add(cari);
            context.SaveChanges();
            return PartialView();
        }
        [HttpGet]
        public ActionResult CariLogin1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CariLogin1(Cariler parametre)
        {
            var bilgiler = context.Carilers.FirstOrDefault(x => x.CariMail == parametre.CariMail && x.CariSifre == parametre.CariSifre);

            if(bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.CariMail, false);
                Session["CariMail"] = bilgiler.CariMail.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            else 
            {
                return RedirectToAction("Index", "Login");
            }
            
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        public ActionResult AdminLogin(Admin admin)
        {
            var bilgiler = context.Admins.FirstOrDefault(x=>x.KullaniciAd == admin.KullaniciAd && x.Sifre==admin.Sifre );
            if(bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAd, false);
                Session["KullaniciAd"] = bilgiler.KullaniciAd.ToString(); //Kullanici ad sonradan yetkilendirme için kullanılacak.
                return RedirectToAction("Index", "Kategori");
            }
            return RedirectToAction("Index","Login");
        }
    }
}