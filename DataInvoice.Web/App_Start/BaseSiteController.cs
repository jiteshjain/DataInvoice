using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Web
{
    public class BaseSiteController : Controller
    {
        public const string defaulLang = "fr";


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string lang = null;
            try
            {
                lang = (string)filterContext.RouteData.Values["lang"];
                if (string.IsNullOrWhiteSpace(lang))
                {
                    filterContext.Result = RedirectToAction("Index", "Home", new { lang = defaulLang });
                    return;
                }

                System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

            }
            catch (Exception e)
            {
                //throw new NotSupportedException(String.Format("ERROR: Invalid language code '{0}'.", lang));
            }
            filterContext.Controller.ViewBag.lang = lang;

            base.OnActionExecuting(filterContext);
        }





    }
}