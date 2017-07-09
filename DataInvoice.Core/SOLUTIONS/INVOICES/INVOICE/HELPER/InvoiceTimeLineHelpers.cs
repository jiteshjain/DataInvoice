using System.Text;
using System.Web;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE.HELPER
{
    public static class InvoiceTimeLineHelpers
    {



        public const string itemstrformatStep = @"<div class='col-md-4 bg-grey {2} mt-step-col {1}'>
                                                <div class='mt-step-number bg-white font-grey'>{0}</div>
                                                <div class='mt-step-title uppercase font-grey-cascade'>{3}</div>
                                                <div class='mt-step-content font-grey-cascade'>{4}</div>
                                            </div>";

        public static HtmlString ShowInvoiceSteps(INVOICE.Invoice invoice)
        {
            try
            {
                StringBuilder retour = new StringBuilder();
                
                retour.Append("<div class='row step-thin'>");

               

                          
            
                retour.AppendFormat(itemstrformatStep, 1, (invoice.InvoiceState== ENUM.InvoiceStateEnum.PREPARE ? "active" : ""), (invoice.InvoiceState >= ENUM.InvoiceStateEnum.PREPARE ? "done" : ""), "PREPARATION", "Edition de la facture");
                retour.AppendFormat(itemstrformatStep, 2, (invoice.InvoiceState == ENUM.InvoiceStateEnum.VALIDATE ? "active" : ""), (invoice.InvoiceState >= ENUM.InvoiceStateEnum.VALIDATE ? "done" : ""), "VALIDATION", "Attente de validation");
                retour.AppendFormat(itemstrformatStep, 4, (invoice.InvoiceState == ENUM.InvoiceStateEnum.END ? "active" : ""), (invoice.InvoiceState >= ENUM.InvoiceStateEnum.END ? "done" : ""), "FIN", "Facture payé");

                retour.Append("</div>");

                return new HtmlString(retour.ToString());
            }
            catch (System.Exception)
            {
                return new HtmlString(string.Empty);
            }
        }




       

    }
}
