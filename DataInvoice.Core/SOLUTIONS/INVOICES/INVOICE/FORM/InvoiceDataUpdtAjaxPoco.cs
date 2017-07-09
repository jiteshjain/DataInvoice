using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE.FORM
{
    public class InvoiceDataUpdtAjaxPoco
    {

        public int IDInvoice { get; set; }

        public string CurrencySymbol { get; set; }

        public double TotalAmount { get; set; }

        public double SubTotal { get; set; }

        public double TotalTax { get; set; }

        public LineUpdtAjaxPoco LastUpdtline { get; set; }


        public List<LineUpdtAjaxPoco> Lines { get; set; }


        public void FromPo(Invoice invoice)
        {
            this.IDInvoice = invoice.IDInvoice;

            TotalAmount = invoice.Lines.CalcTotalAmount();
            TotalTax=invoice.Lines.CalcTotalTax();
            SubTotal = invoice.Lines.CalcSubTotalAmount();
            CurrencySymbol = "€";

            //this.Lines = new List<LineUpdtAjaxPoco>();
            //foreach (var poline in invoice.Lines)
            //{
            //    LineUpdtAjaxPoco ite = new LineUpdtAjaxPoco();
            //    ite.FromPo(poline);
            //    this.Lines.Add(ite);
            //}

        }




        public class LineUpdtAjaxPoco
        {
            public long IDLine { get; set; }
            public string CurrencySymbol { get; set; }
            public double TotalLineAmount { get; set; }


            public void FromPo(InvoiceLine line)
            {
                IDLine = line.IDLine;
                TotalLineAmount = line.GetSubTotalAmount();
                CurrencySymbol = "€";
            }

        }


    }
}
