using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Web.Controllers
{
    public class DevelopersController : Controller
    {
        // GET: Developers
        public ActionResult Index()
        {
            return RedirectToAction("ApiDoc");
            return View();
        }

        public ActionResult ApiDoc()
        {
            return View();
        }


    }
}