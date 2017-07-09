using DataInvoice.Core.COMPONENTS.WEB.MVC;
using DataInvoice.Core.SOLUTIONS.INVOICES.DOCMODEL.FORM;
using DataInvoice.SOLUTIONS.GENERAL.ACCOUNT;
using DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN;
using DataInvoice.SOLUTIONS.INVOICES.DOCMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Manager.Controllers
{
    [DataInvoice.COMPONENTS.WEB.MVC.MyAuthorize]
    public class ModelController : DataInvoiceBaseMvcController
    {
        private DocModelProvider DocModelProvider
        { get { if (_DocModelProvider == null)_DocModelProvider = new DocModelProvider(this.Connector); return _DocModelProvider; } }
        private DocModelProvider _DocModelProvider = null;
         private AccountProvider AccountProvider
        { get { if (_AccountProvider == null) _AccountProvider = new AccountProvider(this.Connector); return _AccountProvider; } }
        private AccountProvider _AccountProvider = null;
         private CampaignProvider CampaignProvider
        { get { if (_CampaignProvider == null)_CampaignProvider = new CampaignProvider(this.Connector); return _CampaignProvider; } }
        private CampaignProvider _CampaignProvider = null;

        // GET: Model
        public ActionResult Index(  DocModelApiPoco  form)
        {
            List<DocModel> result = new List<DocModel>();
            result = DocModelProvider.GetDocModels(form);
            //ViewBag.idAccount = form;
            List<Account> lstAccount = new List<Account>();
            lstAccount.Add(new Account{ IDAccount= 0, AccountName=""});
            lstAccount.AddRange(AccountProvider.GetAllAccounts());
            ViewBag.Accounts = lstAccount;
            List<Campaign> lstCampaign = new List<Campaign>();
            lstCampaign.Add(new Campaign { IDCampaign = 0, Title = "" });
            lstCampaign.AddRange(CampaignProvider.getListCampagne(0));
            ViewBag.campaigns = lstCampaign;
            return View(result);
        }

         public ActionResult CreateEdit(int IDModel)
        {
            DocModel form;
            if (IDModel != 0)
                form = DocModelProvider.GetDocModel(IDModel);
            else   form = new DocModel();
            ViewBag.Accounts = AccountProvider.GetAllAccounts();
            ViewBag.campaigns = CampaignProvider.getListCampagne(0);
            return View(form);
        }

        public ActionResult ValidateCreateEdit(DocModelApiPoco form)
        {
            if (form.IDModel == 0)
                DocModelProvider.CreateModel(AccountProvider.GetAccount(form.IDAccount),
                     CampaignProvider.getCampagne(form.IDCampaign.Value), form.Title);
            else {
                DocModel model = new DocModel();
                model.FromObject(form);
                //Problème avec FromObject à voir
                model.IDModel = form.IDModel;
                model.IDCampaign = form.IDCampaign;
                DocModelProvider.UpdateModel(model);
            }
            return RedirectToAction("Index", new { form = new DataInvoice.Core.SOLUTIONS.INVOICES.DOCMODEL.FORM.DocModelApiPoco() });
        }
    }

    }
