using DataInvoice.Core.COMPONENTS.WEB.MVC;
using DataInvoice.SOLUTIONS.GENERAL.ACCOUNT;
using DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN;
using DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN.FORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Manager.Controllers
{
    [DataInvoice.COMPONENTS.WEB.MVC.MyAuthorize]
    public class CampaignsController : DataInvoiceBaseMvcController
    {
        private CampaignProvider CampaignProvider
        { get { if (_CampaignProvider == null)_CampaignProvider = new CampaignProvider(this.Connector); return _CampaignProvider; } }
        private CampaignProvider _CampaignProvider = null;
        private AccountProvider AccountProvider
        { get { if (_AccountProvider == null) _AccountProvider = new AccountProvider(this.Connector); return _AccountProvider; } }
        private AccountProvider _AccountProvider = null;
        private DataInvoice.SOLUTIONS.INVOICES.CONTACT.AddressProvider AddressProvider
        { get { if (_AddressProvider == null) _AddressProvider = new DataInvoice.SOLUTIONS.INVOICES.CONTACT.AddressProvider(this.Connector); return _AddressProvider; } }
        private DataInvoice.SOLUTIONS.INVOICES.CONTACT.AddressProvider _AddressProvider = null;

        public Campaign GetCampagne(int idCampaign)
        {
            if (idCampaign == 0) return null;
            if (idCampaign == -1) return new Campaign();
            Campaign campaign = CampaignProvider.getCampagne(idCampaign);
            ViewBag.campaign = campaign;
            return campaign;
        }




        // GET: Campaign
        public ActionResult Index()
        {
            List<Campaign> result = new List<Campaign>();
            result = CampaignProvider.getListCampagne(this.MyUser.IDAccount);
            ViewBag.idAccount = this.MyUser.IDAccount;
           
            return View(result);
        }



        public ActionResult ModalList()
        {
            List<Campaign> result = new List<Campaign>();
            result = CampaignProvider.getListCampagne(this.MyUser.IDAccount);
            ViewBag.idAccount = this.MyUser.IDAccount;

            return View(result);
        }


        public ActionResult Campaign(int idCampaign)
        {
            Campaign campagne = this.GetCampagne(idCampaign);
            if (campagne == null) { return RedirectToAction("Index"); }
            return View(campagne);
        }

        [HttpGet]
        public ActionResult Config(int idCampaign)
        {
            Campaign campagne = this.GetCampagne(idCampaign);
            if (campagne == null) { return RedirectToAction("Index"); }

            return View(campagne);
        }

        [HttpPost]
        public ActionResult Config(int idCampaign, string Title)
        {
            Campaign campagne = this.GetCampagne(idCampaign);
            if (campagne == null) { return RedirectToAction("Index"); }
            campagne.Title = Title;
            this.CampaignProvider.UpdateCampagne(campagne);

            return RedirectToAction("Config", new { IDCampaign = idCampaign });
        }


        [HttpGet]
        public ActionResult DefaultConfig(int idCampaign)
        {
            Campaign campagne = this.GetCampagne(idCampaign);
            if (campagne == null) { return RedirectToAction("Index"); }

            return View(campagne);
        }

        [HttpPost]
        public ActionResult DefaultConfig(DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN.Campaign form) // !!! NE PAS METTRE UN PO DIRECTEMENT EN TEMPS QUE FORMULAIRE .... , A revoir proprement ...
        {
            Campaign campagne = this.GetCampagne(form.IDCampaign);
            if (campagne == null) { return RedirectToAction("Index"); }
            campagne.DefaultLogoUrl = form.DefaultLogoUrl;           
            var adrForm = new SOLUTIONS.INVOICES.CONTACT.FORM.AddressForm();
            adrForm.FromPo(form.AddresseBuyerDefault);
            int idAdr =(int) this.AddressProvider.CreateAddress(adrForm).IDAddress;
            campagne.IdAddresseBuyerDefault= idAdr;
            adrForm.FromPo(form.AddresseSellerDefault);
            idAdr = (int)this.AddressProvider.CreateAddress(adrForm).IDAddress;
            campagne.IdAddresseSellerDefault= idAdr;

            this.CampaignProvider.UpdateCampagne(campagne);
            return RedirectToAction("DefaultConfig", new { IDCampaign = form.IDCampaign });
        }





    }
}