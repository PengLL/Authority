using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Model;
using Lib;
using System.Web.Security;
using ICoreInterfaces;

namespace AuthoritySystem.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/
        private IMembership memberiship = new BaseController().GetIMem();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid && memberiship.ValidateUser(user.User_Name, user.Pwd))
            {
                FormsAuthentication.SetAuthCookie(user.User_Name, true);
                string Name4 = FormsAuthentication.FormsCookieName; 
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "用户名或密码错误");
                return View(user);
            }               
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}
