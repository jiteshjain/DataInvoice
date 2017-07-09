using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.CONTACT.POCO
{
    public class CompanyPoco
    {
        //siren
        public string siren { get; set; }

        //legal_form
        public string legal_form { get; set; }

        //address
        public string address { get; set; }

        //postal_code
        public string postal_code { get; set; }

        //city
        public string city { get; set; }

        //vat_number
        public string vat_number { get; set; }

        //capital
        public string capital { get; set; }

        //administration
        public string administration { get; set; }

        //activity
        public string activity { get; set; }

        //radie
        public bool radie { get; set; }

        //last_legal_update
        public DateTime last_legal_update { get; set; }

        //established_on
        public DateTime established_on { get; set; }

        //id
        public long id { get; set; }

        //names
        public NamesPoco names { get; set; }


    }
}
