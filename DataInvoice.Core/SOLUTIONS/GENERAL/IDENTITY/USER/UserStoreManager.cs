
using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.SERVICE;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Security;

namespace DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER
{
    /// <summary>
    /// Gestion utilisateur
    /// </summary>
    // Implémentation officiel de ASP.net
    public class UserStoreManager //: UserManager<LocalUser, int>
    {
        UserProvider userProvider;
      //  UserStore userStore { get { return (UserStore)this.Store; } }

        public UserStoreManager(UserProvider userprovider)// : base(new UserStore(userprovider))
        {
            this.userProvider = userprovider;
        }


        // !!! revoir cette méthode pour la rendre standard et implémenter les méthode officiel de login
        public LocalUser AuthLogin(string UserName, string Password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password)) throw new Exception("Credentials Invalids");
                Dictionary<string, object> ins = new Dictionary<string, object>();
                ins.Add("UserName", UserName);
                ins.Add("PasswordHash", UserTools.PasswordHash(Password));
                
                string sql = "SELECT * FROM identity_users WHERE UserName=@UserName AND PasswordHash=@PasswordHash";
                System.Data.DataTable ret = this.userProvider.Connector.Query(sql, ins);
                if (ret.Rows.Count == 0) return null;

                LocalUser user = new LocalUser(ret.Rows[0]);
                user._IsAuthenticated = true; // Warning
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public LocalUser VerifUserToken(string userName, String securityphone)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(securityphone)) throw new Exception("Credentials Invalids");
                Dictionary<string, object> ins = new Dictionary<string, object>();
                ins.Add("userName", userName);
                ins.Add("securityphone", securityphone);


                string sql = "SELECT * FROM identity_users WHERE userName=@userName AND securityphone=@securityphone";
                System.Data.DataTable ret = this.userProvider.Connector.Query(sql, ins);
                if (ret.Rows.Count == 0) return null;

                LocalUser user = new LocalUser(ret.Rows[0]);
                user._TokenExiste = true; // Warning
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GeneratePasswordResetTokenAsync(string Email)
        {

            string token = Membership.GeneratePassword(15, 0);
            token = Regex.Replace(token, @"[^a-zA-Z0-9]", m => "9");
           
            LocalUser myuser = userProvider.GetUser(Email);
            EmailService emailservice = new EmailService();
            try
            {
                if (myuser != null)
                {
                    //update database
                    myuser = userProvider.UpdateUserToken(Email, token);
                    emailservice.SendMail(Email,token);
                }
                else throw new Exception("User not found");
            }
            catch
            {
                return ("Exception");
            }
            return token;
        }
        public LocalUser ExternalAuthLogin(string UserName = null)
        {
            try
            {
                //if (string.IsNullOrWhiteSpace(UserName)) throw new Exception("Credentials Invalids");
                Dictionary<string, object> ins = new Dictionary<string, object>();
                ins.Add("UserName", UserName == null ? (object)DBNull.Value : UserName);

                //ins.Add("IDAccount", IDCEntity);

                string sql = "SELECT * FROM identity_users WHERE UserName=@UserName";
                System.Data.DataTable ret = this.userProvider.Connector.Query(sql, ins);
                if (ret.Rows.Count == 0) return null;

                LocalUser user = new LocalUser(ret.Rows[0]);
                user._IsAuthenticated = true; // Warning
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public LocalUser CreateExternalLogin(LocalUser model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.UserName)) throw new Exception("Credentials Invalids");
                Dictionary<string, object> ins = new Dictionary<string, object>();
                ins.Add("UserName", model.UserName);
                ins.Add("entityid", 1);
                ins.Add("UserLevel", 1);
                ins.Add("idcentity", "All");
                ins.Add("datecreate", DateTime.Now);

                this.userProvider.Connector.Insert("identity_users", ins, true);
                var user = ExternalAuthLogin(model.UserName);
                user._IsAuthenticated = user.IsAuthenticated; // Warning
                return user;

                //string sql = "SELECT * FROM identity_users WHERE UserName=@UserName AND passwordhash=@passwordhash";
                //System.Data.DataTable ret = this.userProvider.Connector.Query(sql, ins);
                //if (ret.Rows.Count == 0) return null;

                //LocalUser user = new LocalUser(ret.Rows[0]);
                // user._IsAuthenticated = true; // Warning
                //return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}

