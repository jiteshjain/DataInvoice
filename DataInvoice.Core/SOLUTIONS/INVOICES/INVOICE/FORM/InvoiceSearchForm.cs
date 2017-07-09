using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE.FORM
{
    public class InvoiceSearchForm
    {
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

        public long CustomerIDAddress { get; set; }

        public DataInvoice.SOLUTIONS.INVOICES.CONTACT.Address CustomerAddress { get; set; }

        // string CustomerRef
        public string CustomerRef { get; set; }

        public long ProviderIDAddress { get; set; }

        public DataInvoice.SOLUTIONS.INVOICES.CONTACT.Address ProviderAddress { get; set; }

        // string ProviderRef
        public string ProviderRef { get; set; }

        //public bool ActiveSearch { get; set; }
        


  




    }
}
