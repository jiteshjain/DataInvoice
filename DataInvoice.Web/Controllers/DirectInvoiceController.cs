using DataInvoice.SOLUTIONS.INVOICES.INVOICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Web.Controllers
{
    public class DirectInvoiceController : Controller
    {

        //public const int IDAccountDirect = 1;
        //public const int IDCampaignDirect = 1;



        protected NGLib.DATA.CONNECTOR.IDataConnector Connector
        //{ get { if (_Connector == null) _Connector = DataInvoice.Core.SOLUTIONS.GENERAL.ACCES.CoreAcces.GetTestConnector(); return _Connector; } }
        { get { if (_Connector == null) _Connector = NGLib.COMPONENTS.APP.AppCore.GetDataConnector(); return _Connector; } }
        private NGLib.DATA.CONNECTOR.IDataConnector _Connector = null;

        private InvoiceProvider invoiceProvider
        { get { if (_invoiceProvider == null) _invoiceProvider = new InvoiceProvider(this.Connector); return _invoiceProvider; } }
        private InvoiceProvider _invoiceProvider = null;


        private InvoiceManager invoiceManager
        { get { if (_invoiceManager == null) _invoiceManager = new InvoiceManager(this.Connector); return _invoiceManager; } }
        private InvoiceManager _invoiceManager = null;

 


        private Invoice InvoiceCache
        {
            get { return (Invoice)this.Session["directinvoice"]; }
            set { this.Session["directinvoice"] = value; }
        }

        private void CreateInvoice()
        {

        }

        private Invoice GetInvoice()
        {
            Invoice retour = null;
            if (this.InvoiceCache != null)
                retour = InvoiceCache;

            if(retour==null)
            {
                retour = new Invoice();
                retour.IDInvoice = -1;
                retour.IDAccount = -1;
                this.InvoiceCache= retour;
            }

            this.ViewBag.invoice = retour;
            return retour;
        }




        // GET: DirectInvoice
        public ActionResult Index()
        {
            return RedirectToAction("InvoiceEdition");
        }




        [AllowAnonymous]
        public ActionResult ViewInvoice(int idInvoice)
        {
            Invoice invoice = GetInvoice();
            if (invoice == null) return RedirectToAction("Index");

            //ViewBag.templateview = invoice.invoi
            string templatenamemvc = "InvoiceTemplate1";
            ViewBag.templatenamemvc = templatenamemvc;

            return View(templatenamemvc, invoice);
        }



        public ActionResult InvoiceEdition()
        {
            Invoice invoice = GetInvoice();
            return View();
        }



        public ActionResult DownloadFile(bool notfilename = false)
        {
            try
            {
                Invoice invoice = GetInvoice();
                if (invoice == null) throw new Exception("Invoice not found");
                byte[] fileres = null;
                if (invoice.InvoiceState == SOLUTIONS.INVOICES.INVOICE.ENUM.InvoiceStateEnum.PREPARE)
                    fileres = this.invoiceManager.GenerateInvoice(invoice, true); // uniquement si user en cours
                else fileres = this.invoiceProvider.DownloadFile(invoice);
                if (fileres == null) throw new Exception("File not found");
                if (notfilename)
                    return File(fileres, "application/pdf");
                string filename = "datainvoice" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
                return File(fileres, "application/pdf", filename);
            }
            catch (Exception ex)
            {
                return Content("Facture non disponible " + ex.Message);
                //throw new Exception("DownloadFile : "+ex.Message, ex);
            }
        }














        #region InvoiceEditionAjax
        // !!! mettre les fonction d'édition dans un autre controlleur

        [HttpPost]
        public ActionResult UpdateAjaxInvoice(string name, string pk, string value)
        {
            Invoice invoice = GetInvoice();
            if (invoice == null) return RedirectToAction("Index");

            SOLUTIONS.INVOICES.INVOICE.FORM.InvoiceDataUpdtAjaxPoco retour = invoiceProvider.UpdateField(invoice, name, value);

            return Json(retour, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AddLine()
        {
            Invoice invoice = GetInvoice();
            InvoiceLine line = invoice.Lines.NewLine(invoice);
            invoice.Lines.AddLine(line);
            return Content(line.IDLine.ToString());
        }

        public ActionResult ModifierLogo()
        {
            Invoice invoice = GetInvoice();
            if (invoice == null) return RedirectToAction("Index");

            return View(invoice);

        }

        public ActionResult ValiderModifierLogo(string url)
        {
            Invoice invoice = GetInvoice();
            if (invoice == null) return RedirectToAction("Index");
            invoice.InvoiceLogo = url;
            //invoiceProvider.SaveInvoice(invoice);
            return RedirectToAction("InvoiceEdition", new {  });

        }



        [HttpPost]
        public ActionResult DeleteLine(long idLine)
        {
            // !!! en cours d'évolution pour gérer les facture hors base de données
            InvoiceLine l = invoiceProvider.GetLine(idLine);

            invoiceProvider.DeleteBubble(l);
            Invoice inv = GetInvoice();
            double somme = inv.Lines.Sum(ln => ln.LineQuantity * ln.LineAmount);
            inv.FinalAmount = somme;
            invoiceProvider.SaveBubble(inv);
            return Content("€ " + somme);
        }
        #endregion


























    }
}