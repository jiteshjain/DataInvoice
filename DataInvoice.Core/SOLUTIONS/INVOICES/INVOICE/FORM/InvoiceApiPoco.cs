using DataInvoice.SOLUTIONS.INVOICES.INVOICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.Core.SOLUTIONS.INVOICES.FORM
{
   public class InvoiceApiPoco
    {
        public int IDAccount { get; set; }

        public int IDCampaign { get; set; }

        public int IDInvoice { get; set; }

        public string InvoiceTitle { get; set; }

        public DateTime? DateCreate { get; set; }

        public DateTime? DateValidate { get; set; }

        public DateTime? DateSend { get; set; }

        // DateAcq
        public DateTime? DateAcq { get; set; }

        // DatePaid
        public DateTime? DatePaid { get; set; }

        // InvoiceState (DataInvoice.SOLUTIONS.INVOICES.INVOICE.ENUM)
        public DataInvoice.SOLUTIONS.INVOICES.INVOICE.ENUM.InvoiceStateEnum InvoiceState { get; set; }

        // InvoiceType
        public string InvoiceType { get; set; }

        // DateInvoice (date sans les heures)
        public DateTime? DateInvoice { get; set; }

        // RefInvoice (varchar 48)
        public string RefInvoice { get; set; }

        // Userfield001 (varchar 64)
        public string Userfield001 { get; set; }

        // double FinalAmount {}    (Montant final de la facture)
        public Double FinalAmount { get; set; }

        public DataInvoice.SOLUTIONS.INVOICES.CONTACT.FORM.AddressForm BuyerAddress { get; set; }

        // string CustomerRef
        public string CustomerRef { get; set; }

        public DataInvoice.SOLUTIONS.INVOICES.CONTACT.FORM.AddressForm SellerAddress { get; set; }

        // string ProviderRef
        public string ProviderRef { get; set; }




        public InvoiceApiPoco() {}
        public InvoiceApiPoco(Invoice invoice) { this.FromPo(invoice); }

       public void FromPo(Invoice invoice)
       {
           if (invoice == null) return;
           this.IDInvoice = invoice.IDInvoice;
           this.IDAccount = invoice.IDAccount;
           this.IDCampaign = invoice.IDCampaign;

           // !!! faire la suite !!!!!!!!!!!!!!!!!!!!!!!


       }









    }
}
