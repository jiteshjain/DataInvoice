using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER
{
    public class UserProvider : NGLib.DATA.DATAPO.DataPOProviderSQL<LocalUser>
    {

        public UserProvider(NGLib.DATA.CONNECTOR.IDataConnector connector)
            : base(connector)
        {

        }
        public LocalUser GetUser(int UserId, bool Complete = true)
        {
            Dictionary<string, object> ins = new Dictionary<string, object>();
            ins.Add("UserId", UserId);
            LocalUser user = base.GetOneDefault<LocalUser>(ins);
            return user;
        }

        public LocalUser GetUser(string UserName, int IDAccount = 0, bool Complete = true)
        {
            Dictionary<string, object> ins = new Dictionary<string, object>();
            ins.Add("UserName", UserName);
            if (IDAccount > 0) ins.Add("IDAccount", IDAccount);
            LocalUser user = base.GetOneDefault<LocalUser>(ins);
            return user;
        }


        public LocalUser GetUser(string UserName)
        {
            Dictionary<string, object> ins = new Dictionary<string, object>();
            ins.Add("UserName", UserName);
            LocalUser user = base.GetOneDefault<LocalUser>(ins);
            return user;
        }

       

        public LocalUser CreateUser(FORM.CreateUserForm form, ACCOUNT.Account account)
        {
            try
            {
                form.Validate();
                if (GetUser(form.Mail, 0, false) != null) throw new Exception("User Already Found");
                LocalUser retour = new LocalUser();
                retour.IDAccount = account.IDAccount;
                retour["iduser"] = DBNull.Value;

                if (string.IsNullOrWhiteSpace(form.Pseudo)) retour.Pseudo = form.Mail;
                else retour.Pseudo = form.Pseudo;
                retour.UserName = form.Mail;
                retour.SecurityMail = form.Mail;
                retour.SecurityPhone = form.Phone;
                if (!string.IsNullOrWhiteSpace(form.Password)) retour.Password = UserTools.PasswordHash(form.Password);
                retour["DateCreate"] = DateTime.Now;
                retour["DateUpdate"] = DateTime.Now;
                retour.UserLevel = string.IsNullOrWhiteSpace(form.Password) ? ENUMS.UserLevelEnum.DISABLED : ENUMS.UserLevelEnum.STANDARD;

                this.InsertBubble(retour, false, true);

                return retour;
            }
            catch (Exception ex)
            {
                throw new Exception("CreateUser " + ex.Message);
            }
        }
        /*
         * //_____________________________________fonction de test de création d'un utilisateur______________________________________________//
                public LocalUser CreateUserTest(int account, string Phone, string UserName, string Password, string Pseudo, string SecurityMail)
                {
                    try
                    {
                        LocalUser retour = new LocalUser();

                        retour.IDAccount = @account;
                        retour["iduser"] = DBNull.Value;

                        retour.Pseudo = @Pseudo; ;
                        retour.UserName = @UserName; ;
                        retour.SecurityMail = @SecurityMail;
                        retour.SecurityPhone = @Phone;
                        if (!string.IsNullOrWhiteSpace(@Password)) retour.PasswordHash = UserTools.PasswordHash(@Password);
                        retour["DateCreate"] = DateTime.Now;
                        retour["DateUpdate"] = DateTime.Now;
                        retour.UserLevel = string.IsNullOrWhiteSpace(@Password) ? ENUMS.UserLevelEnum.DISABLED : ENUMS.UserLevelEnum.STANDARD;
                        this.InsertBubble(retour, false, true);


                        return retour;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("CreateUser " + ex.Message);
                    }
                }*/


        public void SaveUser(LocalUser user)
        {
            this.SaveBubble(user);
        }



        public void DeleteUser(LocalUser user)
        {
            //
            // 
            this.Connector.QueryO("delete .... WHERE userid=@p1", user.UserId); /// !!!  a faire
        }
        public LocalUser UpdateUserToken(string UserName, string securityphone)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(securityphone)) throw new Exception("Credentials Invalids");
                Dictionary<string, object> ins = new Dictionary<string, object>();
                ins.Add("userName", UserName);
                ins.Add("securityphone", securityphone);

                string sql = "UPDATE identity_users SET securityphone = @securityphone WHERE username=@username";
                System.Data.DataTable ret = this.Connector.Query(sql, ins);
                
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public LocalUser UpdatePasswordToken(string UserName, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(password)) throw new Exception("Credentials Invalids");
                Dictionary<string, object> ins = new Dictionary<string, object>();
                ins.Add("userName", UserName);
                ins.Add("PasswordHash", UserTools.PasswordHash(password));

                string sql = "UPDATE identity_users SET passwordHash=@PasswordHash WHERE username=@username";
                System.Data.DataTable ret = this.Connector.Query(sql, ins);
                
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
       

            //public UserOtp FindOtp(LocalUser user, string OtpKey, string OtpMode)
            //{
            //    if (OtpKey == null) return null;
            //    Dictionary<string, object> ins = new Dictionary<string, object>();
            //    ins.Add("UserId", user.UserId);
            //    ins.Add("OtpMode", OtpMode);
            //    ins.Add("OtpKey", OtpKey); // !! peut etre préférable de ne pas envoyer cette chaine dans la base pour la sécurité
            //    UserOtp retour = base.GetOneDefault<UserOtp>(ins);
            //    return retour;
            //}

            //public UserOtp FindOtp(string OtpKey)
            //{
            //    if (OtpKey == null) return null;
            //    Dictionary<string, object> ins = new Dictionary<string, object>();
            //    ins.Add("OtpKey", OtpKey); // !! peut etre préférable de ne pas envoyer cette chaine dans la base pour la sécurité
            //    UserOtp retour = base.GetOneDefault<UserOtp>(ins);
            //    return retour;
            //}


            //public UserOtp CreateOtp(LocalUser user, string OtpMode, int expired = 86400, bool SimplifiedOtp = false, string RedirectUrlAfter = null)
            //{
            //    string OtpKey = CreateOtpGenerateUnique(SimplifiedOtp);
            //    if (string.IsNullOrWhiteSpace(OtpKey)) throw new Exception("Impossible de générer un OTP");

            //    UserOtp retour = new UserOtp();
            //    retour.UserId = user.UserId;
            //    retour.OtpMode = OtpMode;
            //    retour.OtpKey = OtpKey;
            //    retour["DateCreate"] = DateTime.Now;
            //    retour["DateLimit"] = DateTime.Now.AddSeconds(expired);
            //    retour.RedirectUrl = RedirectUrlAfter;
            //    base.InsertBubble(retour);

            //    return retour;
            //}


            //private int OtpKeyMaxRecurs = 10; // on tente jusqu'a ce que l'on trouve une chaine de libre
            //private string CreateOtpGenerateUnique(bool SimplifiedOtp=false, int Recurs=0)
            //{
            //    string OtpKey = null;
            //    if (SimplifiedOtp) OtpKey = NGLib.DATA.FORMAT.StringUtilities.GenerateString(3, "123456789")+NGLib.DATA.FORMAT.StringUtilities.Complete(DateTime.Now.Second.ToString(),2,true);
            //    else OtpKey = NGLib.DATA.FORMAT.StringUtilities.GenerateString(22) + NGLib.DATA.FORMAT.StringUtilities.Complete(DateTime.Now.Second.ToString(), 2, true);

            //    if (FindOtp(OtpKey) == null) return OtpKey; // existe pas donc c'est bon
            //    else if (Recurs < OtpKeyMaxRecurs) return this.CreateOtpGenerateUnique(SimplifiedOtp, (Recurs + 1));
            //    else return null;
            //}


        }
}
