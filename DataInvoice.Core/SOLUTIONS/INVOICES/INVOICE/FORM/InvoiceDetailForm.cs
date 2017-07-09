using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE.FORM
{
    public class InvoiceDetailForm
    {





        public string RefSeller { get; set; }
        public string RefBuyer { get; set; }


        public string InvoiceComment { get; set; }






        public void ToPo(INVOICE.Invoice invoicepo)
        {
            invoicepo.RefSeller = this.RefSeller;
            invoicepo.RefBuyer = this.RefBuyer;
            invoicepo.Flux.SetString("/param/data/Comment", this.InvoiceComment);

        }

        public void FromPo(INVOICE.Invoice invoicepo)
        {
            this.RefSeller = invoicepo.RefSeller;
            this.RefBuyer = invoicepo.RefBuyer;
            this.InvoiceComment = invoicepo.Flux.GetString("/param/data/Comment", NGLib.DATA.BASICS.DataAccessorOptionEnum.Nullable);
        }




    }
}
