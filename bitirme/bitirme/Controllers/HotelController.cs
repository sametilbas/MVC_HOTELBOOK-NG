using bitirme.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace bitirme.Controllers
{
    public class HotelController : Controller
    {
        OurDbContext db = new OurDbContext();
        // GET: Hotel
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult or()
        {
            ViewBag.otelID = new SelectList(db.otels, "otelID");
            return View();
        }
        [HttpPost]
        public ActionResult or(bitirme.Models.otel s, HttpPostedFileBase foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    WebImage img = new WebImage(foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(600, 400);
                    img.Save("~/Resimler/OtelImage" + newfoto);
                    s.or = "~/Resimler/OtelImage" + newfoto;
                    db.otels.Add(s);
                    db.SaveChanges();
                    return RedirectToAction("HotelLogin");
                }
            }
            return View(s);
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
                    ViewBag.otelID = usr.otelID;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı");
                }
            }
            return View();
        }
        public ActionResult otelupdate(int otelid)
        {
            var ot = db.otels.Where(m => m.otelID == otelid).SingleOrDefault();
            if (ot == null)
            {
                return HttpNotFound();
            }
            return View(ot);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult otelupdate(bitirme.Models.otel s, int otelid, HttpPostedFileBase foto)
        {
            var yeni = db.otels.Where(m => m.otelID == otelid).SingleOrDefault();
            if (foto != null)
            {
                if (System.IO.File.Exists(Server.MapPath(yeni.or)))
                {
                    System.IO.File.Delete(Server.MapPath(yeni.or));
                }

                WebImage img = new WebImage(foto.InputStream);
                FileInfo fotoinfo = new FileInfo(foto.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Resize(600, 400);
                img.Save("~/Resimler/OtelImage" + newfoto);
                s.or = "~/Resimler/OtelImage" + newfoto;
                yeni.adres = s.adres;
                yeni.otelaciklama = s.otelaciklama;
                yeni.otelAdi = s.otelAdi;
                yeni.otelEmail = s.otelEmail;
                yeni.otelkullaniciAdi = s.otelkullaniciAdi;
                yeni.otelSifre = s.otelSifre;
                yeni.otelTel = s.otelTel;
                yeni.sehir = s.sehir;
                db.SaveChanges();
            }

            return View("otelupdate");
        }
        public ActionResult oteldelete(int otelid)
        {
            var ot = db.otels.Where(m => m.otelID == otelid).SingleOrDefault();
            if (ot == null)
            {
                return HttpNotFound();
            }
            return View(ot);
        }
        [HttpPost]
        public ActionResult oteldelete(int otelid, FormCollection collection)
        {
            var sil = db.otels.Where(m => m.otelID == otelid).SingleOrDefault();
            if (sil == null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(sil.or)))
            {
                System.IO.File.Delete(Server.MapPath(sil.or));
            }
            db.otels.Remove(sil);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult otelodaliste(int otelid)
        {
            return View(db.otelodas.Where(m => m.otelID == otelid).ToList());
        }
        public ActionResult otelodaEkle(int otelid)
        {
            //var x = db.otelodas.Where(m=>m.otelID==otelid).SingleOrDefault();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult otelodaEkle(bitirme.Models.oteloda s, HttpPostedFileBase foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    WebImage img = new WebImage(foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(600, 400);
                    img.Save("~/Resimler/OdaResims" + newfoto);
                    s.odaresim = "~/Resimler/OdaResims" + newfoto;
                    db.otelodas.Add(s);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("otelodaEkle");
        }
        public ActionResult otelodaDuzenle(int odaid)
        {
            var ot = db.otelodas.Where(m => m.odaID == odaid).SingleOrDefault();
            if (ot == null)
            {
                return HttpNotFound();
            }
            return View(ot);
        }
        [HttpPost]
        public ActionResult otelodaDuzenle(bitirme.Models.oteloda s, HttpPostedFileBase foto)
        {
            var yeni = db.otelodas.Where(m => m.odaID == s.odaID).SingleOrDefault();
            if (foto != null)
            {
                if (System.IO.File.Exists(Server.MapPath(yeni.odaresim)))
                {
                    System.IO.File.Delete(Server.MapPath(yeni.odaresim));
                }

                WebImage img = new WebImage(foto.InputStream);
                FileInfo fotoinfo = new FileInfo(foto.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Resize(600, 400);
                img.Save("~/Resimler/OtelImage" + newfoto);
                s.odaresim = "~/Resimler/OtelImage" + newfoto;
                yeni.aciklama = s.aciklama;
                yeni.odaAdi = s.odaAdi;
                yeni.odakisi = s.odakisi;
                yeni.odaucret = s.odaucret;
                db.SaveChanges();
            }

            return View("otelodaDuzenle");
        }
        public ActionResult otelodaSil(int odaid)
        {
            var ot = db.otelodas.Where(m => m.odaID == odaid).SingleOrDefault();
            if (ot == null)
            {
                return HttpNotFound();
            }
            return View(ot);
        }
        [HttpPost]
        public ActionResult otelodaSil(int odaid, FormCollection collection)
        {
            var sil = db.otelodas.Where(m => m.odaID == odaid).SingleOrDefault();
            if (sil == null)
            {
                return HttpNotFound();
            }
            if (System.IO.File.Exists(Server.MapPath(sil.odaresim)))
            {
                System.IO.File.Delete(Server.MapPath(sil.odaresim));
            }
            db.otelodas.Remove(sil);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
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
           // oz.kategoriID = string.Join(",", oz.selectedID);
            using (OurDbContext db = new OurDbContext())
            {
                if (oz.ozellikID == 0)
                {
                    for (int i = 0; i < oz.selectedID.Length; i++)
                    {
                        var temp = oz.selectedID[i];
                        oz.kategoriID = temp;
                        oz.otelID = otelid;
                        db.ozelliks.Add(oz);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("otelozellik", new { id = 0 });
        }
        public ActionResult otelresimEkle(int otelid)
        {
            return View();
        }
        [HttpPost]
        public ActionResult otelresimEkle(bitirme.Models.otelresim s, HttpPostedFileBase foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    WebImage img = new WebImage(foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(600, 400);
                    img.Save("~/Resimler/OdaResims" + newfoto);
                    s.otelresimAdi = "~/Resimler/OdaResims" + newfoto;
                    db.otelresims.Add(s);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(s);
        }

    }
}