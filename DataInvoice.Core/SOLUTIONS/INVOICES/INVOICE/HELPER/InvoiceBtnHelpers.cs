using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE.HELPER
{
    public class InvoiceBtnHelpers
    {




        /// <summary>
        /// Affichage du bouton status suivant
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static HtmlString ShowBtnNextStep(HtmlHelper helper, INVOICE.Invoice invoice)
        {
            if (invoice == null) return new HtmlString(string.Empty);
            StringBuilder retour = new StringBuilder();
            try
            {
                UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
                if (invoice.IDInvoice < 0) // la facture n'est pas encore créer en base
                {
                    retour.Append("<a class='btn btn-primary' type='submit' name='submit' value='create'><i class='fa fa-save'></i> Créer la facture</a>");
                    return new HtmlString(retour.ToString());
                }

                if (invoice.InvoiceState == ENUM.InvoiceStateEnum.PREPARE) // passer automatiquement à envoyer si pas besoin de la valider
                {
                    //if(true) //
                    retour.AppendFormat("<a class='btn btn-primary' href='{0}' title='La facture sera scellé, il ne sera plus possible de la modifier'><i class='fa fa-check-circle-o'></i> Valider la facture</a>", urlHelper.Action("invoicework", "invoices", new { invoice.IDInvoice, work = "validate" }));
                }
                else if (invoice.InvoiceState == ENUM.InvoiceStateEnum.VALIDATE)
                {
                    retour.Append("<div class='btn-group'>");
                    retour.AppendFormat("<a href='{0}' class='btn btn-primary btn-sm'><i class='fa fa-send-o'></i> Envoyer la facture</a>", urlHelper.Action("invoicework", "invoices", new { invoice.IDInvoice, work = "send" })); // !!!choix par default dans la campagne
                    retour.Append("<a type='button' class='btn btn-primary dropdown-toggle btn-sm' data-toggle='dropdown' aria-expanded='false'>&nbsp;<i class='fa fa-angle-down'></i></a>");
                    retour.Append("<ul class='dropdown-menu' role='menu'>");
                    retour.AppendFormat("<li><a href = '{0}' >Envoyer la facture par Mail</a></li>", urlHelper.Action("invoicework", "invoices", new { invoice.IDInvoice, work = "send", workvalue = "mail" }));
                    retour.AppendFormat("<li><a href = '{0}' disabled>Envoyer la facture par Courrier postal</a></li>", urlHelper.Action("invoicework", "invoices", new { invoice.IDInvoice, work = "send", workvalue = "letter" }));
                    retour.AppendFormat("<li><a href = '{0}' >Simplement valider, je vais l'envoyer moi même</a></li>", urlHelper.Action("invoicework", "invoices", new { invoice.IDInvoice, work = "send", workvalue = "self" }));
                    retour.Append("</ul></div>");
                }
                else if (invoice.InvoiceState == ENUM.InvoiceStateEnum.SEND)
                {
                    //!!! déclenchement d'une popup, proposant les choix
                    // - la  facture à été payé (+ input date de paiment , avec la date du jour prérenseigné)
                    // - annuler la facture (la facture n'a pas été payé)
                    retour.Append("<button class='btn btn-primary btn-block' type='submit' name='submit' value='save'><i class='fa fa-gavel'></i> Cloturer la facture</button>");
                }




                return new HtmlString(retour.ToString());
            }
            catch (System.Exception)
            {
                return new HtmlString(string.Empty);
            }
        }



        /// <summary>
        /// bouton d'actions diverses
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static HtmlString ShowBtnActions(HtmlHelper helper, INVOICE.Invoice invoice)
        {
            if (invoice == null) return new HtmlString(string.Empty);
            StringBuilder retour = new StringBuilder();
            try
            {
                UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
                retour.Append("<div class='btn-group'>");
                retour.Append("<a class='btn btn-circle btn-default btn-sm' href='javascript:;' style='margin-left:3px;' data-toggle='dropdown' aria-expanded='false'>Actions<i class='fa fa-angle-down'></i></a>");
                retour.Append("<ul class='dropdown-menu pull-right'>");

                retour.AppendFormat("<li><a href = '{0}' target='_blank'><i class='fa fa-eye'></i> Visualiser</a></li>", urlHelper.Action("ViewInvoice", "Invoices", new { IDInvoice = invoice.IDInvoice }));
                retour.Append("<li class='divider'> </li>");


                if (true) // on peut pas toujours la supprimer
                {
                    retour.AppendFormat("<li><a href = '{0}' title='Delete'><i style = 'color:red;' class='fa fa-remove'></i> Supprimer</a></li>", urlHelper.Action("DeleteFile", "Invoices", new { IDInvoice = invoice.IDInvoice }));
                }

                retour.Append("</ul></div>");


                return new HtmlString(retour.ToString());
            }
            catch (System.Exception)
            {
                return new HtmlString(string.Empty);
            }
        }



        /// <summary>
        /// bouton d'actions diverses
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static HtmlString ShowBtnDownload(HtmlHelper helper, INVOICE.Invoice invoice)
        {
            if (invoice == null) return new HtmlString(string.Empty);
            StringBuilder retour = new StringBuilder();
            try
            {
                UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                if (invoice.IDInvoice > 0)
                    retour.AppendFormat("<a href='{1}' class='btn btn-circle btn-default btn-sm'><i class='fa fa-download'></i> {0}</a>",
                       "Télécharger la facture", urlHelper.Action("DownloadFile", "Invoices", new { IDInvoice = invoice.IDInvoice })); //DataInvoice.Resources.Resources.downloadFile





                return new HtmlString(retour.ToString());
            }
            catch (System.Exception)
            {
                return new HtmlString(string.Empty);
            }
        }



        /// <summary>
        /// bouton d'actions diverses
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static HtmlString ShowBtnOpen(HtmlHelper helper, INVOICE.Invoice invoice)
        {
            if (invoice == null) return new HtmlString(string.Empty);
            StringBuilder retour = new StringBuilder();
            try
            {
                UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                if (invoice.InvoiceState == ENUM.InvoiceStateEnum.PREPARE)
                    retour.AppendFormat("<a href='{1}' class='btn btn-primary btn-sm'><i class='fa fa-edit'></i> {0}</a>",
                     "Modifier", urlHelper.Action("Invoice", "Invoices", new { IDInvoice = invoice.IDInvoice }));

                                   
                    else
                    retour.AppendFormat("<a href='{1}' class='btn btn-default btn-sm'><i class='fa fa-sticky-note'></i> {0}</a>",
                       "Consulter", urlHelper.Action("DownloadFile", "Invoices", new { IDInvoice = invoice.IDInvoice })); 





                return new HtmlString(retour.ToString());
            }
            catch (System.Exception)
            {
                return new HtmlString(string.Empty);
            }
        }

    }
}
