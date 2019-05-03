using bitirme.Models;
using Newtonsoft.Json;
using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace bitirme.Controllers
{
    public class userController : Controller
    {
        OurDbContext db = new OurDbContext();

        public class CaptchaResult
        {
            [JsonProperty("success")]
            public bool Success { get; set; }
            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }
        // GET: user
        public ActionResult Index()
        {
            var a = db.rezerves.Count();
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
            HomeIndexView hm = new HomeIndexView();
            hm.otel = db.otels.Where(a => a.otelID > 0).Take(3).ToList();
            hm.sehir = db.sehirs.Where(a => a.sehirID > 0).ToList();
            hm.oteloda = db.otelodas.Where(a => a.odaID > 0).Take(3).ToList();
            return View(hm);
        }
        public ActionResult userRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult userRegister(bitirme.Models.User user)

        {
            var captcha = Request.Form["g-recaptcha-response"];

            const string secret = "6Lf3A6EUAAAAAKd06PJgKtG8cKkSqSwdzLvzu4jX";

            var restUrl = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, captcha);

            WebRequest req = WebRequest.Create(restUrl);
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

            JsonSerializer serializer = new JsonSerializer();

            CaptchaResult result = null;

            using (var reader = new StreamReader(resp.GetResponseStream()))
            {
                string resultObject = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<CaptchaResult>(resultObject);
            }

            if (!result.Success)
            {
                ModelState.AddModelError("", "captcha mesajınız hatalı");
                if (result.ErrorCodes != null)
                {
                    ModelState.AddModelError("", result.ErrorCodes[0]);
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.users.Add(user);
                    db.SaveChanges();
                }
                return RedirectToAction("userRegister");
            }

            return View();
        }
        public ActionResult userLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult userLogin(bitirme.Models.User user)
        {
            var usr = db.users.Single(u => u.userNickName == user.userNickName && u.userPassword == user.userPassword);
            if (usr != null)
            {
                Session["userID"] = usr.userID;
                ViewBag.userID = usr.userID;
                Session["userName"] = usr.userName;
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı");
            }
            return View();
        }
        public ActionResult userUpdate(int userid)
        {
            return View(db.users.Find(userid));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult userUpdate(bitirme.Models.User user)
        {
            var k = db.users.Find(user.userID);
            k.userName = user.userName;
            k.userLastName = user.userLastName;
            k.userEmail = user.userEmail;
            k.userPhone = user.userPhone;
            k.userPassword = user.userPassword;
            k.userNickName = user.userNickName;
            db.SaveChanges();

            return RedirectToAction("Index", "user");
        }
        public ActionResult oteller(string sad, int userid)
        {
            HomeIndexView hm = new HomeIndexView();
            hm.otel = db.otels.Where(x => x.sehir == sad).ToList();
            hm.sehir = db.sehirs.Where(a => a.sehirID > 0).Take(3).ToList();
            hm.oteloda = db.otelodas.Where(a => a.odaID > 0).Take(3).ToList();
            return View(hm);
        }
        public ActionResult oteltarih(int oad, int userid)
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
            ViewBag.userID = hı.userID;
            ViewBag.kisi = hı.kisi;
            hm.otel = db.otels.Where(a => a.otelID > 0).Take(3).ToList();
            hm.sehir = db.sehirs.Where(a => a.sehirID > 0).Take(3).ToList();
            hm.oteloda = db.otelodas.Where(x => x.otelID == hı.otelID).ToList();
            hm.rezer = db.rezerves.Where(x => x.rezerveID > 0).ToList();
            return View(hm);
        }
        public ActionResult oteldetay(int oad)
        {
            oteldetayview ov = new oteldetayview();
            ov.otel = db.otels.Where(x => x.otelID == oad).ToList();
            ov.otelresim = db.otelresims.Where(x => x.otelID == oad).ToList();
            ov.oteloda = db.otelodas.Where(x => x.otelID == oad).ToList();
            ov.kategoris = db.kategoris.Where(x => x.kategoriID > 0).ToList();
            ov.ozelliks = db.ozelliks.Where(x => x.otelID == oad).ToList();
            return View(ov);
        }
        public ActionResult odadetay()
        {
            return View();
        }
        public ActionResult rezerve(int odaid, int otelid, DateTime gtarih, DateTime ctarih, int kisi, int userid)
        {
            HomeIndexView hm = new HomeIndexView();
            otel otel = db.otels.Find(otelid);
            oteloda oteloda = db.otelodas.Find(odaid);
            hm.otelad = otel.otelAdi;
            hm.otelID = otelid;
            hm.userID = userid;
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
        [HttpPost]
        public ActionResult rezerve(bitirme.Models.rezerve rez, bitirme.Models.HomeIndexView hm)
        {
            if (Session["userID"] != null)
            {
                rezerve r = new rezerve();
                r.ctarih = hm.ctarih;
                r.Durum = true;
                r.gtarih = hm.gtarih;
                r.gunsayisi = hm.gunsayisi;
                r.kisi = hm.kisi;
                r.odaID = hm.odaID;
                r.otelID = hm.otelID;
                r.ucret = hm.ucret;
                r.userID = hm.userID;
                db.rezerves.Add(r);
                db.SaveChanges();
                return RedirectToAction("Index", "user");
            }
            else
            {
                return RedirectToAction("userRegister", "user");
            }
        }
        public ActionResult rezerveliste(int userid)
        {
            RezerveView rez = new RezerveView();
            rez.rezerves = db.rezerves.Where(x => x.userID == userid).ToList();
            rez.otels = db.otels.ToList();
            rez.otelodas = db.otelodas.ToList();
            rez.users = db.users.Where(x => x.userID == userid).ToList();
            return View(rez);
        }
    }
}