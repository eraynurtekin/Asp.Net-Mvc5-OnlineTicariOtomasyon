using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
        Context context = new Context();
        public ActionResult Index(string search)
        {
            var kargolar = from x in context.KargoDetays select x;
            if (!string.IsNullOrEmpty(search))
            {
                kargolar = kargolar.Where(y => y.TakipKodu.Contains(search));
            }
            return View(kargolar.ToList());
        }

        [HttpGet]
        public ActionResult YeniKargo()
        {
            Random random = new Random();
            string[] karakterler = { "A", "B", "C", "D" };
            int k1, k2, k3;
            k1=random.Next(0,4);
            k2=random.Next(0,4);
            k3=random.Next(0,4);

            int s1,s2, s3; 
            s1=random.Next(100,1000);
            s2=random.Next(10,99);
            s3=random.Next(10,99);

            string kod = s1.ToString() + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];

            ViewBag.takipkod = kod;
            return View();
        }
        [HttpPost]
        public ActionResult YeniKargo(KargoDetay kargo)
        {
            context.KargoDetays.Add(kargo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KargoTakip(string id)
        {
            var degerler = context.KargoTakips.Where(x=>x.TakipKodu == id).ToList();

            return View(degerler);
        }


    }
}