using ICoreInterfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AuthoritySystem.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        protected List<string> CurentAuthorizedUrlList { get; set; }

        public IAuthorityManager GetIrep()
        {
            IAuthorityManager tempIrep = null;
            var assem = Assembly.Load(ConfigurationManager.AppSettings["UserRepositoryAssembly"]);
            foreach (var typeItem in assem.GetTypes())
            {
                if (string.Compare(typeItem.Name, "AuthorityManager") == 0 && typeItem.IsClass)
                {
                    tempIrep = assem.CreateInstance(typeItem.FullName) as IAuthorityManager;
                    //var tempList = irep.GetAllUsers();                                    
                }
            }
            return tempIrep;
        }
        public IMembership GetIMem()
        {
            IMembership Imem = null;
            Assembly assm = Assembly.Load(ConfigurationManager.AppSettings["UserRepositoryAssembly"]);
            foreach(var typeItem in assm.GetTypes())
            {
                if(string.Compare(typeItem.Name,"MyMembership")==0&&typeItem.IsClass)
                {
                    Imem = assm.CreateInstance(typeItem.FullName) as IMembership;
                }
            }
            return Imem;
        }

    }
}
