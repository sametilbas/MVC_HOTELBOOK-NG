using bitirme.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bitirme.Controllers
{
    public class HomeController : Controller
    {
        OurDbContext db = new OurDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var x = db.sehirs.Where(a => a.sehirID > 0).ToList();
            return View(x);
        }
        public ActionResult oteller(string sad)
        {
            var oteller = db.otels.Where(x=>x.sehir==sad).ToList();
            return View(oteller);
        }
        public ActionResult odalar(int oad)
        {
            var oteldetay = db.otelodas.Where(x => x.otelID == oad).ToList();
            return View(oteldetay);
        }
        public ActionResult res()
        {
            return View();
        }
        [HttpPost]
        public ActionResult res(bitirme.Models.sehir s)//Burası şehir ekleme
        {
            string fileName = Path.GetFileNameWithoutExtension(s.res.FileName);
            string extension = Path.GetExtension(s.res.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            s.sehirresim = "~/Resimler/images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Resimler/images/"), fileName);
            s.res.SaveAs(fileName);
            using (OurDbContext db = new OurDbContext())
            {
                db.sehirs.Add(s);
                db.SaveChanges();
            }
            return View();
        }
        public ActionResult oteldetay(int? otelid)
        {
            oteldetayview ov = new oteldetayview();
            ov.otel = db.otels.Where(x => x.otelID == otelid).ToList();
            ov.otelresim = db.otelresims.Where(x => x.otelID == otelid).ToList();
            ov.oteloda = db.otelodas.Where(x => x.otelID == otelid).ToList();
            return View(ov);
        }
        public ActionResult odadetay()
        {
            return View();
        }
        public ActionResult rezerve()
        {
            return View();
        }
    }
}