using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context context= new Context();
        public ActionResult Index()
        {   
            var degerler = context.Personels.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {   
            List<SelectListItem> deger1=(from x in context.Departmans.ToList() 
                                         select new SelectListItem
                                         {
                                             Text=x.DepartmanAd,
                                             Value=x.DepartmanId.ToString()
                                         }).ToList();
                                        
            ViewBag.dgr1=deger1;
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel personel)
        {
            //if ((Request.Files.Count > 0))
            //{
            //    string dosyaadi = Path.GetFileName(Request.Files[0].FileName); //GetFileName ile dosyanın adını aldık
            //    string uzanti = Path.GetExtension(Request.Files[0].FileName); //GetExtension ile uzantıyı aldık.
            //    string yol = "~/Image/" + dosyaadi + uzanti;
            //    //Aldığımız resmi içine atmamız gerekiyor.
            //    //Server içindeki MapPath e kaydet
            //    Request.Files[0].SaveAs(Server.MapPath(yol));
            //    //Veritabanına kaydetmek için:
            //    personel.PersonelGorsel = "~/Image" + dosyaadi + uzanti;

            //}
            if (Request.Files.Count > 0)

            {

                var extention = Path.GetExtension(Request.Files[0].FileName);

                var randomName = string.Format($"{DateTime.Now.Ticks}{extention}");

                //var randomName = string.Format($"{Guid.NewGuid().ToString().Replace("-", "")}{extention}");

                personel.PersonelGorsel = "/Images/" + randomName;

                var path = "~/Images/" + randomName;

                Request.Files[0].SaveAs(Server.MapPath(path));

            }
            context.Personels.Add(personel);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in context.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.DepartmanId.ToString()
                                           }).ToList();
            ViewBag.dgr1= deger1;

            var guncellenecekPersonel = context.Personels.Find(id);
            return View("PersonelGetir",guncellenecekPersonel);
        }
        public ActionResult PersonelGuncelle(Personel personel)
        {
            if (Request.Files.Count > 0)

            {

                var extention = Path.GetExtension(Request.Files[0].FileName);

                var randomName = string.Format($"{DateTime.Now.Ticks}{extention}");

                //var randomName = string.Format($"{Guid.NewGuid().ToString().Replace("-", "")}{extention}");

                personel.PersonelGorsel = "/Images/" + randomName;

                var path = "~/Images/" + randomName;

                Request.Files[0].SaveAs(Server.MapPath(path));

            }

            var gncpersonel = context.Personels.Find(personel.PersonelId);
            gncpersonel.PersonelAd = personel.PersonelAd;
            gncpersonel.PersonelSoyad=personel.PersonelSoyad;
            gncpersonel.PersonelGorsel=personel.PersonelGorsel;
            gncpersonel.Departmanid=personel.Departmanid;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
         
        public ActionResult PersonelListe()
        {   
            var degerler = context.Personels.ToList();
            return View(degerler);
        }
    }
}