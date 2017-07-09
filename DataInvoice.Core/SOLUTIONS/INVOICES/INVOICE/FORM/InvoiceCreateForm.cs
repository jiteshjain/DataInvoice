using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE.FORM
{
    public class InvoiceCreateForm
    {
       public int IDCampaign { get; set; }

        public int IDInvoice { get; set; }





        // RefInvoice (varchar 48)
        public string RefInvoice { get; set; }
        public string InvoiceTitle { get; set; }

        // InvoiceType
        public ENUM.InvoiceTypeEnum InvoiceType { get; set; } // !!! mettre enum



        // DateInvoice (date sans les heures)
        public DateTime? DateInvoice { get; set; }



        // Userfield001 (varchar 64)
        public string Userfield001 { get; set; }

        // double FinalAmount {}    (Montant final de la facture)
        public Double FinalAmount { get; set; }




        public DataInvoice.SOLUTIONS.INVOICES.CONTACT.FORM.AddressForm BuyerAddress { get; set; }
        public DataInvoice.SOLUTIONS.INVOICES.CONTACT.FORM.AddressForm SellerAddress { get; set; }

        public DataInvoice.SOLUTIONS.INVOICES.INVOICE.FORM.InvoiceDetailForm Details { get; set; }
      





        public void FromPo(Invoice invoicepo)
        {
            if (invoicepo == null) return;

            //invoicepo.FromObject(this); // !!! ne pas utiliser cette méthode, faire la champs 1 par 1, c'est mieu

            this.IDInvoice = invoicepo.IDInvoice;
            this.IDCampaign = invoicepo.IDCampaign;
            this.RefInvoice = invoicepo.RefInvoice;
            this.InvoiceTitle = invoicepo.InvoiceTitle;
            this.InvoiceType = invoicepo.InvoiceType;
            this.DateInvoice = invoicepo.DateInvoice;
            this.Userfield001 = invoicepo.Userfield001;

            if (this.Details == null) Details = new InvoiceDetailForm();
            this.Details.FromPo(invoicepo);

            if (this.BuyerAddress == null) this.BuyerAddress = new DataInvoice.SOLUTIONS.INVOICES.CONTACT.FORM.AddressForm();
            this.BuyerAddress.FromPo(invoicepo, ENUM.ContactInvoiceTypeEnum.BUYER);

            if (this.SellerAddress == null) this.SellerAddress = new DataInvoice.SOLUTIONS.INVOICES.CONTACT.FORM.AddressForm();
            this.SellerAddress.FromPo(invoicepo,  ENUM.ContactInvoiceTypeEnum.SELLER);

        }


        public void ToPo(Invoice invoicepo, bool allowWriteSecureFields = false)
        {

            if (allowWriteSecureFields)
            {
                invoicepo.IDCampaign = this.IDCampaign;
                invoicepo.IDInvoice = this.IDInvoice;
            }



            invoicepo.IDInvoice = this.IDInvoice;
            invoicepo.IDCampaign = this.IDCampaign;
            invoicepo.RefInvoice = this.RefInvoice;
            invoicepo.InvoiceTitle = this.InvoiceTitle;
            invoicepo.InvoiceType = this.InvoiceType;
            invoicepo.DateInvoice = this.DateInvoice;
            invoicepo.Userfield001 = this.Userfield001;



            if (this.Details != null) this.Details.ToPo(invoicepo);
            if (this.BuyerAddress != null) this.BuyerAddress.ToPo(invoicepo, ENUM.ContactInvoiceTypeEnum.BUYER);
            if (this.SellerAddress != null) this.SellerAddress.ToPo(invoicepo, ENUM.ContactInvoiceTypeEnum.SELLER);

        }








    }
}
