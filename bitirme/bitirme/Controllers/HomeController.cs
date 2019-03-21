using bitirme.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult oteldetay()
        {
            return View();
        }
        public ActionResult res()
        {
            return View();
        }
        [HttpPost]
        public ActionResult res(bitirme.Models.sehir s)
        {
            string fileName = Path.GetFileNameWithoutExtension(s.res.FileName);
            string extension = Path.GetExtension(s.res.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            s.sehirresim = "~/images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
            s.res.SaveAs(fileName);
            using (OurDbContext db=new OurDbContext())
            {
                db.sehirs.Add(s);
                db.SaveChanges();
            }
            return View();
        }
    }
}   