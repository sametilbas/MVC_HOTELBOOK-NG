﻿using bitirme.Models;
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
        public ActionResult OtelRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OtelRegister(bitirme.Models.otel otel)
        {
            if (ModelState.IsValid)
            {
                db.otels.Add(otel);
                db.SaveChanges();
                return RedirectToAction("OtelRegister");
            }

            return View(otel);
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
        public ActionResult otelupdate(otel otel)
        {
            var k = db.otels.Find(otel.otelID);

            k.otelAdi = otel.otelAdi;
            k.otelkullaniciAdi = otel.otelkullaniciAdi;
            k.otelSifre = otel.otelSifre;
            k.otelEmail = otel.otelEmail;
            k.otelTel = otel.otelTel;
            k.adres = otel.adres;

            db.SaveChanges();

            return View("Index", db.otels);
        }
        public ActionResult otelresimEkle()
        {
            return View();
        }//resim ekleme hatalı
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult otelresimEkle(otelresim res, HttpPostedFileBase otelresimAdi, int? otelid)
        {
            if (ModelState.IsValid)
            {
                WebImage img = new WebImage(otelresimAdi.InputStream);
                FileInfo imginfo = new FileInfo(otelresimAdi.FileName);
                string newimg = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Resize(400, 400);
                img.Save("~/images/");
                res.otelresimAdi = "/images" + newimg;

                otelid = res.otelID;

                db.otelresims.Add(res);
                db.SaveChanges();
            }
            return RedirectToAction("otelresimEkle");
        }
        public ActionResult otelodaEkle()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult otelodaEkle(bitirme.Models.oteloda oteloda, int? otelid)
        {
            if (ModelState.IsValid)
            {
                oteloda.otelID = otelid;
                db.otelodas.Add(oteloda);
                db.SaveChanges();
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
    }
}