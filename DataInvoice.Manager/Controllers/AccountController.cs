using DataInvoice.COMPONENTS.WEB.MVC;
using DataInvoice.Core.COMPONENTS.WEB.MVC;
using DataInvoice.Manager;
using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.SERVICE;
 
using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.Identity;
using MySql.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
namespace DataInvoice.Manager.Controllers
{

 //[DataInvoice.COMPONENTS.WEB.MVC.MyAuthorize]
    public class AccountController : DataInvoiceBaseMvcController
    {
        //
        // GET: /Identity/

        #region Accesseur



        protected DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.UserBusiness userManager
        {
            get { if (_userManager == null) _userManager = new DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.UserBusiness(this.Connector); return _userManager; }
        }
        private DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.UserBusiness _userManager = null;






        #endregion

     
        public ActionResult Index()
        {
             if (this.MyUser != null) return RedirectToAction("AccountManager");
            
            else return RedirectToAction("Login");
        }
       

        public ActionResult Signout()
        {
            try
            {
                this.Session.Clear();
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Login", "Home");
        }

//******************************Changer le mot de passe*****************************************//
        private ApplicationUserManager userManagerApp;
        public ApplicationUserManager UserManager
        {
            get
            {
                return userManagerApp ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManagerApp = value;
            }
        }

        public ActionResult ResetPassrod()
        {
            return View();
        }
        public async Task<ActionResult> ChangePassSendMail(string Email)
        {

            try
            {
                var user = await UserManager.FindByNameAsync(Email);
                if (user != null)
                {
                    DataInvoice.SOLUTIONS.GENERAL.IDENTITY.SERVICE.EmailService email = new SOLUTIONS.GENERAL.IDENTITY.SERVICE.EmailService();
                    email.SendMail(Email, UserManager.GenerateUserToken("ChangePassword", user.Id.ToString()));
                   
                    // Don't reveal that the user does not exist
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
            }
            catch (Exception ex)
            {
                this.PageInfo.CreateAlert("Login Error", 4);
            }
            return RedirectToAction("Login", "Login");
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            //var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            //if (result.Succeeded)
            //{
            //    return RedirectToAction("ResetPasswordConfirmation", "Account");
            //}
            //AddErrors(result);
             

            //UserProvider userprovider = new UserProvider(this.Connector);
            //UserStoreManager usermanager = new UserStoreManager(userprovider);
            //EmailService emailservice = new EmailService();
            //LocalUser myuser = userprovider.GetUser(Email);
            //try
            //{
            //    if (myuser != null)
            //    {
            //        //emailservice.SendMail(Email);
            //        usermanager.GeneratePasswordResetTokenAsync(Email);

            //        System.Diagnostics.Debug.WriteLine("Envoie okkkkkkkkkkkkkk");


            //    }
            //    else throw new Exception("User not found");
            //}
            //catch (Exception ex)
            //{
            //    this.PageInfo.CreateAlert("Login Error", 4);
            //}
            //return RedirectToAction("Login", "Login");
        }
       
         public ActionResult ChangePassword()
        {
            return View();
        }

//******************************Fin Changer le mot de passe*****************************************//

        public ActionResult AccountManager()
        {
            this.PageInfo.SetPageInfo("Gestion du compte", this.MyUser.UserName);
            return View(this.MyUser);
        }

        [MyAuthorize]
        [HttpPost]
        public ActionResult AccountManager(string Pseudo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Pseudo) || Pseudo.Length < 3) throw new Exception("Pseudo emtpy");
                this.MyUser.Pseudo = Pseudo;
                this.userManager.userProvider.SaveBubble(this.MyUser);
                this.PageInfo.CreateAlert("Modification terminée", 2);
            }
            catch (Exception ex)
            {
                this.PageInfo.CreateAlert("Modification Impossible", 4);
            }
            return RedirectToAction("AccountManager");
        }


