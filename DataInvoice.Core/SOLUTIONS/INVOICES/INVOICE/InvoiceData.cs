using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE
{
    
    /// <summary>
    /// Il s'agit d'une extention du PO Invoice avec des accesseurs pour l'accès au données
    /// </summary>
    public class InvoiceData 
    {
        private Invoice invoice;
        public InvoiceData(Invoice invoicebase)
        { this.invoice = invoicebase; }



        public bool editablemode
        {
            get { return this.invoice.Flux.IsTrue("/param/config/editablemode"); }
            set { this.invoice.Flux.SetObject("/param/config/editablemode", value); }
        }



    }
}
