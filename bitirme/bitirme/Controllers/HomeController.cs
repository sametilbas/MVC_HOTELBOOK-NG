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
            var a = db.rezerves.Count();
            if (a!=0)
            {
                for (int i = 1; i < a; i++)
                {
                    var b = db.rezerves.Find(i);
                    if (b.ctarih < DateTime.Now)
                    {
                        if (b.Durum == true)
                        {
                            b.Durum = false;
                            db.SaveChanges();
                        }
                    }
                }
            }
            HomeIndexView hm = new HomeIndexView();
            hm.otel = db.otels.Where(x => x.otelID > 0).Take(3).ToList();
            hm.sehir = db.sehirs.Where(x => x.sehirID > 0).ToList();
            hm.oteloda = db.otelodas.Where(x => x.odaID > 0).Take(3).ToList();
            return View(hm);
        }
        public ActionResult Search(bitirme.Models.HomeIndexView hı)
        {
            if (db.sehirs.Where(x => x.sehirAd == hı.search).SingleOrDefault() != null)
            {
                HomeIndexView hm = new HomeIndexView();
                hm.otel = db.otels.Where(x => x.sehir == hı.search).ToList();
                hm.sehir = db.sehirs.Where(a => a.sehirID > 0).Take(3).ToList();
                hm.oteloda = db.otelodas.Where(a => a.odaID > 0).Take(3).ToList();
                return RedirectToAction("oteller", hm);
            }
            else if (db.otels.Where(x => x.otelAdi == hı.search).SingleOrDefault() != null)
            {
                oteldetayview ov = new oteldetayview();
                ov.otel = db.otels.Where(x => x.otelAdi == hı.search).ToList();
                otel a = db.otels.Find(hı.search);
                ov.otelresim = db.otelresims.Where(x => x.otelID == a.otelID).ToList();
                ov.oteloda = db.otelodas.Where(x => x.otelID == a.otelID).ToList();
                return RedirectToAction("oteldetay",ov);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult oteller(string sad)
        {
            HomeIndexView hm = new HomeIndexView();
            hm.otel = db.otels.Where(x => x.sehir == sad).ToList();
            hm.sehir = db.sehirs.Where(a => a.sehirID > 0).Take(3).ToList();
            hm.oteloda = db.otelodas.Where(a => a.odaID > 0).Take(3).ToList();
            return View(hm);
        }
        public ActionResult oteltarih(int oad)
        {
            HomeIndexView hm = new HomeIndexView();
            otel otel = db.otels.Find(oad);
            hm.otelad = otel.otelAdi;
            hm.otelID = oad;
            return View(hm);
        }
        public ActionResult odalar(bitirme.Models.HomeIndexView hı)
        {
            //21.5.2019 00:00:00 gelen tarih
            HomeIndexView hm = new HomeIndexView();
            ViewBag.gtarih = hı.gtarih;
            ViewBag.ctarih = hı.ctarih;
            ViewBag.kisi = hı.kisi;
            hm.otel = db.otels.Where(a => a.otelID > 0).Take(3).ToList();
            hm.sehir = db.sehirs.Where(a => a.sehirID > 0).Take(3).ToList();
            hm.oteloda = db.otelodas.Where(x => x.otelID == hı.otelID).ToList();
            hm.rezer = db.rezerves.Where(x => x.rezerveID > 0).ToList();
            return View(hm);
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
        public ActionResult oteldetay(int oad)
        {
            oteldetayview ov = new oteldetayview();
            ov.otel = db.otels.Where(x => x.otelID == oad).ToList();
            ov.otelresim = db.otelresims.Where(x => x.otelID == oad).ToList();
            ov.oteloda = db.otelodas.Where(x => x.otelID == oad).ToList();
            ov.kategoris = db.kategoris.Where(x=>x.kategoriID>0).ToList();
            ov.ozelliks = db.ozelliks.Where(x => x.otelID == oad).ToList();
            ov.yorums = db.yorums.Where(x => x.otelID == oad).Take(3).ToList();
            return View(ov);
        }
        public ActionResult odadetay()
        {
            return View();
        }
        public ActionResult rezerve(int odaid,int otelid,DateTime gtarih,DateTime ctarih,int kisi)
        {
            HomeIndexView hm = new HomeIndexView();
            if (Session["userID"] == null)
            {
                ViewBag.mesaj = "Rezervasyon yapmak için lütfen giriş yapıp tekrar deneyiniz!";
                return View();
            }
            else if(Session["userID"]!=null)
            {
                otel otel = db.otels.Find(otelid);
                oteloda oteloda = db.otelodas.Find(odaid);
                hm.otelad = otel.otelAdi;
                hm.otelID = otelid;
                int yil = (ctarih.Year - gtarih.Year) * 365;
                int ay = 0;
                if (ctarih.Month == 1 || ctarih.Month == 3 || ctarih.Month == 5 || ctarih.Month == 7 || ctarih.Month == 8 || ctarih.Month == 10 || ctarih.Month == 12)
                {
                    if (gtarih.Month == 1 || gtarih.Month == 3 || gtarih.Month == 5 || gtarih.Month == 7 || gtarih.Month == 8 || gtarih.Month == 10 || gtarih.Month == 12)
                    {
                        ay = (ctarih.Month - gtarih.Month) * 31;
                    }
                    else if (gtarih.Month == 4 || gtarih.Month == 6 || gtarih.Month == 9 || gtarih.Month == 11)
                    {
                        ay = ((ctarih.Month * 31) - (gtarih.Month * 30));
                    }
                    else if (gtarih.Month == 2)
                    {
                        ay = (ctarih.Month * 31) - (gtarih.Month * 28);
                    }
                }
                else if (ctarih.Month == 4 || ctarih.Month == 6 || ctarih.Month == 9 || ctarih.Month == 11)
                {
                    if (gtarih.Month == 1 || gtarih.Month == 3 || gtarih.Month == 5 || gtarih.Month == 7 || gtarih.Month == 8 || gtarih.Month == 10 || gtarih.Month == 12)
                    {
                        ay = (ctarih.Month * 30) - (gtarih.Month * 31);
                    }
                    else if (gtarih.Month == 4 || gtarih.Month == 6 || gtarih.Month == 9 || gtarih.Month == 11)
                    {
                        ay = (ctarih.Month - gtarih.Month) * 30;
                    }
                    else if (gtarih.Month == 2)
                    {
                        ay = (ctarih.Month * 30) - (gtarih.Month * 28);
                    }
                }
                else if (ctarih.Month == 2)
                {
                    if (gtarih.Month == 1 || gtarih.Month == 3 || gtarih.Month == 5 || gtarih.Month == 7 || gtarih.Month == 8 || gtarih.Month == 10 || gtarih.Month == 12)
                    {
                        ay = (ctarih.Month * 28) - (gtarih.Month * 31);
                    }
                    else if (gtarih.Month == 4 || gtarih.Month == 6 || gtarih.Month == 9 || gtarih.Month == 11)
                    {
                        ay = ((ctarih.Month * 28) - (gtarih.Month * 30));
                    }
                    else if (gtarih.Month == 2)
                    {
                        ay = (ctarih.Month * 28) - (gtarih.Month * 28);
                    }
                }
                int gun = 0;
                if (ctarih.Month > gtarih.Month)
                {
                    if (ctarih.Day > gtarih.Day)
                    {
                        gun = ctarih.Day - gtarih.Day;
                    }
                    else if (ctarih.Day == gtarih.Day)
                    {
                        gun = 0;
                    }
                    else if (ctarih.Day < gtarih.Day)
                    {
                        gun = gtarih.Day - ctarih.Day;
                    }
                }
                else if (ctarih.Month == gtarih.Month)
                {
                    gun = ctarih.Day - gtarih.Day;
                }
                int topgun = yil + ay + gun;
                hm.gunsayisi = topgun;
                hm.ucret = (topgun * oteloda.odaucret);
                return View(hm);
            }
            return View();
        }
        [HttpPost]
        public ActionResult rezerve(bitirme.Models.rezerve rez,bitirme.Models.HomeIndexView hm)
        {

            rezerve r = new rezerve();
            rez.ctarih = r.ctarih;
            r.Durum = true;
            rez.gtarih = r.gtarih;
            rez.kisi = r.kisi;
            rez.odaID = r.odaID;
            rez.otelID = r.otelID;
            db.rezerves.Add(r);
            db.SaveChanges();
            return View();
        }
    }
}