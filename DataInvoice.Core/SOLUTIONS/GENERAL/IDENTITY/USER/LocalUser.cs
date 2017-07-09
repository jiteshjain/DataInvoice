using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.ENUMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER
{
    public class LocalUser : Identity.IdentityUser
    {

        #region Constructeur et Clefs

        public LocalUser()
        {
            DefineStructRow(); // meme si vide on définie quand meme la structure pour créer les champs principaux dans le datarow
        }


        public LocalUser(System.Data.DataRow row)
        {
            this.SetRow(row); //Création du datarow depuis une requette sql
        }


        /// <summary>
        /// Definir la structure de l'objet métier (clés primaires)
        /// </summary>
        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("identity_users", new System.Data.DataColumn("UserId", typeof(int)));
        }


        /// <summary>
        /// DataValues (Flux XML) de données diverses
        /// </summary>
        public NGLib.DATA.DATAPO.DataPOFluxXML Flux
        {
            get { if (base._fluxxml == null) _fluxxml = new NGLib.DATA.DATAPO.DataPOFluxXML(this, "Fluxxml"); return _fluxxml; }
        }


        /// <summary>
        /// Identifiant unique de la propriété 
        /// </summary>
        public int UserId
        {
            get { return Convert.ToInt32(base["UserId"]); }
            set { base["UserId"] = value; }
        }


        /// <summary>
        /// Compte de facturation (CLEF ISOLATION)
        /// </summary>
        public int IDAccount
        {
            get { return this.GetInt("IDAccount"); }
            set { base["IDAccount"] = value; }
        }



        #endregion



        #region Accesseurs Informations
        // --------------------------------------------------------------------

        /// <summary>
        /// Attaché à un contact
        /// </summary>
        public int IDContact
        {
            get { return Convert.ToInt32(base["IDContact"]); }
            set { base["IDContact"] = value; }
        }




        public string UserName
        {
            get { return base.GetString("UserName"); }
            set { base["UserName"] = value; }
        }

        public string Password
        {
            get { return base.GetString("Password"); }
            set { base["Password"] = value; }
        }

        public string PasswordHash
        {
            get { return base.GetString("passwordhash"); }
            set { base["passwordhash"] = value; }
        }

        public string Pseudo
        {
            get { return base.GetString("Pseudo"); }
            set { base["Pseudo"] = value; }
        }


        public string SecurityMail
        {
            get { return base.GetString("SecurityMail"); }
            set { base["SecurityMail"] = value; }
        }

        public string SecurityPhone
        {
            get { return base.GetString("SecurityPhone"); }
            set { base["SecurityPhone"] = value; }
        }


        //public int? DefaultIDAssociation
        //{
        //    get { return base.GetInt("DefaultIDAssociation", NGLib.DATA.BASICS.DataAccessorOptionEnum.None, false); }
        //    set { base["DefaultIDAssociation"] = value; }
        //}

        //public int? DefaultIDProperty
        //{
        //    get { return base.GetInt("DefaultIDProperty", NGLib.DATA.BASICS.DataAccessorOptionEnum.None, false); }
        //    set { base["DefaultIDProperty"] = value; }
        //}

        // DateCreation
        // LastUpdate
        // LastConnection
        //







        /// <summary>
        /// Niveau d'accréditation de l'utilisateur
        /// </summary>
        [Obsolete("temporaire")]
        public UserLevelEnum UserLevel
        {
            get { object obj = this["UserLevel"]; if (obj == DBNull.Value) return UserLevelEnum.DISABLED; else return (UserLevelEnum)Convert.ToInt32(obj); }
            set { base["UserLevel"] = (int)value; }
        }



        // --------------------------------------------------------------------
        #endregion



        #region region SECURITE


        //public UserSession CurrentSession { get; set; }



        public string Name
        {
            get { return this.UserName; }
        }

        public string AuthenticationType
        {
            get { return "AUTH"; }
        }

        public bool IsAuthenticated
        {
            get { return _IsAuthenticated; }
        }
        internal bool _IsAuthenticated = false; // Attention sécurité

        public bool TokenExiste
        {
            get { return _TokenExiste; }
        }
        internal bool _TokenExiste = false; // Attention sécurité


        public Identity.IdentityUser Identity
        {
            get { return this; }
        }

        public int Id { get { return this.UserId; } }

        /// <summary>
        /// STANDARD,SYNDIC,ADMIN
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            if (role == null) role = "";

           

            //// Implémentation simple des roles pour le moment
            //if (role.Equals("ADMIN", StringComparison.InvariantCultureIgnoreCase) && this.Claims.Contains((Int32)UserLevelEnum.ADMIN) return true;
            //else if (role.Equals("SYNDIC", StringComparison.InvariantCultureIgnoreCase) && this.UserLevel >= UserLevelEnum.SYNDICMANAGER) return true;
            //else if (role.Equals("STANDARD", StringComparison.InvariantCultureIgnoreCase) && this.UserLevel >= UserLevelEnum.STANDARD) return true;
            //else if (string.IsNullOrWhiteSpace(role) && this.UserLevel >= UserLevelEnum.STANDARD) return true;
            //else return false;
            return true;
        }


        #endregion



        #region Divers

        public override string ToString()
        {
            return string.Format("[{0}]{1}", this.GetString("UserId"), this.GetString("UserName"));
        }

        #endregion



    }
}
