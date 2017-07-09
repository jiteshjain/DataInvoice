using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Manager.Areas.ADMIN.Controllers
{
    public class AdminInvoicesController : Controller
    {
        // GET: ADMIN/AdminInvoices
        public ActionResult Index()
        {
            return View();
        }
    }
}