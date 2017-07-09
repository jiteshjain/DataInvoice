using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataInvoice.SOLUTIONS.INVOICES.CONTACT
{
    public class Contact : NGLib.DATA.DATAPO.DataPO, CONTACT.FORM.IWithAddress
    {

        public Contact()
        {
            DefineStructRow(); // meme si vide on définie quand meme la structure pour créer les champs principaux dans le datarow
        }


        public Contact(System.Data.DataRow row)
        {
            this.SetRow(row); //Création du datarow depuis une requette sql
        }


        /// <summary>
        /// Definir la structure de l'objet métier (clés primaires)
        /// </summary>
        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("contacts", new System.Data.DataColumn("IDContact", typeof(int)));
        }


        /// <summary>
        /// DataValues (Flux XML) de données diverses
        /// </summary>
        public NGLib.DATA.DATAPO.DataPOFluxXML Flux
        {
            get { if (base._fluxxml == null)_fluxxml = new NGLib.DATA.DATAPO.DataPOFluxXML(this, "Fluxxml"); return _fluxxml; }
        }


        /// <summary>
        /// Identifiant unique de la propriété 
        /// </summary>
        public int IDContact
        {
            get { return Convert.ToInt32(base["IDContact"]); }
            set { base["IDContact"] = value; }
        }






        public bool DedicatedAddressPrimary
        {
            get { return this.GetBoolean("DedicatedAddressPrimary",false, NGLib.DATA.BASICS.DataAccessorOptionEnum.None); }
            set { base["DedicatedAddressPrimary"] = value; }
        }
        /// <summary>
        /// Addresse principale du contact
        /// </summary>
        public int? IDAddressPrimary
        {
            get { object obj = this["IDAddressPrimary"]; if (obj == DBNull.Value)return null; else return Convert.ToInt32(obj); }
            set { base["IDAddressPrimary"] = value.Value; }
        }
        /// <summary>
        /// Adresse
        /// </summary>
        public CONTACT.Address AddressPrimary { get; set; }



        public int? IDUser
        {
            get { object obj = this["IDUser"]; if (obj == DBNull.Value)return null; else return Convert.ToInt32(obj); }
            set { base["IDUser"] = value.Value; }
        }







        /// <summary>
        /// Civilité
        /// </summary>
        public string Civility
        {
            get { return this.GetString("Civility"); }
            set { base["Civility"] = value; }
        }

        /// <summary>
        /// Nom
        /// </summary>
        public string LastName
        {
            get { return this.GetString("LastName"); }
            set { base["LastName"] = value; }
        }

        /// <summary>
        /// Prénom
        /// </summary>
        public string FirstName
        {
            get { return this.GetString("FirstName"); }
            set { base["FirstName"] = value; }
        }


        /// <summary>
        /// Mail
        /// </summary>
        public string Mail
        {
            get { return this.GetString("Mail"); }
            set { base["Mail"] = value; }
        }


        /// <summary>
        /// Phone
        /// </summary>
        public string Phone
        {
            get { return this.GetString("Phone"); }
            set { base["Phone"] = value; }
        }


        /// <summary>
        /// MobilePhone
        /// </summary>
        public string MobilePhone
        {
            get { return this.GetString("MobilePhone"); }
            set { base["MobilePhone"] = value; }
        }

        /// <summary>
        /// Identifiant isolation syndics
        /// </summary>
        public string IDCentity
        {
            get { return base.GetString("IDCentity"); }
            set { base["IDCentity"] = value; }
        }





        public string GetIdentity()
        {
            if (string.IsNullOrWhiteSpace(this.Civility)) return this.LastName + " " + this.FirstName;
            else return this.Civility+" "+this.LastName + " " + this.FirstName;
        }






 
    }
}
