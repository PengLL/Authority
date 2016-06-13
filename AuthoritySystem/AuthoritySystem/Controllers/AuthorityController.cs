using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lib;
using Domain.Model;
using System.Text.RegularExpressions;
using ICoreInterfaces;
using System.Reflection;
using System.Configuration;
using ICoreInterfaces.ModelIrepository;


namespace AuthoritySystem.Controllers
{
    [MyAuthorize]
    public class AuthorityController : BaseController
    {
        //
        // GET: /Authority/

        private IAuthorityManager irep = new BaseController().GetIrep();
        
        
        public ActionResult RoleAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RoleAdd(Role Role)
        {
            Session["RoleName"] = Role.Role_Name;
            if (ModelState.IsValid && irep.GetRole(Role.Role_Name).Role_Name == null)
            {
                if ( irep.AddRole(Role.Role_Name))
                    return RedirectToAction("RoleAuthoritiesChoose");
                else
                    return View();
            }
            else
            {
                ModelState.AddModelError("","角色已经存在");
                return View();
            }
                        
        }
        public ActionResult RoleDelete(int roleId)
        {
            irep.DeleteRole(roleId);
            return RedirectToAction("RoleManage","Home");
        }
        public ActionResult RoleAuthoritiesChoose()
        {
            //Role role = manager.GetUsersRole();
            //string roleName = role.Role_Name;
            return View(irep.GetAllAuthorities());
        }
        [HttpPost]
        public ActionResult RoleAuthoritiesChoose(string authoritiesId)
        {
            string[] array = authoritiesId.Split(',');
            if (Session["RoleName"] == null)
                return RedirectToAction("RoleManage","Home");
            else
            {               
                string roleName = Session["RoleName"].ToString();
                int roleId = irep.GetRole(roleName).Role_Id;
                irep.ClearRolesAuthority(roleId);
                for (int i = 0; i < array.Length; i++)
                {
                    int authorityId = int.Parse(array[i]);
                    irep.AddRolesAuthority(roleId, authorityId);
                }
            }
            return View("Ok");
        }
        public ActionResult RoleChoose()
        {
            ViewBag.AllRoles = irep.GetAllRoles();
            return View();
        }
        [HttpPost]
        public ActionResult RoleChoose(string roleName)
        {
            if (Session["userName"] == null)
                return RedirectToAction("UserManage","Home");
            string userName = Session["userName"].ToString();
            IRole role = irep.GetRole(roleName);
            Iuser user = irep.GetUser(userName);
            irep.AddUserInRoles(user.User_Id, role.Role_Id);
            return View("OK");
        }
        public ActionResult RolesUsers(string roleName)
        {
            return View(irep.GetRolesUsers(roleName));
        }
        public ActionResult RolesSearch(string text)
        {
            return View(irep.SearchRoles(text));
        }
        public ActionResult AuthorityChanged(string roleName)
        {
            Session["RoleName"] = roleName;
            return RedirectToAction("RoleAuthoritiesChoose");
        }
        public ActionResult AuthorityCheck(string roleName)
        {
            return View(irep.GetRolesAuthority(roleName));
        }
        public ActionResult AuthorityAdd()
        {
            ViewBag.AllUrl = GetAsssemblyUrl();    
            return View();
        }
        [HttpPost]
        public ActionResult AuthorityAdd(Authorities authorities)
        {                   
            ViewBag.AllUrl = GetAsssemblyUrl();
            bool flag=true;
            IAuthorities[] list=irep.GetAllAuthorities();
            foreach(var item in list)
            {
                if(item.Url==authorities.Url||item.Authority_Name==authorities.Authority_Name)
                    flag=false;
            }
            if(ModelState.IsValid&&flag)
            {
                irep.AddAuthority(authorities);
                return RedirectToAction("AuthorityManage","Home");
            }
            else
            {
                ModelState.AddModelError("","该权限已存在");
                return View();
            }
            
        }
        public ActionResult AuthorityUpdate(string authorityName)
        {
            ViewBag.AllUrl = GetAsssemblyUrl();;
            return View(irep.GetAuthority(authorityName));
        }
        [HttpPost]
        public ActionResult AuthorityUpdate(Authorities authority)
        {
            irep.SaveAuthority(authority);
            return RedirectToAction("AuthorityManage","Home");
        }
        public ActionResult AuthorityDelete(string authorityName)
        {
            irep.DeleteAuthority(authorityName);
            return RedirectToAction("AuthorityManage","Home");
        }       
        public ActionResult UserUpdate(string userName)
        {
            IRole role = irep.GetUsersRole(userName);
            if (role.Role_Name == null)
            {
                ViewBag.Role = irep.GetUsersRole("normal");
            }
            else
                ViewBag.Role = role;
            ViewBag.AuthorityList = irep.GetRolesAuthority(role.Role_Name);
            return View(irep.GetUser(userName));
        }
        public ActionResult UserAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserAdd(User user)
        {
            if(ModelState.IsValid&&irep.GetUser(user.User_Name).User_Name==null)
            {
                if (irep.AddUser(user))
                {
                    Session["userName"] = user.User_Name;
                    return RedirectToAction("RoleChoose");
                }
                else
                    return View();
            }                   
            else
            {
                ModelState.AddModelError("", "用户名已存在");
                return View();
            }
                
        }
        public ActionResult UserDelete(int userId)
        {
            irep.DeleteUser(userId);
            return RedirectToAction("UserManage","Home");
        }
        public ActionResult UsersRoleChange(int userId)
        {
            Session["userId"] = userId;
            return View(irep.GetAllRoles());
        }
        [HttpPost]
        public ActionResult UsersRoleChange(string roleName)
        {
            int userId = int.Parse(Session["userId"].ToString());
            int roleId=irep.GetRole(roleName).Role_Id;
            irep.UpdateUsersRole(userId, roleId);
            return View("OK");
        }      
        public ActionResult UsersSearch(string text)
        {
            return View(irep.SearchUsers(text));
        }
        public List<string> GetAsssemblyUrl()
        {
            List<string> list = new List<string>();
            IAuthorities[] AuList = irep.GetAllAuthorities();
            Type type = typeof(HomeController);
            var classes = type.Assembly.GetTypes();
            foreach (var method in classes)
            {
                if (method.Name.Contains("Controller")||method.Name.Contains("AccountController"))
                {
                    var me = method.GetMethods();
                    foreach (var item in me)
                    {
                        var str = item.Name;
                        var methodName = item.ReturnType.ToString();
                        if (methodName.Contains("ActionResult"))
                        {
                            string subName = method.Name.Substring(0, method.Name.Length - 10);
                            string s = "/" + subName + "/" + str;
                            if (list.Contains(s)||list.Contains("Account")) ;
                            else
                                list.Add(s);
                        }
                    }
                }
            }
            foreach(var item in AuList)
            {
                list.Remove(item.Url);
            }
            return list;
        }     
    }
   
}
