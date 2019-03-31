using bitirme.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace bitirme.Controllers
{
    public class otelController : Controller
    {
        OurDbContext db = new OurDbContext();
        // GET: otel
        public ActionResult Index()
        {
            return View(db.otelodas.ToList());
        }

        public ActionResult HotelLogin()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult HotelLogin(bitirme.Models.otel otel)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var usr = db.otels.Single(u => u.otelkullaniciAdi == otel.otelkullaniciAdi && u.otelSifre == otel.otelSifre);
                if (usr != null)
                {
                    Session["otelID"] = usr.otelID.ToString();
                    Session["otelkullaniciAdi"] = usr.otelkullaniciAdi.ToString();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı");
                }
            }
            return View();
        }
        public ActionResult otelupdate(int? otelid)
        {
            return View(db.otels.Find(otelid));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult otelupdate(bitirme.Models.otel s)
        {
            var k = db.otels.Find(s.otelID);
            k.otelID = s.otelID;
            k.otelAdi = s.otelAdi;
            k.otelkullaniciAdi = s.otelkullaniciAdi;
            k.adres = s.adres;
            k.otelEmail = s.otelEmail;
            k.otelSifre = s.otelSifre;
            k.otelTel = s.otelTel;
            k.otelaciklama = s.otelaciklama;
            db.SaveChanges();


            return View("otelupdate");
        }
        public ActionResult otelodaEkle()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult otelodaEkle(bitirme.Models.oteloda s, int? otelid)
        {
            //string file = Path.GetFileNameWithoutExtension(s.resim.FileName);
            //string ex = Path.GetExtension(s.resim.FileName);
            //file = file + DateTime.Now.ToString("yymmssfff") + ex;
            //s.odaresim = "~/Resimler/OdaResims" + file;
            //file = Path.Combine(Server.MapPath("~/Resimler/OdaResims"), file);
            //s.resim.SaveAs(file);

            using (OurDbContext db = new OurDbContext())
            {
                if (ModelState.IsValid)
                {                  
                   
                    db.otelodas.Add(s);
                    db.SaveChanges();
                    return RedirectToAction("resim");
                }
            }
            return RedirectToAction("otelodaEkle");
        }
        public ActionResult otelozellik(int id = 0)
        {
            ozellik oze = new ozellik();
            using (OurDbContext db = new OurDbContext())
            {
                if (id != 0)
                {
                    oze.selectedID = oze.kategoriID.Split(',').ToArray();
                }
                oze.kategoris = db.kategoris.ToList();
            }
            return View(oze);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult otelozellik(bitirme.Models.ozellik oz, int? otelid)
        {
            oz.kategoriID = string.Join(",", oz.selectedID);
            using (OurDbContext db = new OurDbContext())
            {
                if (oz.ozellikID == 0)
                {
                    oz.otelID = otelid;
                    db.ozelliks.Add(oz);
                }
                else
                {
                    db.Entry(oz).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            return RedirectToAction("otelozellik", new { id = 0 });
        }

        public ActionResult resim()
        {
            return View();
        }
        [HttpPost]
        //public ActionResult resim( int? otelid, bitirme.Models.otelresim s)//Bu resimde otelid çekilmiyo onu düzelt
        //{
        //    string file = Path.GetFileNameWithoutExtension(s.resim.FileName);
        //    string ex = Path.GetExtension(s.resim.FileName);
        //    file = file + DateTime.Now.ToString("yymmssfff") + ex;
        //    s.otelresimAdi = "~/Resimler/OtelResims" + file;
        //    file = Path.Combine(Server.MapPath("~/Resimler/OtelResims"), file);
        //    s.resim.SaveAs(file);
        //    using (OurDbContext db = new OurDbContext())
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            s.otelID = otelid;
        //            db.otelresims.Add(s);
        //            db.SaveChanges();
        //            return RedirectToAction("resim");
        //        }
        //    }
        //    return View(s);

        //}

        public ActionResult or()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult or(bitirme.Models.otel s)
        //{
        //    string file = Path.GetFileNameWithoutExtension(s.resim.FileName);
        //    string ex = Path.GetExtension(s.resim.FileName);
        //    file = file + DateTime.Now.ToString("yymmssfff") + ex;
        //    s.or = "~/Resimler/OtelImage" + file;
        //    file = Path.Combine(Server.MapPath("~/Resimler/OtelImage"), file);
        //    s.resim.SaveAs(file);
        //    using (OurDbContext db = new OurDbContext())
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.otels.Add(s);
        //            db.SaveChanges();
        //            return RedirectToAction("HotelLogin");
        //        }
        //    }
        //    return View(s);
        //}

    }
}