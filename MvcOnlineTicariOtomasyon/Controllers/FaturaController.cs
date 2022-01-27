using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        Context context = new Context();
        public ActionResult Index()
        {   
            var liste = context.Faturalars.ToList();

            return View(liste);
        }

        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar faturalar)
        {   context.Faturalars.Add(faturalar);
            context.SaveChanges();  
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult FaturaGetir(int id)
        {
            var fatura = context.Faturalars.Find(id);
            return View("FaturaGetir",fatura);
        }
        [HttpPost]
        public ActionResult FaturaGuncelle(Faturalar faturalar)
        {
            var gncFatura = context.Faturalars.Find(faturalar.FaturaId); // Guncellenceği bulduk.
            gncFatura.FaturaSeriNo = faturalar.FaturaSeriNo;
            gncFatura.FaturaSıraNo = faturalar.FaturaSıraNo;
            gncFatura.VergiDairesi= faturalar.VergiDairesi;
            gncFatura.Tarih = faturalar.Tarih;
            gncFatura.Saat=faturalar.Saat;
            gncFatura.TeslimEden = faturalar.TeslimEden;
            gncFatura.TeslimAlan = faturalar.TeslimAlan;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaDetay(int id)
        {
            var degerler = context.FaturaKalems.Where(x =>x.Faturaid == id).ToList();
            var teslimAlan = context.Faturalars.Where(x=>x.FaturaId == id).Select(y=>y.TeslimAlan).FirstOrDefault();
            ViewBag.dgr = teslimAlan;
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem faturalar)
        {
            context.FaturaKalems.Add(faturalar);
            context.SaveChanges();
            return RedirectToAction("Index");


        }
        public ActionResult Dinamik()
        {
            Class4 class4 = new Class4();
            class4.deger1 = context.Faturalars.ToList();
            class4.deger2 = context.FaturaKalems.ToList();
            return View(class4);

        }
        public ActionResult FaturaKaydet(string FaturaSeriNo,string FaturaSıraNo,DateTime Tarih,string VergiDairesi,string Saat,
            string TeslimEden,string TeslimAlan,string Toplam,FaturaKalem[] kalemler)
        {
            Faturalar faturalar = new Faturalar();
            faturalar.FaturaSeriNo = FaturaSeriNo;
            faturalar.FaturaSıraNo = FaturaSıraNo;
            faturalar.Tarih = Tarih;
            faturalar.VergiDairesi = VergiDairesi;
            faturalar.Saat = Saat;
            faturalar.TeslimEden = TeslimEden;
            faturalar.TeslimAlan = TeslimAlan;
            faturalar.Toplam =decimal.Parse(Toplam);
            context.Faturalars.Add(faturalar);

            foreach(var item in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = item.Aciklama;
                fk.BirimFiyat= item.BirimFiyat;
                fk.Miktar=item.Miktar;
                fk.Faturaid = item.FaturaKalemId;
                fk.Tutar = item.Tutar;
                context.FaturaKalems.Add(fk);
            }

            context.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
        
    }
}