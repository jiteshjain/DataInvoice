using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE.HELPER
{
    public class InvoiceHelpers
    {



        /// <summary>
        /// Affichage du status du dossier
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static HtmlString ShowInvoiceStateLabel(INVOICE.Invoice invoice)
        {
            if (invoice == null) return new HtmlString(string.Empty);
            StringBuilder retour = new StringBuilder();
            try
            {
                if (invoice.IDInvoice == 0)
                {
                    retour.AppendFormat("<span class='label label-{0}' title='{2}'>{1}</span>", "default", "non enregistrée", "Vous devez enregistrer la facture");
                    return new HtmlString(retour.ToString());
                }

                string title = "";
                string color = "primary";
                string texte = "";

                if (invoice.InvoiceState == ENUM.InvoiceStateEnum.END)
                {
                    //color = "bg-green-meadow";
                    color = "success";
                    if (invoice.DatePaid.HasValue)
                    {
                        title = "Payé le " + invoice.DatePaid.Value.ToShortDateString();
                        texte = "Payée";
                    }
                    else
                    {
                        title = "Terminée";
                        texte = "Terminée";
                    }
                }
                else if (invoice.InvoiceState == ENUM.InvoiceStateEnum.CANCEL)
                {
                    color = "default";
                    title = "Facture annulée";
                    texte = "Annulée";
                }
                else if (invoice.InvoiceState == ENUM.InvoiceStateEnum.PREPARE)
                {
                    color = "info";
                    title = "Edition de la facture en cours";
                    texte = "Edition";
                }
                else if (invoice.InvoiceState == ENUM.InvoiceStateEnum.VALIDATE)
                {
                    color = "info";
                    if (false) title = "En attente de validation par tous les intervenants";
                    else title = "En attente de validation";
                    texte = "Validation";
                }
                else if (invoice.InvoiceState == ENUM.InvoiceStateEnum.SEND)
                {
                    color = "blue-dark";
                    title = string.Format("Facture envoyée, en attente du paiement"); // envoyé par quel moyen et quand? !!!

                    texte = "Envoyée";
                }
                retour.AppendFormat("<span class='label label-{0}' title='{2}'>{1}</span>", color, texte, title);

                // icone dépassement
                if (invoice.InvoiceState == ENUM.InvoiceStateEnum.SEND && invoice.DateInvoice.HasValue)
                {
                    int nbjour = 30; // payable en x jours
                    if (invoice.DateInvoice.Value.AddDays(nbjour) > DateTime.Now.Date)
                        retour.AppendFormat("<i class='text-warning fa fa-hourglass-end' title='Le délais de paiement à été dépassé ({0} jours)'></i>", nbjour);
                }



                return new HtmlString(retour.ToString());
            }
            catch (System.Exception)
            {
                return new HtmlString(string.Empty);
            }
        }






    }
}
