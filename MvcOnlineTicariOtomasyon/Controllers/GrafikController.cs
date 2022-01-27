using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
    
        // GET: Grafik
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            var grafikCiz = new Chart(600, 600);
            grafikCiz.AddTitle("Kategori - Ürün Stok Sayısı").AddLegend("Stok").AddSeries("Değerler",
                xValue: new[] { "Mobilya", "Ofis Eşyaları", "Bilgisayar" }, yValues: new[] { 85, 90, 70 }).Write();

            return File(grafikCiz.ToWebImage().GetBytes(), "image/jpeg");

        }
        Context context = new Context();
        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList(); 
            ArrayList yvalue = new ArrayList();
            var sonuclar = context.Uruns.ToList();
            sonuclar.ToList().ForEach(x => xvalue.Add(x.UrunAd));
            sonuclar.ToList().ForEach(y=>yvalue.Add(y.Stok));
            var grafik = new Chart(width: 800, height: 800)
                .AddTitle("Stoklar")
                .AddSeries(chartType: "Pie", name: "Stok", xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");

        }

        public ActionResult Index4()
        {
            return View();
        }
        
        public ActionResult VisualizeUrunResult()
        {
            return Json(UrunListesi(), JsonRequestBehavior.AllowGet);
        }
        public List<Sinif1> UrunListesi()
        {
            List<Sinif1> snf = new List<Sinif1>();
            snf.Add(new Sinif1()
            {
                UrunAd = "Bilgisayar",
                Stok = 120
            }) ;
            snf.Add(new Sinif1()
            {
                UrunAd = "Beyaz Eşya",
                Stok = 150
            });
            snf.Add(new Sinif1()
            {
                UrunAd = "Mobilya",
                Stok = 100
            });
            snf.Add(new Sinif1()
            {
                UrunAd = "Küçük Ev Aletleri",
                Stok = 300
            });
            snf.Add(new Sinif1()
            {
                UrunAd = "Mobil Cihazlar",
                Stok = 90
            });
            return snf;
        }

        public ActionResult Index5()
        {
            return View();
        }

        public ActionResult VisualizeUrunResult2()
        {
            return Json(UrunListesi2(),JsonRequestBehavior.AllowGet);
        }

        public List<Sinif2> UrunListesi2()
        {
            List<Sinif2> sinif2 = new List<Sinif2>();

            var sorgu = from x in context.Uruns
                        group x by x.UrunAd into g
                        select new Sinif2
                        {
                            urn = g.Key,
                            stk = (short)g.Count()
                        };
            return (sorgu.ToList()); 

        }

        public ActionResult Index6()
        {
            return View();
        }
        public ActionResult Index7()
        {
            return View();
        }
    }
}