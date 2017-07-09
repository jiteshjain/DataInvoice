using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.CONTACT.FORM
{
   public  class ContactForm
    {
        public int IDContact{ get;  set; }


        public AddressForm Address { get; set; }



        /// <summary>
        /// syndic
        /// </summary>
        public string IDCEntity { get; set; }

        /// <summary>
        /// Civilité
        /// </summary>
        public string Civility { get;  set; }
  

        /// <summary>
        /// Nom
        /// </summary>
        public string LastName { get;  set; }

        /// <summary>
        /// Prénom
        /// </summary>
        public string FirstName { get;  set; }


        /// <summary>
        /// Mail
        /// </summary>
        public string Mail { get;  set; }


        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get;  set; }


        /// <summary>
        /// MobilePhone
        /// </summary>
        public string MobilePhone { get; set; }

        public Boolean ActiveSearch { get; set; }    



       public string GetIdentity()
        {
            string retour = "";
            if (!string.IsNullOrWhiteSpace(LastName)) retour += LastName;
            if (!string.IsNullOrWhiteSpace(FirstName)) retour += " " + FirstName;
            if (string.IsNullOrWhiteSpace(retour)) return Mail;
            return retour;
        }





    }
}
