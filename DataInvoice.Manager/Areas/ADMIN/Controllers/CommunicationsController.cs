using DataInvoice.Core.COMPONENTS.WEB.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Manager.Areas.ADMIN.Controllers
{
    [DataInvoice.COMPONENTS.WEB.MVC.MyAuthorize(Roles = "ADMIN")]
    public class CommunicationsController : DataInvoiceBaseMvcController
    {

        // NGLIB COMPONENTS.RELATION.COMMUNICATION




        // GET: ADMIN/Communications
        public ActionResult Index()
        {
            return View();
        }
    }
}