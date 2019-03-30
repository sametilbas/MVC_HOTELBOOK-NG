using bitirme.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
            return View();
        }
        [HttpPost]
        public ActionResult or(bitirme.Models.otel s)
        {
            string file = Path.GetFileNameWithoutExtension(s.resim.FileName);
            string ex = Path.GetExtension(s.resim.FileName);
            file = file + DateTime.Now.ToString("yymmssfff") + ex;
            s.or = "~/Resimler/OtelImage" + file;
            file = Path.Combine(Server.MapPath("~/Resimler/OtelImage"), file);
            s.resim.SaveAs(file);
            using (OurDbContext db = new OurDbContext())
            {
                if (ModelState.IsValid)
                {
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
    }
}