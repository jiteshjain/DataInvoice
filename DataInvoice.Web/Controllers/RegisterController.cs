using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DataInvoice.Web.Models;

namespace DataInvoice.Web.Controllers
{

    public class RegisterController : BaseSiteController
    {


        public ActionResult Index()
        {
            return View();
        }






    }
}