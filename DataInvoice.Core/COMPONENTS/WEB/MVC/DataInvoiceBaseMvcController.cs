using DataInvoice.GLOBAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

 

namespace DataInvoice.Core.COMPONENTS.WEB.MVC
{
    public class DataInvoiceBaseMvcController : System.Web.Mvc.Controller
    {


        protected NGLib.DATA.CONNECTOR.IDataConnector Connector
        //{ get { if (_Connector == null) _Connector = DataInvoice.Core.SOLUTIONS.GENERAL.ACCES.CoreAcces.GetTestConnector(); return _Connector; } }
       { get { if (_Connector == null) _Connector = NGLib.COMPONENTS.APP.AppCore.GetDataConnector(); return _Connector; } }
        private NGLib.DATA.CONNECTOR.IDataConnector _Connector = null;

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;
            // Validate culture name
            cultureName = DataInvoice.Core.Culture.CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }


        protected System.Web.Mvc.ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = DataInvoice.Core.Culture.CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }


        protected DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.Identity.ApplicationUser MyUser
        {
            get { if (_MyUser == null && this.Session["myuser"] != null) _MyUser = (DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.Identity.ApplicationUser)this.Session["myuser"]; return _MyUser; }
        }
        private DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.Identity.ApplicationUser _MyUser = null;




        /// <summary>
        /// Pour les Helpers de la page (sera remplacer par layoutmanager)
        /// </summary>
        protected DataInvoice.Core.COMPONENTS.WEB.MVC.PageInfoManager PageInfo
        {
            get
            {
                if (_pageinfo == null) { _pageinfo = DataInvoice.Core.COMPONENTS.WEB.MVC.PageInfoManager.Open(this); }
                return _pageinfo;
            }
        }
        private DataInvoice.Core.COMPONENTS.WEB.MVC.PageInfoManager _pageinfo = null;

        /// <summary>
        /// Authorisation de l'utilisateur et affectation des variables users
        /// </summary>
        /// <param name="myuser"></param>
        protected void LocalAuthorizeUser(DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.LocalUser myuser)
        {
            try
            {
                // on valide l'utilisateur (controles de bases)
                if (!myuser.IsAuthenticated) throw new Exception("Utilisateur non authentifié");
                if (!myuser.IsInRole("STANDARD")) throw new Exception("Utilisateur non admis");
                //this.UserDatas = new UserDataCache(myuser);

                // autres (obsolete)
                this.Session["myuser"] = myuser;
                this.Session["myUserId"] = myuser.UserId;
                //this.Session["myidentity"] = this.UserDatas.MySyndic.IDCEntity;

            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}


