using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.ACCOUNT
{
    public class Account : NGLib.DATA.DATAPO.DataPO
    {

        public Account()
        {
            this.DefineStructRow();
        }

        public Account(System.Data.DataRow row)
        {
            this.SetRow(row);
        }

        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("accounts", new System.Data.DataColumn("IDAccount", typeof(int)));
        }


        public int IDAccount
        {
            get { return this.GetInt("IDAccount", 0); }
            set { this["IDAccount"] = value; }
        }


        // string AccountName
        public string AccountName 
        {
            get { return this.GetString("AccountName"); }
            set { this["AccountName"] = value; }
        }


        // CompanyNumber = Siren 
        //public string CompanyNumber


        // public string CountryCode

      
        // public long IDAdress

        // Il s'agit de l'adresse principal du compte
        // public CONTACT.Address Address {get; set;}




    }
}
