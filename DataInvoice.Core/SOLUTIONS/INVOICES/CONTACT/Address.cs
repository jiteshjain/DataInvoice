using DataInvoice.SOLUTIONS.INVOICES.CONTACT.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.CONTACT
{
    public class Address   : NGLib.DATA.DATAPO.DataPO
    {

        public Address()
        {
            this.DefineStructRow();
        }

        public Address(System.Data.DataRow row)
        {
            this.SetRow(row);
        }

        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("contacts_address", new System.Data.DataColumn("IDAddress", typeof(long)));
        }


        public int IDAccount
        {
            get { return this.GetInt("IDAccount", 0); }
            set { this["IDAccount"] = value; }
        }

        public long IDAddress
        {
            get { return this.GetLong("IDAddress", 0); }
            set { this["IDAddress"] = value; }
        }





        // Identity
        public string Identity
        {
            get { return this.GetString("Identity"); }
            set { this["Identity"] = value; }
        }

        // Compagny
        public string Compagny
        {
            get { return this.GetString("Compagny"); }
            set { this["Compagny"] = value; }
        }
        // CompagnyNumber
        public int CompagnyNumber
        {
            get { return this.GetInt("CompagnyNumber", 0); }
            set { this["CompagnyNumber"] = value; }
        }
        // CompagnylegalNotice
        public string CompagnylegalNotice
        {
            get { return this.GetString("CompagnylegalNotice"); }
            set { this["CompagnylegalNotice"] = value; }
        }


        // Adress1
        public string Adress1 // corriger !!! address1
        {
            get { return this.GetString("Adress1"); }
            set { this["Adress1"] = value; }
        }

        // Adress2
        public string Adress2
        {
            get { return this.GetString("Adress2"); }
            set { this["Adress2"] = value; }
        }

        // Adress3
        public string Adress3
        {
            get { return this.GetString("Adress3"); }
            set { this["Adress3"] = value; }
        }

        // Postcode
        public string Postcode
        {
            get { return this.GetString("Postcode"); }
            set { this["Postcode"] = value; }
        }

        // City
        public string City
        {
            get { return this.GetString("City"); }
            set { this["City"] = value; }
        }

        // Country
        public string Country
        {
            get { return this.GetString("Country"); }
            set { this["Country"] = value; }
        }

        // ContactIdentity
        public string ContactIdentity
        {
            get { return this.GetString("ContactIdentity"); }
            set { this["ContactIdentity"] = value; }
        }
        // ContactMail
        public string ContactMail
        {
            get { return this.GetString("ContactMail"); }
            set { this["ContactMail"] = value; }
        }
        // ContactPhone
        public string ContactPhone
        {
            get { return this.GetString("ContactPhone"); }
            set { this["ContactPhone"] = value; }
        }

        public string GetIdentity()
        {
            return this.Identity;
        }


    }
}
