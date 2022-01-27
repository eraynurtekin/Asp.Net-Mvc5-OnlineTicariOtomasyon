using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        // GET: UrunDetay
        Context context = new Context();
        public ActionResult Index()
        {   
            Class1 cs = new Class1();
            //var degerler = context.Uruns.Where(x=>x.UrunId == 1).ToList(); 
            
            cs.Deger1 = context.Uruns.Where(x=>x.UrunId == 1).ToList();
            cs.Deger2 = context.Detays.Where(y=>y.DetayID == 1).ToList();

            return View(cs);
        }
    }
}