using DataInvoice.Core.SOLUTIONS.INVOICES.DOCMODEL.FORM;
using DataInvoice.SOLUTIONS.INVOICES.DOCMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Api.Controllers
{
    public class DocumentModelsController : BaseApiController
    {
        private DocModelProvider _DocModelProvider;
        public DocModelProvider DocModelProvider
        {
            get { if (_DocModelProvider == null) _DocModelProvider = new DocModelProvider(Connector); return _DocModelProvider; }
        }
        //
        // GET: /DocumentModels/
        public ActionResult Get(string id)
        {
            DocModel docModel = DocModelProvider.GetDocModel(int.Parse(id));
            DocModelApiPoco retour = new DocModelApiPoco(docModel);
            return Json(retour, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Index()
        {
            return Content("api");
        }
	}
}