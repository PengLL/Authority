using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Lib;
using Domain.Model;
using System.Reflection;
using ICoreInterfaces;
using System.Configuration;
using System.Security.Principal;
using System.Web.Hosting;
using ICoreInterfaces.ModelIrepository;

namespace AuthoritySystem.Controllers
{
    [MyAuthorize]
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        private IAuthorityManager irep = new BaseController().GetIrep();
        private HttpRequest request = System.Web.HttpContext.Current.Request;
        private string userName = System.Web.HttpContext.Current.User.Identity.Name;
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Menu()
        {           
            IAuthorities[] list;
            IRole role=irep.GetUsersRole(userName);
            List<Authorities> urlListToShow=new List<Authorities>();
            if (role.SuperAdmin == true)
                list = irep.GetAllAuthorities();
            else
                list = irep.GetRolesAuthority(role.Role_Name);
            foreach(Authorities au in list)
            {
                if(au.Url.Contains("Home"))
                {
                    urlListToShow.Add(au);
                }
            }
            ViewBag.UrlListToShow = urlListToShow;
            ViewBag.UserName = userName;          
            return PartialView();
        }
        public ActionResult Advertise()
        {
            return View();
        }
        public ActionResult RoleManage()
        {
            IRole[] list = irep.GetAllRoles();
            return View(list);
        }
        public ActionResult UserManage()
        {
            Iuser[] list = irep.GetAllUsers();
            return View(list);
        }      
        public ActionResult AuthorityManage()
        {
            IAuthorities[] list = irep.GetAllAuthorities();
            return View(list);
        }
        public ActionResult LogManage()
        {
            Iuser user = irep.GetUser(userName);
            ViewBag.UserName = userName;
            ViewBag.UserId = user.User_Id;
            ViewBag.Date = DateTime.Now;
            return View();
        }       
        [HttpPost]
        public ActionResult LogManage(Log log)
        {
            if(ModelState.IsValid)
            {
                irep.SaveLog(log.User_Id, log.Date, log.Content);
            }     
            return View("Index");
        }
       
        //public List<string> GetAsssemblyUrl()
        //{
        //    List<string> list = new List<string>();
        //    Type type = typeof(HomeController);
        //    var classes = type.Assembly.GetTypes();
        //    foreach (var method in classes)
        //    {
        //        if (method.Name.Contains("Controller"))
        //        {
        //            var me = method.GetMethods();
        //            foreach (var item in me)
        //            {
        //                var str = item.Name;
        //                var methodName = item.ReturnType.ToString();
        //                if (methodName.Contains("ActionResult"))
        //                {
        //                    string subName = method.Name.Substring(0, method.Name.Length - 10);
        //                    string s = "/" + subName + "/" + str;
        //                    if (list.Contains(s)) ;
        //                    else
        //                        list.Add(s);
        //                }

        //            }
        //        }
        //    }
        //    return list;
        //}
       


    }
}
