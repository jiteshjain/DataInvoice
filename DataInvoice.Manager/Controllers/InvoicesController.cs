using DataInvoice.Core.COMPONENTS.WEB.MVC;
using DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN;
using DataInvoice.SOLUTIONS.INVOICES.CONTACT;
using DataInvoice.SOLUTIONS.INVOICES.INVOICE;
using DataInvoice.SOLUTIONS.INVOICES.INVOICE.FORM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Manager.Controllers
{
    [DataInvoice.COMPONENTS.WEB.MVC.MyAuthorize]
    public class InvoicesController : DataInvoiceBaseMvcController
    {

        #region Outlis et accesseur

        private InvoiceProvider invoiceProvider
        { get { if (_invoiceProvider == null)_invoiceProvider = new InvoiceProvider(this.Connector); return _invoiceProvider; } }
        private InvoiceProvider _invoiceProvider = null;


        private InvoiceManager invoiceManager
        { get { if (_invoiceManager == null)_invoiceManager = new InvoiceManager(this.Connector); return _invoiceManager; } }
        private InvoiceManager _invoiceManager = null;

        private CampaignProvider campaignProvider
        { get { if (_campaignProvider == null) _campaignProvider = new CampaignProvider(this.Connector); return _campaignProvider; } }
        private CampaignProvider _campaignProvider = null;


        private Invoice InvoiceCache
        {
            get { return (Invoice)this.Session["InvoiceCache"]; }
            set { this.Session["InvoiceCache"] = value; }
        }




        private Invoice GetInvoice(int idInvoice, int? selectedIDCampaign=null)
        {
            SOLUTIONS.GENERAL.ACCOUNT.AccountProvider accountProvider = new SOLUTIONS.GENERAL.ACCOUNT.AccountProvider(this.Connector);
            SOLUTIONS.GENERAL.ACCOUNT.Account MyAccount = accountProvider.GetAccount(this.MyUser.IDAccount);

            SOLUTIONS.INVOICES.CAMPAIGN.Campaign selectedCampaign = null;
            if (selectedIDCampaign.HasValue) selectedCampaign = campaignProvider.getCampagne(selectedIDCampaign.Value);//  camapgne par default si besoin de création
            Invoice invoice = null;
            if (idInvoice == 0) return null; // invalide
            if (idInvoice == -2) InvoiceCache = null; // Force la recréation
            if (idInvoice < 0)
            { // mode création nouvelle facture
                if (InvoiceCache == null) InvoiceCache = this.invoiceProvider.PrepareInvoice(MyAccount, selectedCampaign); 
                invoice = this.InvoiceCache;
            }
            else invoice = invoiceProvider.GetInvoice(idInvoice); // en base
            this.ViewBag.invoice = invoice;

            DataInvoice.SOLUTIONS.GENERAL.GENERATOR.DocGeneratorProvider generatorprovider = new SOLUTIONS.GENERAL.GENERATOR.DocGeneratorProvider(this.Connector);
            DataInvoice.SOLUTIONS.GENERAL.GENERATOR.DocGeneratorPO docgenerator = generatorprovider.GetDocGenerator(1);
            this.ViewBag.docgenerator = docgenerator;

            return invoice;
        }

        #endregion


        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Index(InvoiceSearchForm form)
        {
            List<Invoice> result = new List<Invoice>();
           // if (form.ActiveSearch)
                result = invoiceProvider.SearchInvoice(form);
                ViewBag.contactform = form;
                ViewBag.InvoiceSearchForm = form;
            return View(result);
        }

        public ActionResult Invoice(int idInvoice, int? selectedIDCampaign=null)
        {
            Invoice invoice = GetInvoice(idInvoice, selectedIDCampaign);
            if (invoice == null) return RedirectToAction("Index");
            if (invoice.IDInvoice < 0) return RedirectToAction("InvoiceEdition", new { idInvoice = invoice.IDInvoice });
            InvoiceCreateForm form = new InvoiceCreateForm();
            if (idInvoice > 0) form.FromPo(invoice);

            if (invoice.InvoiceState == SOLUTIONS.INVOICES.INVOICE.ENUM.InvoiceStateEnum.PREPARE) return RedirectToAction("InvoiceEdition", new { idInvoice = invoice.IDInvoice });//InvoiceEdition

            string templatenamemvc = "InvoiceTemplate1";
            ViewBag.templatenamemvc = templatenamemvc;
            
            return View(form);
        }




      
        public ActionResult InvoiceEdition(int idInvoice)
        {
            Invoice invoice = GetInvoice(idInvoice);
            if (invoice == null) return RedirectToAction("Index");
            InvoiceCreateForm form = new InvoiceCreateForm();
            if (idInvoice > 0) form.FromPo(invoice);

            string templatenamemvc = "InvoiceTemplate1";
            ViewBag.templatenamemvc = templatenamemvc;
            ViewBag.campaigns = campaignProvider.getListCampagne(this.MyUser.IDAccount);
            return View(form);
        }









        public ActionResult InvoiceCreateEdit(InvoiceCreateForm form, string submit=null)
        {
            Invoice invoice = GetInvoice(form.IDInvoice);
            if (invoice == null) return RedirectToAction("Index");



            form.ToPo(invoice);
            this.invoiceProvider.SaveFullInvoice(invoice);

              return RedirectToAction("Invoice", new { IDInvoice = invoice.IDInvoice });
        }



        [AllowAnonymous]
        public ActionResult ViewInvoice(int idInvoice)
        {
            Invoice invoice = GetInvoice(idInvoice);
            if (invoice == null) return RedirectToAction("Index");

            //ViewBag.templateview = invoice.invoi
            string templatenamemvc = "InvoiceTemplate1";
            ViewBag.templatenamemvc = templatenamemvc;

            return View(templatenamemvc, invoice);
        }







        //public ActionResult UploadFile(int idInvoice)
        //{
        //    Invoice invoice = GetInvoice(idInvoice);
        //    if (invoice == null) return RedirectToAction("Index");

        //    return View(invoice);
        //}


        [HttpPost]
        public ActionResult UploadFile(int idInvoice)
        {
            Invoice invoice = GetInvoice(idInvoice);
            if (invoice == null) return RedirectToAction("Index");
            try
            {
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase postedFile = Request.Files[upload];
                    string IDFile = invoiceProvider.AddFile(invoice, postedFile.InputStream);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Invoice", new { idInvoice = idInvoice });
        }
     

      



        #region InvoiceEditionAjax
        // !!! mettre les fonction d'édition dans un autre controlleur

        [HttpPost]
        public ActionResult UpdateAjaxInvoice(string name, string pk, string value)
        {
            Invoice invoice = GetInvoice(int.Parse(pk));
            if (invoice == null) return RedirectToAction("Index");

            InvoiceDataUpdtAjaxPoco retour = invoiceProvider.UpdateField(invoice, name, value);

            return Json(retour, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AddLine(int idInvoice)
        {
            Invoice invoice = GetInvoice(idInvoice);
            InvoiceLine line = invoice.Lines.NewLine(invoice);
            invoice.Lines.AddLine(line);
            if (idInvoice > 0) this.invoiceProvider.InsertLine(line, invoice);
           return Content(line.IDLine.ToString());
        }

        public ActionResult ModifierLogo(int idInvoice)
        {
            Invoice invoice = GetInvoice(idInvoice);
            if (invoice == null) return RedirectToAction("Index");

            return View(invoice);

        }

        public ActionResult ValiderModifierLogo(int idInvoice, string url)
        {
            Invoice invoice = GetInvoice(idInvoice);
            if (invoice == null) return RedirectToAction("Index");
            invoice.InvoiceLogo = url;
            invoiceProvider.SaveInvoice(invoice);
            return RedirectToAction("InvoiceEdition", new { idInvoice = idInvoice });

        }



        [HttpPost]
        public ActionResult DeleteLine(long idLine, int idInvoice = 0)
        {
            // !!! en cours d'évolution pour gérer les facture hors base de données
            InvoiceLine l = invoiceProvider.GetLine(idLine);

            invoiceProvider.DeleteBubble(l);
            Invoice inv = GetInvoice(l.IDInvoice);
            double somme = inv.Lines.Sum(ln => ln.LineQuantity * ln.LineAmount);
            inv.FinalAmount = somme;
            invoiceProvider.SaveBubble(inv);
            return Content("€ " + somme);
        }
        #endregion




















        public ActionResult DownloadFile(int idInvoice, bool notfilename=false)
        {
            try
            {
                Invoice invoice = GetInvoice(idInvoice);
                if (invoice == null) throw new Exception("Invoice not found");
                byte[] fileres = null;
                if (invoice.InvoiceState == SOLUTIONS.INVOICES.INVOICE.ENUM.InvoiceStateEnum.PREPARE)
                    fileres = this.invoiceManager.GenerateInvoice(invoice, true); // uniquement si user en cours
                else fileres = this.invoiceProvider.DownloadFile(invoice); 
                if (fileres == null) throw new Exception("File not found");
                if(notfilename)
                    return File(fileres, "application/pdf");
                string filename =  "invoice" + idInvoice + ".pdf";
                return File(fileres, "application/pdf", filename);
            }
            catch (Exception ex)
            {
                return Content("Facture non disponible "+ex.Message);
                //throw new Exception("DownloadFile : "+ex.Message, ex);
            }
        }













        public ActionResult InvoiceWork(int idInvoice, string work, string workvalue=null)
        {
            Invoice invoice = GetInvoice(idInvoice);
            if (invoice == null) return RedirectToAction("Index");
            try
            {
                
                if (string.IsNullOrWhiteSpace(work)) { }
                else if(work.Equals("delete"))
                {
                    invoiceProvider.DeleteFile(invoice);
                }
                else if(work.Equals("validate"))
                {
                    this.invoiceManager.GenerateInvoice(invoice, true);
                    this.invoiceProvider.SetInvoiceState(invoice, SOLUTIONS.INVOICES.INVOICE.ENUM.InvoiceStateEnum.VALIDATE);
                }

                
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Invoice", new { idInvoice = idInvoice });
        }

    }
}