        //[MyAuthorize]
        //[HttpPost]
        //public ActionResult AccountManager_Password(string OldPassword, string Password, string ConfirmPassword)
        //{
        //    if (string.IsNullOrWhiteSpace(Password)) return RedirectToAction("AccountManager");
        //    try
        //    {
        //        // mot de passe actuel
        //        if (string.IsNullOrWhiteSpace(OldPassword)) throw new Exception("OldPassword invalid");
        //        if (!UserTools.PasswordHash(OldPassword).Equals(this.MyUser.Password)) throw new Exception("OldPassword invalid");

        //        // validation nouveau
        //        Password = Password.Trim();
        //        if (Password.Length < 5) throw new Exception("Password invalid");
        //        if (!Password.Equals(ConfirmPassword)) throw new Exception("ConfirmPassword invalid");

        //        //maj
        //        string encodePassword = UserTools.PasswordHash(Password);
        //        userManager.userProvider.UpdateBubble(this.MyUser, "Password", encodePassword);

        //        this.PageInfo.CreateAlert("Votre mot de passe a été modifié", 2);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.PageInfo.CreateAlert("Mise à jour Impossible", 4);
        //    }
        //    return RedirectToAction("AccountManager");
        //}








        // Utilisé(e) pour la protection XSRF lors de l'ajout de connexions externes
        private const string XsrfKey = "XsrfId";
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



        #region todelete

        //[MyAuthorize]
        //[HttpPost]
        //public ActionResult AccountManager_SecurityMail(string SecurityMail)
        //{

        //    if (string.IsNullOrWhiteSpace(SecurityMail) || this.MyUser.SecurityMail.Equals(SecurityMail)) // RAS
        //        return RedirectToAction("AccountManager");
        //    try
        //    {
        //        if (SecurityMail.Length < 5) throw new Exception("SecurityMail invalid");
        //        LocalUser myuser2 = this.userManager.userProvider.GetUser(this.MyUser.UserId, false);
        //        myuser2.SecurityMail = SecurityMail; // on modifie temporairement pour envoyer l'otp

        //        UserOtp otpsms = userManager.SendOtpMail(myuser2, null, "CHANGEMAIL", null, SecurityMail);
        //        this.PageInfo.CreateAlert("Mail de validation envoyé a " + SecurityMail, 2);

        //        return RedirectToAction("ValidateOTPMAIL");
        //    }
        //    catch (Exception ex)
        //    {
        //        this.PageInfo.CreateAlert("Mise à jour Impossible", 4);
        //    }


        //    return RedirectToAction("AccountManager");
        //}

        //[MyAuthorize]
        //[HttpPost]
        //public ActionResult AccountManager_SecurityPhone(string SecurityPhone)
        //{

        //    if (string.IsNullOrWhiteSpace(SecurityPhone) || this.MyUser.SecurityPhone.Equals(SecurityPhone)) // RAS
        //        return RedirectToAction("AccountManager");
        //    try
        //    {
        //        if (SecurityPhone.Length < 10) throw new Exception("SecurityPhone invalid");

        //        LocalUser myuser2 = this.userManager.userProvider.GetUser(this.MyUser.UserId, false);
        //        myuser2.SecurityPhone = SecurityPhone; // on modifie temporairement pour envoyer l'otp

        //        UserOtp otpsms = userManager.SendOtpSMS(myuser2, null, "CHANGEPHONE", null, SecurityPhone);
        //        this.PageInfo.CreateAlert("Sms envoyé a " + SecurityPhone, 2);

        //        return RedirectToAction("ValidateOTPSMS");
        //    }
        //    catch (Exception ex)
        //    {
        //        this.PageInfo.CreateAlert("Mise à jour Impossible", 4);
        //    }

        //    return RedirectToAction("AccountManager");
        //}



        //public ActionResult Register()
        //{
        //    this.PageInfo.SetPageInfo("Création d'un compte");
        //    return View();
        //}



