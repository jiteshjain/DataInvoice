using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE
{
    public class InvoiceLineResults : NGLib.DATA.DATAPO.ResultsPO<InvoiceLine>
    {

        public InvoiceLine GetLine(long idline)
        {
            foreach (InvoiceLine item in this)
                if (item.IDLine == idline)
                    return item;
            return null;
        }





        public void AddLine(InvoiceLine line)
        {
            // Si la ligne n'a pas d'ID, on lui en affecte un temporairement
            // il sera ignorer et remplacer lors de l'ajout en base
            if (line.IDLine == 0) line.IDLine = -(this.Count() + 1);

            this.Add(line);
        }

        public InvoiceLine NewLine(Invoice invoicePO)
        {
            InvoiceLine retour = new InvoiceLine();
            retour.IDInvoice = invoicePO.IDInvoice;
            retour.IDAccount = invoicePO.IDAccount;
            retour.LineTax = invoicePO.DefaultTaxeValue;
            return retour;
        }




        public double CalcSubTotalAmount()
        {
            double retour = 0;

            foreach (InvoiceLine item in this)
            {
                retour += item.GetSubTotalAmount();
            }

            return retour;
        }



        public double CalcTotalAmount()
        {
            double retour = 0;

            foreach (InvoiceLine item in this)
            {
                retour += item.GetTotalLineAmount();
            }

            return retour;
        }



        public double CalcTotalTax()
        {
            double retour = 0;

            foreach (InvoiceLine item in this)
            {
                retour += item.GetTotalLineTax();
            }

            return retour;
        }


    }
}
