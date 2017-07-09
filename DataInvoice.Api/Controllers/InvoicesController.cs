using DataInvoice.Core.SOLUTIONS.INVOICES;
using DataInvoice.Core.SOLUTIONS.INVOICES.FORM;
using DataInvoice.SOLUTIONS.INVOICES.INVOICE;
using DataInvoice.SOLUTIONS.INVOICES.INVOICE.ENUM;
using DataInvoice.SOLUTIONS.INVOICES.INVOICE.FORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataInvoice.Api.Controllers
{
    public class InvoicesController : BaseApiController
    {

        private InvoiceProvider _invoiceProvider;
        public InvoiceProvider invoiceProvider
        {
            get { if (_invoiceProvider == null) _invoiceProvider = new InvoiceProvider(Connector); return _invoiceProvider; }
        }

        // GET: /Invoices/
        public ActionResult Index()
        {
            return Content("api");
        }

        public ActionResult Get(string id)
        {
            Invoice invoice = invoiceProvider.GetInvoice(int.Parse(id));
            InvoiceApiPoco retour = new InvoiceApiPoco();
            retour.FromPo(invoice);
            return Json(retour, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAll()
        {
            InvoiceSearchForm form = new InvoiceSearchForm();
            List<Invoice> invoices = invoiceProvider.SearchInvoice(form);
            List<InvoiceApiPoco> retour = new List<InvoiceApiPoco>();
            invoices.ForEach(x => retour.Add(new InvoiceApiPoco(x)));
            return Json(retour, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(InvoiceSearchForm form)
        {
           List<Invoice> invoices = invoiceProvider.SearchInvoice(form);
           List<InvoiceApiPoco> retour = new List<InvoiceApiPoco>();
           invoices.ForEach(x =>  retour.Add(new InvoiceApiPoco(x)));
            return Json(retour, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(InvoiceCreateForm InvoiceCreateForm)
        {
            Invoice invoice = invoiceProvider.CreateInvoice(InvoiceCreateForm);
            InvoiceApiPoco retour = new InvoiceApiPoco(invoice);
            return Json(retour);
        }

        public ActionResult ChangeState(Invoice invoice, InvoiceStateEnum state)
        {
            Invoice invoic = invoiceProvider.ChangeState(invoice, state);
            InvoiceApiPoco retour = new InvoiceApiPoco(invoic);
            return Json(retour, JsonRequestBehavior.AllowGet);
        }


	}
}