        //[HttpPost]
        //public ActionResult Register(DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.FORM.CreateUserForm form)
        //{
        //    try
        //    {
        //        form.Validate();
        //        DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.FORM.CreateUserForm createform = form;

        //        DataInvoice.SOLUTIONS.GENERAL.ACCOUNT.AccountProvider accountprovide = new SOLUTIONS.GENERAL.ACCOUNT.AccountProvider(this.Connector);
        //        DataInvoice.SOLUTIONS.GENERAL.ACCOUNT.Account accountuin = accountprovide.GetAccount(1); // !!! tous sur le 1 pour le moment


        //        UserProvider userprovider = new UserProvider(this.Connector);
        //        UserBusiness userManager = new UserBusiness(userprovider);
        //        LocalUser myuser = userprovider.CreateUser(createform, accountuin);

        //        //string content = userManager.ContentMailNewUser(myuser);
        //        //DataInvoice.GENERAL.IDENTITY.USER.UserOtp otpmail = userManager.SendOtpMail(myuser, content, "CHANGEMAIL", null, createform.Mail);

        //        myuser = userprovider.AuthLogin(form.Mail, form.Password);
        //        this.LocalAuthorizeUser(myuser);



        //        return RedirectToAction("AccountManager");



        //    }
        //    catch (Exception)
        //    {
        //        this.PageInfo.CreateAlert("Register Error", 4);
        //    }
        //    return RedirectToAction("Register");
        //}




        //public ActionResult ValidateOTPSMS(string OtpKeyValidate = null, string id = null)
        //{
        //    if (!string.IsNullOrWhiteSpace(id)) OtpKeyValidate = id;
        //    ViewBag.UserId = base.MyUser.UserId;

        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(OtpKeyValidate))
        //        {
        //           UserProvider userprovider = new UserProvider(this.Connector);
        //            UserBusiness userManager = new UserBusiness(userprovider);
        //            UserOtp otp = userManager.ValidateOtp(OtpKeyValidate, base.MyUser.UserId, true);

        //            if (otp.OtpMode.Equals("CHANGEPHONE"))
        //            {
        //                userprovider.UpdateBubble(this.MyUser, "SecurityPhone", otp.Sender);
        //                this.PageInfo.CreateAlert("Votre numéro de téléphone de sécurité à été mis à jour", 2);
        //                return RedirectToAction("AccountManager");
        //            }



        //            if (!string.IsNullOrWhiteSpace(otp.RedirectUrl)) return Redirect(otp.RedirectUrl);
        //            else { this.PageInfo.CreateAlert("OTP Validé", 2); return RedirectToAction("index"); }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.PageInfo.CreateAlert("Impossible de valider l'OTP", 4);
        //    }
        //    return View();
        //}





        //public ActionResult ValidateOTPMail(string OtpKeyValidate = null, string id = null, string RedirectUrl = null, string Mail = null)
        //{
        //    if (!string.IsNullOrWhiteSpace(id)) OtpKeyValidate = id;
        //    ViewBag.Mail = Mail;
        //    ViewBag.UserId = base.MyUser.UserId;


        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(OtpKeyValidate))
        //        {
        //            UserProvider userprovider = new UserProvider(this.Connector);
        //            UserBusiness userManager = new UserBusiness(userprovider);
        //            UserOtp otp = userManager.ValidateOtp(OtpKeyValidate, base.MyUser.UserId, true);

        //            if (otp.OtpMode.Equals("CHANGEMAIL"))
        //            {
        //                userprovider.UpdateBubble(this.MyUser, "SecurityMail", otp.Sender);
        //                this.PageInfo.CreateAlert("Votre Mail de sécurité à été mis à jour", 2);
        //                return RedirectToAction("AccountManager");
        //            }

        //            if (!string.IsNullOrWhiteSpace(RedirectUrl)) return Redirect(RedirectUrl);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.PageInfo.CreateAlert("Impossible de valider l'OTP", 4);
        //    }


        //    return View();
        //}

        #endregion

    }
}
