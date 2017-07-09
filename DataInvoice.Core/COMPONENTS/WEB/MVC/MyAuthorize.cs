using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.COMPONENTS.WEB.MVC
{
    public class MyAuthorize : System.Web.Mvc.AuthorizeAttribute
    {
        public static string ihmPassword = "clearstream";

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                

                if (httpContext.Session == null) return false;
                LocalUser myuser = (LocalUser)httpContext.Session["myuser"];
                if (myuser == null) return false;
                if (!myuser.IsAuthenticated) return true;

                if (!string.IsNullOrWhiteSpace(this.Roles))
                {
                    if (!myuser.IsInRole(this.Roles)) return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            try
            {
                bool valid = this.AuthorizeCore(filterContext.HttpContext);

                if (!valid)
                {
                    var url = new UrlHelper(filterContext.RequestContext);
                    var logonUrl = url.Action("Login", "Login", new { area=""});
                    filterContext.Result = new RedirectResult(logonUrl);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}