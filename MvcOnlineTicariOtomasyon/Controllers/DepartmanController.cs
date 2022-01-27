using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
  
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context context = new Context();
        public ActionResult Index()
        {   
            var degerler = context.Departmans.Where(x=>x.Durum==true).ToList();
            return View(degerler);
        }
        
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult DepartmanEkle(Departman departman)
        {
            context.Departmans.Add(departman); //departman View tarafından gelen parametreleri tutar.
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanSil(int id)
        {
            var departman = context.Departmans.Find(id);
            departman.Durum = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanGetir(int id)
        {
            var dpt = context.Departmans.Find(id);
            return View("DepartmanGetir", dpt);
        }
        public ActionResult DepartmanGüncelle(Departman departman)
        {
            var gncldpt = context.Departmans.Find(departman.DepartmanId);
            gncldpt.DepartmanAd = departman.DepartmanAd;
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DepartmanDetay(int id)
        {   
            var degerler = context.Personels.Where(x=>x.Departmanid == id).ToList();
            var departman = context.Departmans.Where(x=>x.DepartmanId == id).Select(y=>y.DepartmanAd).FirstOrDefault();
            //Tek değeri çekmek için list yerine FirstOrDefault kullandık.

            //View a taşımak için ViewBag kullanıyoruz.
            //ViewBag Controller tarafından View'a veri taşır.
            //d ile sanal bir değer oluşturduk
            ViewBag.d = departman;
            return View(degerler);

            
        }
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = context.SatisHareketis.Where(x=>x.Personelid ==id).ToList();
            var per = context.Personels.Where(x=>x.PersonelId ==id).Select(y=>y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.dper = per;  
                return View(degerler);
        }

    }
}