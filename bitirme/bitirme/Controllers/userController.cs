using bitirme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bitirme.Controllers
{
    public class userController : Controller
    {
        OurDbContext db = new OurDbContext();
        // GET: user
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult userRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult userRegister(bitirme.Models.User user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
            }
            return RedirectToAction("userRegister");
        }
        public ActionResult userLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult userLogin(bitirme.Models.User user)
        {
            var usr = db.users.Single(u => u.userNickName == user.userNickName && u.userPassword == user.userPassword);
            if (usr!=null)
            {
                Session["userID"] = usr.userID.ToString();
                Session["userName"] = usr.userName.ToString();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("","Kullanıcı Adı veya Şifre Hatalı");
            }
            return View();
        }
        public ActionResult userUpdate(int? id)
        {            
            return View(db.users.Find(id));
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

            return RedirectToAction("userUpdate","user");
        }
    }
}