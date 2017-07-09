using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Manager.Controllers
{
    [DataInvoice.COMPONENTS.WEB.MVC.MyAuthorize]
    public class MainController : DataInvoice.Core.COMPONENTS.WEB.MVC.DataInvoiceBaseMvcController
    {
        // GET: Main
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Invoices");
            return View();
        }

        public ActionResult UserCulture(string culturename)
        {
            try
            {
                this.SetCulture(culturename);
            }
            catch (Exception)
            {
                
                //throw;
            }


            return RedirectToAction("Index");
        }


        public ActionResult GeneralSearch(string searchText)
        {
            ViewBag.searchText = searchText;
            return View();
        }


    }
}