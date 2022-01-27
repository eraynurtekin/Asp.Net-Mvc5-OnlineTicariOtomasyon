using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        Context context = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = context.Mesajlars.Where(x=>x.Alıcı == mail).ToList();
            ViewBag.m = mail;

            var mailid = context.Carilers.Where(x=>x.CariMail == mail).Select(y=>y.CariId).FirstOrDefault();
            var toplamsatis = context.SatisHareketis.Where(x=>x.Cariid == mailid).Count();
            ViewBag.toplamSatis = toplamsatis;
            ViewBag.mid = mailid;

            var toplamTutar = context.SatisHareketis.Where(x => x.Cariid == mailid).Sum(y => y.ToplamTutar);
            ViewBag.toplamTutar = toplamTutar;

            var toplamUrun = context.SatisHareketis.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
            ViewBag.toplamUrun = toplamUrun;
            

            var adsoyad = context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad= adsoyad;



            return View(degerler);
        }
        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"]; //Sisteme giriş yapan mail adresini Sessiona atadık...
            var id = context.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariId).FirstOrDefault();
            var degerler = context.SatisHareketis.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }
        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = context.Mesajlars.Where(x=>x.Alıcı==mail).OrderByDescending(x=>x.MesajId).ToList();
            var gelenMesajSayisi = context.Mesajlars.Count(x=>x.Alıcı==mail).ToString();
            var gidenMesajSayisi = context.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.d1 = gelenMesajSayisi;
            ViewBag.d2 = gidenMesajSayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = context.Mesajlars.Where(x => x.Gönderici == mail).ToList();
            var gidenMesajSayisi = context.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            var gelenMesajSayisi = context.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            ViewBag.d1 = gelenMesajSayisi;
            ViewBag.d2 = gidenMesajSayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult MesajDetay(int id)
        {   
            var degerler = context.Mesajlars.Where(x=>x.MesajId == id).ToList();


            var mail = (string)Session["CariMail"];
            var gidenMesajSayisi = context.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            var gelenMesajSayisi = context.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            ViewBag.d1 = gelenMesajSayisi;
            ViewBag.d2 = gidenMesajSayisi;
            return View(degerler);
        }
        [Authorize]
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var gidenMesajSayisi = context.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            var gelenMesajSayisi = context.Mesajlars.Count(x => x.Alıcı == mail).ToString();
            ViewBag.d1 = gelenMesajSayisi;
            ViewBag.d2 = gidenMesajSayisi;

            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar mesaj)
        {
            var mail = (string)Session["CariMail"];
            mesaj.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            mesaj.Gönderici = mail;
            context.Mesajlars.Add(mesaj);
            context.SaveChanges();
            return View();
        }
        [Authorize]
        public ActionResult KargoTakip(string search)
        {
            var takip = from x in context.KargoDetays select x;
            
                takip = takip.Where(y => y.TakipKodu.Contains(search));
                
            return View(takip.ToList());
        }
        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = context.KargoTakips.Where(x=>x.TakipKodu == id).ToList();
            return View(degerler);
        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CariMail"];
            var id = context.Carilers.Where(x=>x.CariMail == mail).Select(y=>y.CariId).FirstOrDefault();
            var cariBul = context.Carilers.Find(id);
            return PartialView("Partial1",cariBul);
        }
        public PartialViewResult Partial2()
        {
            var veriler = context.Mesajlars.Where(x => x.Gönderici == "admin").ToList();
            return PartialView(veriler);
        }
        public ActionResult CariBilgiGüncelle(Cariler cari)
        {
            var guncellenecekCari = context.Carilers.Find(cari.CariId);
            guncellenecekCari.CariAd = cari.CariAd;
            guncellenecekCari.CariSoyad = cari.CariSoyad;
            guncellenecekCari.CariSehir = cari.CariSehir;
            guncellenecekCari.CariSifre = cari.CariSifre;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}