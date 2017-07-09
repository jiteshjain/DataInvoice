using DataInvoice.Core.COMPONENTS.WEB.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Manager.Areas.ADMIN.Controllers
{
    [DataInvoice.COMPONENTS.WEB.MVC.MyAuthorize( Roles="ADMIN")]
    public class AdminMainController : DataInvoiceBaseMvcController
    {
        // GET: ADMIN/AdminHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Te()
        {
            return Content("OK");
        }


    }
}