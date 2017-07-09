using DataInvoice.SOLUTIONS.GENERAL.ACCOUNT;
using DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN;
using DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN.FORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Api.Controllers
{
    public class CampaignsController : BaseApiController
    {
        private CampaignProvider _campaignProvider;
        public CampaignProvider campaignProvider
        {
            get { if (_campaignProvider == null) _campaignProvider = new CampaignProvider(Connector); return _campaignProvider; }           
        }
        private AccountProvider _accountProvider;
        public AccountProvider accountProvider
        {
            get { if (_accountProvider == null) _accountProvider = new AccountProvider(Connector); return _accountProvider; }
        }

        //
        // GET: /Campaigns/
        public ActionResult Index()
        {
            return Content("api");
        }



        public ActionResult Create(CampaignApiPoco form)
        {
           Account account = accountProvider.GetAccount(form.IDAccount);
           Campaign campaign = campaignProvider.CreateCampagne(account, form.Title);
            return Json(new CampaignApiPoco(campaign));
        }

        public ActionResult Get(string id)
        {
            Campaign campaign = campaignProvider.getCampagne(int.Parse(id));
            CampaignApiPoco retour = new CampaignApiPoco(campaign);
            return Json(retour, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Set(string id, CampaignApiPoco form)
        {
            campaignProvider.UpdateCampagne(int.Parse(id), form);
            return Content("OK");
        }



	}
}