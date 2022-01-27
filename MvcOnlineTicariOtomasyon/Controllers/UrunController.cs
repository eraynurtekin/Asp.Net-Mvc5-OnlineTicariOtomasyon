using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun

        Context context = new Context();
        public ActionResult Index(string p,int sayfa=1)
        {

            var urunler = context.Uruns.Where(x => x.UrunAd.Contains(p) || p == null).ToList
                ().ToPagedList(sayfa, 8);

            return View(urunler);
            //var urunler = from x in context.Uruns select x;
            //if (!string.IsNullOrEmpty(p))
            //{
            //    urunler = urunler.Where(y => y.UrunAd.Contains(p));
            //}
            //return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {    // Yeni ürüne ait kategorileri listelemek için linq sorgusu yazıyoruz...
            List<SelectListItem> deger1 = (from x in context.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urun urun)
        {
            context.Uruns.Add(urun);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var silinecekUrun = context.Uruns.Find(id);
            silinecekUrun.Durum = false;
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in context.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            

            var guncellenecekUrun = context.Uruns.Find(id);
            return View("UrunGetir",guncellenecekUrun);
        }
        public ActionResult UrunGuncelle(Urun urun)
        {
            var guncellenecekUrun = context.Uruns.Find(urun.UrunId);
            guncellenecekUrun.AlisFiyat = urun.AlisFiyat;
            guncellenecekUrun.Durum = urun.Durum;
            guncellenecekUrun.Kategoriid = urun.Kategoriid;
            guncellenecekUrun.Marka=urun.Marka;
            guncellenecekUrun.Stok= urun.Stok;
            guncellenecekUrun.UrunAd = urun.UrunAd;
            guncellenecekUrun.UrunGorsel=urun.UrunGorsel;
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult UrunListesi()
        {
            var degerler = context.Uruns.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger1 = (from x in context.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var deger2 = context.Uruns.Find(id);
            ViewBag.dgr2 = deger2.UrunId;     
            ViewBag.dgr3 = deger2.SatisFiyat;
            return View();
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket satis)
        {   
            satis.Tarih=DateTime.Parse(DateTime.Now.ToShortDateString());
            context.SatisHareketis.Add(satis);
            context.SaveChanges();
            return RedirectToAction("Index","Satis");
        }

    }
}