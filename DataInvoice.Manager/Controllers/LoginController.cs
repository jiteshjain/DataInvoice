using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER;
using DataInvoice.Core.COMPONENTS.WEB.MVC;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using DataInvoice.Manager.Models;
using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.Identity;
using Microsoft.AspNet.Identity;
namespace DataInvoice.Manager.Controllers
{
    //[RequireHttps]
    public class LoginController : DataInvoiceBaseMvcController
    {

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            if (MyUser != null) return RedirectToAction("Index", "Invoices"); //, new { Area = "Invoice" }
            else return RedirectToAction("login");
            //return View();
        }


        public ActionResult Login()
        {
            if (Request.Cookies["UserSettings"] != null)
            {
                var value = Request.Cookies["UserSettings"].Value;
                string Email = value.ToString().Split('&')[0].Split('=')[1];
                string Password = value.ToString().Split('&')[1].Split('=')[1];

                ViewBag.username = Email;
                ViewBag.password = Password;
                ViewBag.RememberMe = true;
                
            }
            else
            {
                ViewBag.RememberMe = false;
                return View();
            }

            this.PageInfo.SetPageInfo("Authentification");
            return View();
        }

        private ApplicationSignInManager _signInManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        [HttpPost]
        public async Task<ActionResult> Login(string username, string password, bool RememberMe ,string returnUrl)
        {
            try
            {
                ViewBag.RememberMe = RememberMe;
                var result = await SignInManager.PasswordSignInAsync(username, password, RememberMe, shouldLockout: false);
                switch (result)
                {
                        case SignInStatus.Success:
                           ApplicationUser appUser = await UserManager.FindByEmailAsync(username);
                           this.Session["myuser"] = appUser;
                           this.Session["myUserId"] = appUser.Id;
                            return RedirectToLocal(returnUrl);
                     
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View();
                }

                //if (!string.isnullorwhitespace(username))
                //{
                //    userprovider userprovider = new userprovider(this.connector);
                //    userstoremanager usermanager = new userstoremanager(userprovider);
                //    localuser myuser = usermanager.authlogin(username, password);
                //    if (rememberme && myuser != null && myuser.isauthenticated)
                //    {
                //        httpcookie mycookie = new httpcookie("usersettings");
                //        mycookie["email"] = username;
                //        mycookie["pwd"] = password;
                //        mycookie.expires = datetime.now.adddays(30d);
                //        response.cookies.add(mycookie);

                //        this.localauthorizeuser(myuser);
                //        return redirecttoaction("index", "home", new { userid = myuser.userid });
                //    }
                //    else
                //        if (myuser != null && myuser.isauthenticated)
                //        {
                //            this.localauthorizeuser(myuser);
                //            return redirecttoaction("index", "home", new { userid = myuser.userid });
                //        }
                //        else throw new exception("user not found");
                //}
            }
            catch (Exception ex)
            {
                this.PageInfo.CreateAlert("Login Error", 4);
            }
            return RedirectToAction("Login");
        }


        //[HttpPost]
        //public ActionResult Login(string username, string password, bool RememberMe = false)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(username))
        //        {



        //            UserProvider userprovider = new UserProvider(this.Connector);
        //            UserStoreManager usermanager = new UserStoreManager(userprovider);
        //            LocalUser myuser = usermanager.AuthLogin(username, password);
        //            if (RememberMe && myuser != null && myuser.IsAuthenticated)
        //            {
        //                HttpCookie myCookie = new HttpCookie("UserSettings");
        //                myCookie["Email"] = username;
        //                myCookie["PWD"] = password;
        //                myCookie.Expires = DateTime.Now.AddDays(30d);
        //                Response.Cookies.Add(myCookie);

        //                this.LocalAuthorizeUser(myuser);
        //                return RedirectToAction("Index", "Login", new { UserId = myuser.UserId });
        //            }
        //            else
        //                if (myuser != null && myuser.IsAuthenticated)
        //                {
        //                    this.LocalAuthorizeUser(myuser);
        //                    return RedirectToAction("Index", "Login", new { UserId = myuser.UserId });
        //                }
        //                else throw new Exception("User not found");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.PageInfo.CreateAlert("Login Error", 4);
        //    }
        //    return RedirectToAction("Login");
        //}
  
        [HttpPost]
            public async Task<ActionResult> RenewpasswordToken(string password0, string password, string rpassword)
        {
            var value =  HttpContext.Request.Params.Get("token");
            string url = TempData.Peek("valurl").ToString();
         
            string split1 = url.Split('?')[1];
            string token = split1.Split('&')[0];
            string email = split1.Split('&')[1];
            string finaltoken = token.Substring(token.IndexOf("=") + 1);
            string finalemail = email.Split('=')[1];

            try
            {
                ApplicationUser appUser = await UserManager.FindByEmailAsync(finalemail);
                if (appUser != null)
                {
                    if (await UserManager.VerifyUserTokenAsync(appUser.Id, "ChangePassword", finaltoken))
                    {

                        IdentityResult identityResult = await UserManager.ChangePasswordAsync(appUser.Id, appUser.PasswordHash, password);
                       
                            if(identityResult.Succeeded)
                                return RedirectToAction("Login", "Login");
                            else 
                                return RedirectToAction("Login", "Login");
                        }
                    }

                    //UserProvider userprovider = new UserProvider(this.Connector);
                    //UserStoreManager usermanager = new UserStoreManager(userprovider);
                    //LocalUser myuser = usermanager.VerifUserToken(finalemail, finaltoken);
                    //if (myuser != null)
                    //{
                    //    LocalUser verifpassword = usermanager.AuthLogin(finalemail, password0);
                    //    if (verifpassword != null)
                    //    {
                    //        userprovider.UpdatePasswordToken(finalemail, password);
                    //    }
                    //    else return RedirectToAction("Login");

                    //}
                    //else return RedirectToAction("Login");
                
            }
            catch (Exception ex)
            {
                this.PageInfo.CreateAlert("Login Error", 4);
            }
            return RedirectToAction("Login");
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            Dictionary<string, string> str = new Dictionary<string, string>();

            try
            {
                //if(NGLib.COMPONENTS.APP.AppCore.GlobalAPIServer)
                //return Content();
                if (System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"] == null) throw new Exception("DefaultConnection Null");
                if (base.Connector == null) throw new Exception("Connector Null");
                return Content("OK");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public ActionResult Renewpassword()
        {
            

             
            var val = Request.QueryString["token"];
             
            var valurl = Request.RawUrl;
            TempData["valurl"] = valurl;
            return View();
        }
        public ActionResult ForgetPass()
        {
            return View();
        }

        public ActionResult ForgetPassword(string Email)
        {
                   return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }


            return RedirectToAction("Index", "Home");
        }

    }
}