using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Domain;
using Domain.Model;
using System.Web.Hosting;
using System.Threading;

namespace Lib
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private AuthorityManager am = new AuthorityManager();
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
                return false;          
            else
                return true;
        }
    }
}
