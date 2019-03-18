using bitirme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bitirme.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult sehir ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult sehir(IEnumerable<HttpPostedFileBase> SehirResim,bitirme.Models.sehir s)//Bu resimde otelid çekilmiyo onu düzelt
        {
            if (SehirResim != null)
            {
                OurDbContext db = new OurDbContext();
                foreach (var item in SehirResim)//kaç adet resim seçildiyse, o kadar kez çalışacak
                {
                    item.SaveAs(Server.MapPath("~/images/{item.FileName}"));//resim klasörüne resimleri kaydetme
                    s.sehirresim = item.FileName;
                    db.sehirs.Add(s);
                }
                db.SaveChanges();//veri tabanına kayıt işlemi

            }
            return RedirectToAction("sehir");
        }
    }
}   