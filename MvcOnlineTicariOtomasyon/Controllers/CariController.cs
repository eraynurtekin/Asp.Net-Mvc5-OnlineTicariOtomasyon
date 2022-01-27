using System;
using System.Collections.Generic;
using System.Linq;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        Context context = new Context();
        public ActionResult Index()
        {
            var degerler = context.Carilers.Where(x => x.Durum == true).ToList();

            return View(degerler);
        }

        public ActionResult CariEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CariEkle(Cariler cariler) //cariler view tarafından gelen argümanları tutar.
        {   
            cariler.Durum = true;
            context.Carilers.Add(cariler); 
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariSil(int id)
        {
            var cari = context.Carilers.Find(id);
            cari.Durum = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariGetir(int id)
        {
            var cari = context.Carilers.Find(id);
            return View("CariGetir", cari);
        }
        public ActionResult CariGuncelle(Cariler cariler)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }
            var cari = context.Carilers.Find(cariler.CariId);
            cari.CariAd = cariler.CariAd;
            cari.CariSoyad=cari.CariSoyad;
            cari.CariSehir = cariler.CariSehir;
            cari.CariMail = cariler.CariMail;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSatis(int id)
        {
            var degerler = context.SatisHareketis.Where(x => x.Cariid == id).ToList();
            var cr =context.Carilers.Where(x=>x.CariId == id).Select(y=>y.CariAd +""+y.CariSoyad).FirstOrDefault();
            //cr ile Viewbag e göndemek için yazdık.
            //FirstOrDefault ile tek değer çekeceğimizi bildirdik.
            ViewBag.cari = cr; //ViewBag e gönderdik...
           
            return View(degerler);
        }
    }
}