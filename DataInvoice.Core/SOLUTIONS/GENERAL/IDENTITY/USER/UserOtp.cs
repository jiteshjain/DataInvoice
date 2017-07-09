using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER
{
    public class UserOtp: NGLib.DATA.DATAPO.DataPO
    {

       
        public UserOtp()
        {
            DefineStructRow(); // meme si vide on définie quand meme la structure pour créer les champs principaux dans le datarow
        }


        public UserOtp(System.Data.DataRow row)
        {
            this.SetRow(row); //Création du datarow depuis une requette sql
        }


        /// <summary>
        /// Definir la structure de l'objet métier (clés primaires)
        /// </summary>
        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("users_otp", new System.Data.DataColumn("IDUser", typeof(int)), new System.Data.DataColumn("OtpKey", typeof(string)));
        }



        public int IDUser
        {
            get { return Convert.ToInt32(base["IDUser"]); }
            set { base["IDUser"] = value; }
        }



        public string OtpKey
        {
            get { return base.GetString("OtpKey"); }
            set { base["OtpKey"] = value; }
        }


        public string OtpMode
        {
            get { return base.GetString("OtpMode"); }
            set { base["OtpMode"] = value; }
        }

        public string Sender
        {
            get { return base.GetString("Sender"); }
            set { base["Sender"] = value; }
        }

        public string RedirectUrl
        {
            get { return base.GetString("RedirectUrl"); }
            set { base["RedirectUrl"] = value; }
        }


        public DateTime DateCreate
        {
            get { return Convert.ToDateTime(base["DateCreate"]); }
            set { base["DateCreate"] = value; }
        }

        public DateTime DateLimit
        {
            get { return Convert.ToDateTime(base["DateLimit"]); }
            set { base["DateLimit"] = value; }
        }



        public bool ValidateOtp(string OtpTest)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.OtpKey)) return false;
                if (string.IsNullOrWhiteSpace(OtpTest)) return false;
                OtpTest = OtpTest.Trim();
                if (!OtpTest.Equals(this.OtpKey)) return false;
                if (this.DateCreate > DateTime.Now) return false;
                if (this.DateLimit < DateTime.Now) return false;
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
