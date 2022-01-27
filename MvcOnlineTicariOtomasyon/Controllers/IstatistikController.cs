using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        Context context = new Context();
        public ActionResult Index()
        {       //1. KISIM
            var deger1 = context.Carilers.Count().ToString();
            ViewBag.dgr1 = deger1;

            var deger2 = context.Uruns.Count().ToString();
            ViewBag.dgr2 = deger2;

            var deger3 = context.Personels.Count().ToString();
            ViewBag.dgr3 = deger3;

            var deger4 = context.Kategoris.Count().ToString();
            ViewBag.dgr4 = deger4;
            
            //2.KISIM

            var deger5 = context.Uruns.Sum(x=>x.Stok).ToString();
            ViewBag.dgr5 = deger5;

            var deger6 = (from x in context.Uruns select x.Marka).Distinct().Count().ToString();
            ViewBag.dgr6 = deger6;

            var deger7 = context.Uruns.Count(x => x.Stok <= 20).ToString();
            ViewBag.dgr7 = deger7;

            var deger8 = (from x in context.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();    
            ViewBag.dgr8 = deger8;

            //3.KISIM

            var deger9 = (from x in context.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();  
            ViewBag.dgr9 = deger9;

            var deger12 = context.Uruns.GroupBy(x=>x.Marka).OrderByDescending(z=>z.Count()).Select(y=>y.Key).FirstOrDefault();
            ViewBag.dgr12 = deger12;

            var deger10 = context.Uruns.Count(x => x.UrunAd=="Buzdolabı").ToString();
            ViewBag.dgr10 = deger10;

            var deger11 = context.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.dgr11 = deger11;

            //4.KISIM

            var deger13 = context.Uruns.Where(u=>u.UrunId == (context.SatisHareketis.GroupBy(x => x.Urunid).
            OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k=>k.UrunAd).FirstOrDefault();
            ViewBag.dgr13 = deger13;

            var deger14 = context.SatisHareketis.Sum(x=>x.ToplamTutar).ToString();
            ViewBag.dgr14 = deger14;

            DateTime bugün = DateTime.Today;
            var deger15 = context.SatisHareketis.Count(x=>x.Tarih== bugün).ToString();
            ViewBag.dgr15 = deger15;

            var deger16 = context.SatisHareketis.Where(x => x.Tarih == bugün).Sum(y =>(decimal?)y.ToplamTutar).ToString();  
            ViewBag.dgr16 = deger16;


            return View();
        }
        public ActionResult KolayTablolar()
        {
            var sorgu = from x in context.Carilers
                        group x by x.CariSehir into g
                        select new SinifGrup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };
            return View(sorgu.ToList());
        }
        public PartialViewResult Partial1()
        {
            var sorgu2 = from x in context.Personels
                         group x by x.Departman.DepartmanAd into g
                         select new SinifGrup2
                         {
                             Departman = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu2.ToList());
        }
        public PartialViewResult Partial2()
        {
            var sorgu3 = context.Carilers.ToList();
            return PartialView(sorgu3);
        }
        public PartialViewResult Partial3()
        {
            var sorgu4 = context.Uruns.ToList();
            return PartialView(sorgu4);
        }
        public PartialViewResult Partial4()
        {
            var sorgu5 = from x in context.Uruns
                         group x by x.Marka into g
                         select new SinifGrup3
                         {
                             Marka = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu5.ToList());
        }
       
    }
}