using DataInvoice.Core.COMPONENTS.WEB.MVC;
using DataInvoice.Core.Culture;
using DataInvoice.Manager.Models;
using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.SERVICE;
using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Facebook;
using Microsoft.Owin.Security;
using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.Identity;
namespace DataInvoice.Manager.Controllers
{
    //[RequireHttps]
    public class HomeController : DataInvoiceBaseMvcController
    {
        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                _signInManager = value;
            }
        }

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

        private const string XsrfKey = "XsrfId";
        private string providerName = "";
        private UserProvider _userprovider = null;
        private UserProvider userprovider
        { get { if (_userprovider == null) _userprovider = new UserProvider(this.Connector); return _userprovider; } }
        public ActionResult Index()
        {
            if (MyUser != null) return RedirectToAction("Index", "Account"); //, new { Area = "Invoice" }
            else return RedirectToAction("login", "login");
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
        [HttpPost]
        public async Task<ActionResult> Login(string username, string password, bool RememberMe = false)
        {
            System.Diagnostics.Debug.WriteLine("RememberMe value*************** " + RememberMe);
            var result = await SignInManager.PasswordSignInAsync(username, password, RememberMe, false);
            try
            {
                if (!string.IsNullOrWhiteSpace(username))
                {
                    UserProvider userprovider = new UserProvider(this.Connector);
                    UserStoreManager usermanager = new UserStoreManager(userprovider);
                    LocalUser myuser = usermanager.AuthLogin(username, password);
                    if (RememberMe && myuser != null && myuser.IsAuthenticated)
                    {
                        HttpCookie myCookie = new HttpCookie("UserSettings");
                        myCookie["Email"] = username;
                        myCookie["PWD"] = password;
                        myCookie.Expires = DateTime.Now.AddDays(30d);
                        Response.Cookies.Add(myCookie);

                        this.LocalAuthorizeUser(myuser);
                        return RedirectToAction("Index", "Home", new { UserId = myuser.UserId });
                    }
                    else
                        if (myuser != null && myuser.IsAuthenticated)
                        {
                            this.LocalAuthorizeUser(myuser);
                            return RedirectToAction("Index", "Home", new { UserId = myuser.UserId });
                        }
                        else throw new Exception("User not found");
                }
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

        public ActionResult ChangePassSendMail(string Email)
        {
            UserProvider userprovider = new UserProvider(this.Connector);
            EmailService emailservice = new EmailService();
            LocalUser myuser = userprovider.GetUser(Email);
            try
            {
                if (myuser != null)
                {
                    //  emailservice.SendMail(Email);
                    System.Diagnostics.Debug.WriteLine("Envoie okkkkkkkkkkkkkk");
                }
                else throw new Exception("User not found");
            }
            catch (Exception ex)
            {
                this.PageInfo.CreateAlert("Login Error", 4);
            }
            return RedirectToAction("Login");
        }
        public ActionResult ChangePassword()
        {


            return View();
        }

        public ActionResult ForgetPass()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public async Task<ActionResult> RegisterVue(string fullname, string email, string address, string city, string Country, string username, string password, string rpassword)
        {
            //System.Diagnostics.Debug.WriteLine("*************** " + fullname);
            //System.Diagnostics.Debug.WriteLine("*************** " + email);
            //System.Diagnostics.Debug.WriteLine("*************** " + address);
            //System.Diagnostics.Debug.WriteLine("*************** " + city);
            //System.Diagnostics.Debug.WriteLine("*************** " + Country);
            //System.Diagnostics.Debug.WriteLine("*************** " + username);
            //System.Diagnostics.Debug.WriteLine("*************** " + password);
            //System.Diagnostics.Debug.WriteLine("*************** " + rpassword);


            var user = new ApplicationUser { UserName = email, Email = email};
            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
               // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                return RedirectToAction("Login", "Login");
            }

            return RedirectToAction("Register");
        }





        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            providerName = provider;
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Home", new { ReturnUrl = returnUrl }));
        }


        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            var dta = HttpContext.GetOwinContext();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            //var url= HttpContext.Current.Request.QueryString;
            // Sign in the user with this external login provider if the user already has a login
            //var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            UserProvider userprovider = new UserProvider(this.Connector);
            UserStoreManager usermanager = new UserStoreManager(userprovider);
            var email = "";
            if (loginInfo.Login.LoginProvider == "Facebook")
            {
                var identity = AuthenticationManager.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
                var access_token = identity.FindFirstValue("FacebookAccessToken");
                var fb = new FacebookClient(access_token);
                object myInfo = fb.Get("/me?fields=name,email");
                // specify the email field
                //string emailid = (new System.Collections.Generic.Mscorlib_DictionaryValueCollectionDebugView<string, object>(((Facebook.JsonObject)myInfo).Values).Items[0])
                // var soap = JsonConvert.DeserializeObject<object>(myInfo);
                string str = myInfo.ToString();
                Demo d = JsonConvert.DeserializeObject<Demo>(str);
                email = d.email;
            }
            else
            {
                email = loginInfo.Email;
            }

            LocalUser myuser = usermanager.ExternalAuthLogin(email);
            //if (myuser != null && myuser.IsAuthenticated)
            //{
            //    this.LocalAuthorizeUser(myuser);
            //    return RedirectToAction("Index", "Home", new { IDUser = myuser.IDUser });
            //}		Key	"email"	string

            if (myuser == null)
            {
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
            }
            else
            {
                this.LocalAuthorizeUser(myuser);
                return RedirectToAction("Index", "Home", new { UserId = myuser.UserName });
            }

            //    switch (result)
            //{

            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
            //    case SignInStatus.Failure:
            //    default:
            //        // If the user does not have an account, then prompt the user to create an account

            //}
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                //SOLUTIONS.GENERAL.IDENTITY.USER.FORM.CreateUserForm user = new SOLUTIONS.GENERAL.IDENTITY.USER.FORM.CreateUserForm()
                //{
                //    Mail = model.Email,
                //    IDContact = 0,
                //    Password= model.Password,
                //    ConfirmPassword=model.ConfirmPassword,
                //    Phone=model.Phone,
                //    Pseudo= model.Name
                //};
                //var localuser=userprovider.CreateUser(user, new SOLUTIONS.GENERAL.ACCOUNT.Account());
                //if (localuser != null)
                //{
                //   // this.LocalAuthorizeUser(myuser);
                //    return RedirectToAction("Index", "Home", new { IDUser = localuser.UserName });
                //}
                SOLUTIONS.GENERAL.IDENTITY.USER.LocalUser user = new SOLUTIONS.GENERAL.IDENTITY.USER.LocalUser
                {
                    UserName = model.Email,
                    IDAccount = 1,
                    IDContact = 0,
                    Password = model.Password,
                    Pseudo = (model.Name != "" && model.Name != null) ? model.Name : model.Email,
                    SecurityMail = model.Email,
                    SecurityPhone = model.Phone,
                    UserLevel = SOLUTIONS.GENERAL.IDENTITY.USER.ENUMS.UserLevelEnum.STANDARD
                };

                UserProvider userprovider = new UserProvider(this.Connector);
                UserStoreManager usermanager = new UserStoreManager(userprovider);
                LocalUser myuser = usermanager.CreateExternalLogin(user);
                if (myuser == null)
                {
                    IdentityResult result = new IdentityResult("something went wrong while registering please try again after sometime.");
                    AddErrors(result);
                }
                else
                {
                    this.LocalAuthorizeUser(myuser);
                    return RedirectToAction("Index", "Home", new { UserId = myuser.UserName });
                }
                //var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                //var result = await UserManager.CreateAsync(user);
                //if (result.Succeeded)
                //{
                //    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                //    if (result.Succeeded)
                //    {
                //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                //        return RedirectToLocal(returnUrl);
                //    }
                //}
                //AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        internal class Demo
        {
            public string name { get; set; }
            public string email { get; set; }
        }
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }



            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}