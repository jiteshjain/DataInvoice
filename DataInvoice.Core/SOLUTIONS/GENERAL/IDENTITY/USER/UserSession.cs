using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER
{
    public class UserSession : NGLib.DATA.DATAPO.DataPO
    {

        public UserSession()
        {
            DefineStructRow(); // meme si vide on définie quand meme la structure pour créer les champs principaux dans le datarow
        }


        public UserSession(System.Data.DataRow row)
        {
            this.SetRow(row); //Création du datarow depuis une requette sql
        }


        /// <summary>
        /// Definir la structure de l'objet métier (clés primaires)
        /// </summary>
        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("users_sessions", new System.Data.DataColumn("UserId", typeof(int)), new System.Data.DataColumn("Token", typeof(string)));
        }


        /// <summary>
        /// Identifiant unique de l'espace de stockage
        /// </summary>
        public string Token
        {
            get { return base.GetString("Token"); }
            set { base["Token"] = value; }
        }



        /// <summary>
        /// Identifiant unique de la propriété 
        /// </summary>
        public int UserId
        {
            get { return Convert.ToInt32(base["UserId"]); }
            set { base["UserId"] = value; }
        }

    }
}
