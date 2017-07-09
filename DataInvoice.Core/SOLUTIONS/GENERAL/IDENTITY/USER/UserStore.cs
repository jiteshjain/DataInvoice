using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.SHARED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER
{

    /// <summary>
    /// Implémentation officiel selon microsoft.aspnet
    /// </summary>
    internal class UserStore : IIdentityUserStore // usertore a utiliser que pour l'interface avec le userProvider
    {
        public USER.UserProvider userProvider;
        public UserStore(USER.UserProvider userProvider)
        {
            this.userProvider = userProvider;
        }


        #region ==== Users methods

        public Task<LocalUser> FindByNameAndPartnerAsync(string userName, string partnerId)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");

            if (string.IsNullOrEmpty(partnerId))
                throw new ArgumentNullException("partnerId");

            string sql = "string * from identity_users WHERE username=@username AND partnerid=@partnerid"; // revoir la requette !!!
            Dictionary<string, object> ins = new Dictionary<string, object>();
            ins.Add("username", userName);
            ins.Add("partnerid", partnerId);
            LocalUser user = userProvider.GetListSQL<LocalUser>(sql, ins).FirstOrDefault();
            if (user == null) return null;
           

            return Task.FromResult(user);
        }

        public Task DeleteAsync(int id)
        {
            var l_user = userProvider.GetUser(id, false);
            if (l_user == null)
                throw new Exception("Vous essayez de supprimer un utilisateur inexistant");
            return DeleteAsync(l_user);
        }



        public Task<List<LocalUser>> GetListeAsync(string ssoId)
        {
            throw new NotImplementedException();
        }




        public Task CreateAsync(LocalUser user)
        {
            try
            {
                USER.UserBusiness userbusiness = new UserBusiness(this.userProvider);
                //userbusiness.CreateNewUser(user); // !!! implémenter , remplir form, ...
                return Task.FromResult(0);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }






        public Task DeleteAsync(LocalUser user)
        {
            userProvider.DeleteUser(user);
            return Task.FromResult(0);
        }

        public Task<LocalUser> FindByIdAsync(int userId)
        {
            var user = userProvider.GetUser(userId);
            return Task.FromResult(user);
        }

        public Task<LocalUser> FindByNameAsync(string userName)
        {
            LocalUser retour = null;
            if (string.IsNullOrWhiteSpace(userName)) return Task.FromResult(retour);
            retour = userProvider.GetListSQL("WHERE username=@p1", userName).FirstOrDefault();
            if (retour == null) return Task.FromResult(retour);
            //userProvider.EnrishUser(retour);
            return Task.FromResult(retour);
        }

        public Task UpdateAsync(LocalUser user)
        {
            userProvider.SaveUser(user);
            return Task.FromResult(0);
        }



        #endregion


        #region ====  IUserLoginStore  Implementation
        public Task<IList<UserLoginInfo>> GetLoginsAsync(LocalUser user)
        {
            throw new NotImplementedException();
        }
        public Task RemoveLoginAsync(LocalUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }
        public Task<LocalUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }
        public Task AddLoginAsync(LocalUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }


        #endregion


        #region ==== IUserPasswordStore Implementation

        public Task<string> GetPasswordHashAsync(LocalUser user)
        {
            if ((object)user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult<string>(user.Password);
        }

        public Task<bool> HasPasswordAsync(LocalUser user)
        {
            return Task.FromResult<bool>(!string.IsNullOrEmpty(user.Password));
        }

        public Task SetPasswordHashAsync(LocalUser user, string passwordHash)
        {
            if ((object)user == null)
                throw new ArgumentNullException("user");

            user.Password = passwordHash;
            return Task.FromResult<object>(null);
        }

        #endregion


        #region ==== IUserEmailStore Implementation

        public Task SetEmailAsync(LocalUser user, string email)
        {
            if ((object)user == null)
                throw new ArgumentNullException("user");

            user.SecurityMail = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(LocalUser user)
        {
            if ((object)user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.SecurityMail);
        }

        public Task<bool> GetEmailConfirmedAsync(LocalUser user) //La confirmation de l'adresse mail n'est pas géré pour l'instant
        {
            if ((object)user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(true);
        }

        public Task SetEmailConfirmedAsync(LocalUser user, bool confirmed) //La confirmation de l'adresse mail n'est pas géré pour l'instant
        {
            return Task.FromResult(0);
        }

        public Task<LocalUser> FindByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    throw new ArgumentNullException("email");
                }
                IDENTITY.USER.LocalUser l_user = this.userProvider.GetListSQL("WHERE securityemail=@p1", email).FirstOrDefault(); // attention il faut aussi passer le partnerid
                return Task.FromResult(l_user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion






        public void Dispose()
        {
            throw new NotImplementedException();
        }






    }
}
