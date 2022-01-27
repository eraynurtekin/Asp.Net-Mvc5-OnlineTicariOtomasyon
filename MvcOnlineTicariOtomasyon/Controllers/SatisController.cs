using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        Context context = new Context();
        public ActionResult Index()
        {
            var degerler = context.SatisHareketis.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> degerler1 = (from x in context.Uruns.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.UrunAd,
                                                  Value = x.UrunId.ToString()
                                              }).ToList();

            List<SelectListItem> degerler2 = (from x in context.Carilers.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.CariAd + " " + x.CariSoyad,
                                                  Value = x.CariId.ToString()
                                              }).ToList();

            List<SelectListItem> degerler3 = (from x in context.Personels.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                  Value = x.PersonelId.ToString()
                                              }).ToList();

            ViewBag.dgr1 = degerler1;
            ViewBag.dgr2 = degerler2;
            ViewBag.dgr3 = degerler3;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket satisHareket)
        {   
            satisHareket.Tarih =DateTime.Parse(DateTime.Now.ToShortTimeString());
            context.SatisHareketis.Add(satisHareket);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> degerler1 = (from x in context.Uruns.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.UrunAd,
                                                  Value = x.UrunId.ToString()
                                              }).ToList();

            List<SelectListItem> degerler2 = (from x in context.Carilers.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.CariAd + " " + x.CariSoyad,
                                                  Value = x.CariId.ToString()
                                              }).ToList();

            List<SelectListItem> degerler3 = (from x in context.Personels.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                  Value = x.PersonelId.ToString()
                                              }).ToList();

            ViewBag.dgr1 = degerler1;
            ViewBag.dgr2 = degerler2;
            ViewBag.dgr3 = degerler3;

            var deger = context.SatisHareketis.Find(id);
            return View("SatisGetir", deger);
        }
        public ActionResult SatisGuncelle(SatisHareket satisHareket)
        {
            var deger = context.SatisHareketis.Find(satisHareket.SatisId);
            deger.Cariid = satisHareket.Cariid;
            deger.Personelid=satisHareket.Personelid;
            deger.Adet=satisHareket.Adet;
            deger.Fiyat = satisHareket.Fiyat;
            deger.Tarih=satisHareket.Tarih;
            deger.ToplamTutar = satisHareket.ToplamTutar;
            deger.Urunid = satisHareket.Urunid;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var degerler = context.SatisHareketis.Where(x => x.SatisId == id).ToList();
            return View(degerler);
        }
    }
}