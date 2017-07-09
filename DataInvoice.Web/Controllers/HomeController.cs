using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Web.Controllers
{
    public class HomeController : BaseSiteController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Solutions()
        {
            return View();
        }
        public ActionResult Pricing()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        public ActionResult Help()
        {
            return View();
        }











        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }




    }
}