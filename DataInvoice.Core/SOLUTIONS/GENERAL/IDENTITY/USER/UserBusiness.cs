using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER
{
    public class UserBusiness
    {
        public const string keySignOtp = "AZERTY123456789p";
        public USER.UserProvider userProvider = null;


        public UserBusiness(USER.UserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        public UserBusiness(NGLib.DATA.CONNECTOR.IDataConnector Connector)
        {
            this.userProvider = new UserProvider(Connector);
        }






        /// <summary>
        /// Création de l'utilisateur avec envoi de mail
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public LocalUser CreateNewUser(FORM.CreateUserForm form, GENERAL.ACCOUNT.Account account)
        {
            LocalUser user = this.userProvider.CreateUser(form, account);




            return user;
        }












        //public User CreateUserFromContact(Contact contact)
        //{
        //    if (contact == null || string.IsNullOrWhiteSpace(contact.GetString("IDContact")) || string.IsNullOrWhiteSpace(contact.Mail)) return null;
        //    try
        //    {

        //        //préparation
        //        FORM.CreateUserForm form = new FORM.CreateUserForm();
        //        form.IDCEntity = contact.IDCentity;
        //        form.Mail = contact.Mail;
        //        form.Phone = contact.MobilePhone;
        //        form.Pseudo = contact.FirstName;
        //        //string password = UserTools.PasswordHash(form.Password);
        //        //form.Password = password;
        //        form.Validate();

        //        // Inscription
        //        User user = this.userProvider.CreateUser(form);
        //        this.userProvider.UpdateBubble(contact, "UserId", user.UserId); // associer contact 

        //        // envoi du mail
        //        string content = ContentFinalizeregistration(user);
        //        this.SendMail(user, "INSCRIPTION COPROP", content);

        //        return user;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}







        public string GenerateSign(int UserId)
        {
            return NGLib.DATA.FORMAT.CryptHash.EasyHash(NGLib.DATA.FORMAT.StringUtilities.Complete(UserId.ToString(), 7, true, true) + UserBusiness.keySignOtp);
        }










        public bool SendMail(LocalUser user, string Subject, string BodyContent, bool html = true)
        {
            try
            {
                NGLib.COMPONENTS.NET.MailSender sender = new NGLib.COMPONENTS.NET.MailSender();
                System.Net.Mail.MailMessage mail = sender.Create();

                mail.To.Add(user.SecurityMail);
                mail.Subject = Subject;
                mail.Body = BodyContent;
                mail.IsBodyHtml = html;


                sender.SendMail(mail);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
















        #region todelete






        //public UserOtp SendOtpSMS(LocalUser user, string ContentMessage = null, string OtpMode = "SMS", string RedirectUrlAfter = null, string Sender = null)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(user.SecurityPhone)) throw new Exception("SecurityPhone Empty");
        //        UserOtp otp = userProvider.CreateOtp(user, OtpMode, 1800, true, RedirectUrlAfter); // expire dans 30minutes
        //        if (!string.IsNullOrWhiteSpace(Sender)) userProvider.UpdateBubble(otp, "Sender", Sender);

        //        if (string.IsNullOrWhiteSpace(ContentMessage)) ContentMessage = "Votre code de validation : " + otp.OtpKey;
        //        else ContentMessage = ContentMessage.Replace("{{otp}}", otp.OtpKey);

        //        // !!! Envoyer le SMS
        //        //Nuegy.Lib.EXCHANGES.OVH.TELEPHONY.SmsProvider smsprovider = new Nuegy.Lib.EXCHANGES.OVH.TELEPHONY.SmsProvider();
        //        //smsprovider.DefineNuegyTestOVH();
        //        //smsprovider.OvhAuth();
        //        //smsprovider.SendOneSms(ContentMessage, user.SecurityPhone);

        //        return otp;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("SendOtpSMS " + ex.Message, ex);
        //    }
        //}


        //public UserOtp SendOtpMail(LocalUser user, string ContentMessage = null, string OtpMode = "MAIL", string RedirectUrlAfter = null, string Sender = null)
        //{
        //    try
        //    {
        //        UserOtp otp = userProvider.CreateOtp(user, OtpMode, (86400 * 14), false, RedirectUrlAfter); // expire dans 14 jours
        //        if (!string.IsNullOrWhiteSpace(Sender)) userProvider.UpdateBubble(otp, "Sender", Sender);

        //        if (string.IsNullOrWhiteSpace(ContentMessage)) ContentMessage = "Bonjour,<br /> veuillez trouver ci-dessous Votre code de validation à usage unique <br /><br /> CODE: " + otp.OtpKey + " <br /><br />Cordialement,<br />";
        //        else ContentMessage = ContentMessage.Replace("{{otp}}", otp.OtpKey);

        //        // !!! Envoyer le MAIL

        //        return otp;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("SendOtpSMS " + ex.Message, ex);
        //    }
        //}



        //public UserOtp ValidateOtp(string OtpKey, int UserId = 0, bool DeleteAfter = true)
        //{
        //    // !!! ajouter sécurité (cache, valid ip, ...)

        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(OtpKey) || OtpKey.Length < 4 || OtpKey.Length > 24) throw new Exception("OtpKey invalid");
        //        UserOtp otp = userProvider.FindOtp(OtpKey);
        //        if (otp == null) return null;

        //        if (UserId > 0) // secure user
        //        {
        //            if (UserId != otp.UserId)
        //                throw new Exception("OtpUser Invalid");
        //        }

        //        //user = this.userProvider.GetUserInfo(otp.UserId);
        //        if (DeleteAfter) this.userProvider.DeleteBubble(otp);
        //        return otp;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


        #endregion



    }
}
