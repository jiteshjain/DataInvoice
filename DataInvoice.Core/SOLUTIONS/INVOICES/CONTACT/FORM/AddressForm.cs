using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.CONTACT.FORM
{
   public  class AddressForm
    {

       public int IDAccount { get; set; }

       public long IDAddress { get; set; }

        // Identity
       public string Identity { get; set; }

        // Compagny
       public string Compagny { get; set; }

        // CompagnyNumber
       public int CompagnyNumber { get; set; } // !!! Corriger la faute

        // CompagnylegalNotice
       public string CompagnylegalNotice { get; set; } // !!! corriger la faute !

        // Adress1
       public string Adress1 { get; set; } // !!! corriger la faute !

        // Adress2
       public string Adress2 { get; set; } // !!! corriger la faute !

        // Adress3
       public string Adress3 { get; set; } // !!! corriger la faute !

        // Postcode
       public string Postcode { get; set; }

        // City
       public string City { get; set; }

        // Country
       public string Country { get; set; }

        // ContactIdentity
       public string ContactIdentity { get; set; }

        // ContactMail
       public string ContactMail { get; set; }

        // ContactPhone
       public string ContactPhone { get; set; }





       public void ToPo(CONTACT.Address addresspo, bool allowWriteSecureFields=false)
       {
           if (addresspo == null) addresspo = new Address();
           if (allowWriteSecureFields)
           {
               addresspo.IDAccount = this.IDAccount;
               addresspo.IDAddress = this.IDAddress;
           }

           addresspo.Identity = this.Identity;
           addresspo.Compagny = this.Compagny;
           addresspo.CompagnyNumber = this.CompagnyNumber;
           addresspo.CompagnylegalNotice = this.CompagnylegalNotice;
           addresspo.Adress1 = this.Adress1;
           addresspo.Adress2 = this.Adress2;
           addresspo.Adress3 = this.Adress3;
           addresspo.Postcode = this.Postcode;
           addresspo.City = this.City;
           addresspo.Country = this.Country;
           addresspo.ContactIdentity = this.ContactIdentity;
           addresspo.ContactMail = this.ContactMail;
           addresspo.ContactPhone = this.ContactPhone;

       }

       public void FromPo(CONTACT.Address addresspo)
       {
           if (addresspo == null) return;

           this.IDAccount = addresspo.IDAccount;
           this.IDAddress = addresspo.IDAddress;
           this.Identity = addresspo.Identity;
           this.Compagny = addresspo.Compagny;
           this.CompagnyNumber = addresspo.CompagnyNumber;
           this.CompagnylegalNotice = addresspo.CompagnylegalNotice;
           this.Adress1 = addresspo.Adress1;
           this.Adress2 = addresspo.Adress2;
           this.Adress3 = addresspo.Adress3;
           this.Postcode = addresspo.Postcode;
           this.City = addresspo.City;
           this.Country = addresspo.Country;
           this.ContactIdentity = addresspo.ContactIdentity;
           this.ContactMail = addresspo.ContactMail;
           this.ContactPhone = addresspo.ContactPhone;
       }





       public void FromPo(INVOICE.Invoice invoicepo, INVOICE.ENUM.ContactInvoiceTypeEnum ContactType)
       {
           if (ContactType == INVOICE.ENUM.ContactInvoiceTypeEnum.BUYER)
               this.FromPo(invoicepo.BuyerAddress);
           else if (ContactType == INVOICE.ENUM.ContactInvoiceTypeEnum.SELLER)
               this.FromPo(invoicepo.SellerAddress);

       }

       public void ToPo(INVOICE.Invoice invoicepo, INVOICE.ENUM.ContactInvoiceTypeEnum ContactType)
       {
           if (ContactType == INVOICE.ENUM.ContactInvoiceTypeEnum.BUYER)
               ToPo(invoicepo.BuyerAddress);
           else if (ContactType == INVOICE.ENUM.ContactInvoiceTypeEnum.SELLER)
               ToPo(invoicepo.SellerAddress);
       }


    }
}
