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
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context context=new Context();
        public ActionResult Index(int sayfa=1)
        {   

            var kategoriler = context.Kategoris.ToList().ToPagedList(sayfa,4);

            return View(kategoriler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori kategori)
        {
            context.Kategoris.Add(kategori); //kategori View tarafından gelen parametreleri tutar.
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var silinecekKategori = context.Kategoris.Find(id); //silinecekKategori bizim id ile gönderdiğimiz satırı tutar...
            context.Kategoris.Remove(silinecekKategori); //Tuttuğumuz kategoriyi siler.
            context.SaveChanges(); //Yapılan değişikliği kaydediyoruz.
            return RedirectToAction("Index");

            //Tekrardan bir sayfa oluşturmaya gerek yok gönderdiğimiz ID'ye göre silme işlemi yapıyor...
        }
        public ActionResult KategoriGetir(int id)
        {
            var kategori = context.Kategoris.Find(id);
            return View("KategoriGetir",kategori);
        }
        public ActionResult KategoriGuncelle(Kategori kategori)
        {
            var guncellencekKategori = context.Kategoris.Find(kategori.KategoriID); //Oluşturduğumuz değişkene sayfadaki Id'yi hafızaya aldık
            guncellencekKategori.KategoriAd = kategori.KategoriAd; //Sağdaki yeni değeri sola atadık
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Deneme()
        {
            Class3 class3 = new Class3();
            class3.Kategoriler = new SelectList(context.Kategoris, "KategoriID", "KategoriAd");
            class3.Urunler = new SelectList(context.Uruns, "UrunId","UrunAd");
            return View(class3);
        }
        public JsonResult UrunGetir(int secilenUrun)
        {
            var urunListesi = (from x in context.Uruns
                               join y in context.Kategoris
                               on x.Kategori.KategoriID equals y.KategoriID
                               where x.Kategori.KategoriID==secilenUrun
                               select new
                               {
                                   Text=x.UrunAd,
                                   Value=x.UrunId.ToString(),
                               }).ToList();
            return Json(urunListesi,JsonRequestBehavior.AllowGet);
        }
    }
}