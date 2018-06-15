using MYSALE.Models;
using mysalecity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace mysalecity.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                Userlogindata userlogindata = null;
                using (mscContext db = new mscContext())
                {
                    userlogindata = db.Userlogindatas.FirstOrDefault(u => u.Login == model.Name && u.Password == model.Password);

                }
                if (userlogindata != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Userlogindata userlogindata = null;
                using (mscContext db = new mscContext())
                {
                    userlogindata = db.Userlogindatas.FirstOrDefault(u => u.Login == model.Name);
                }
                if (userlogindata == null)
                {
                    // создаем нового пользователя
                    using (mscContext db = new mscContext())
                    {
                        db.Userlogindatas.Add(new Userlogindata { Login = model.Name, Password = model.Password});
                        db.SaveChanges();
                        userlogindata = db.Userlogindatas.Where(u => u.Login == model.Name && u.Password == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (userlogindata != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}