using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataInvoice.Manager.App_Start
{
    public class SecureAuthoriseAttribute : System.Web.Mvc.AuthorizeAttribute
    {



        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["myuser"] == null) return false;
            DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.LocalUser myuser = (DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.LocalUser)httpContext.Session["myuser"];
            if (!myuser.IsAuthenticated) return false;
            if (!myuser.IsInRole("STANDARD")) return false;

            return base.AuthorizeCore(httpContext);
        }

    }
}