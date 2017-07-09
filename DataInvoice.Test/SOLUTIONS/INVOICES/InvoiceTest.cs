using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataInvoice.SOLUTIONS.INVOICES.INVOICE;

namespace DataInvoice.Test.SOLUTIONS.INVOICES
{
    [TestClass]
    public class InvoiceTest
    {
       public DataInvoice.GLOBAL.DataInvoiceEnv env = new GLOBAL.DataInvoiceEnv();


        [TestMethod]
        public void getinvoicetest()
        {
            DataInvoice.SOLUTIONS.INVOICES.INVOICE.InvoiceProvider invoiceprovide = new DataInvoice.SOLUTIONS.INVOICES.INVOICE.InvoiceProvider(env.ConnectorSgbd);
            DataInvoice.SOLUTIONS.INVOICES.INVOICE.Invoice invoice = null;
            invoice = invoiceprovide.GetInvoice(1);
            Console.WriteLine(invoice.InvoiceTitle);

            InvoiceResults  invoices = invoiceprovide.SearchInvoice(new DataInvoice.SOLUTIONS.INVOICES.INVOICE.FORM.InvoiceSearchForm());
            foreach (var invoiceitem in invoices)
            {
                Console.WriteLine(invoiceitem.ToString());
            }

        }


        [TestMethod]
        public void sendinvoice()
        {
            DataInvoice.SOLUTIONS.INVOICES.INVOICE.InvoiceProvider invoiceprovide = new DataInvoice.SOLUTIONS.INVOICES.INVOICE.InvoiceProvider(env.ConnectorSgbd);
            DataInvoice.SOLUTIONS.INVOICES.INVOICE.InvoiceManager invoiceManager = new DataInvoice.SOLUTIONS.INVOICES.INVOICE.InvoiceManager(invoiceprovide);

            DataInvoice.SOLUTIONS.INVOICES.INVOICE.Invoice invoice = null;
            invoice = invoiceprovide.GetInvoice(1);

            invoiceManager.SendInvoice(invoice);

            Console.WriteLine(invoice.InvoiceTitle);

        }



        //[TestMethod]
        //public void createInvoiceTest()
        //{
        //    DataInvoice.SOLUTIONS.INVOICES.INVOICE.InvoiceProvider invoiceprovide = new DataInvoice.SOLUTIONS.INVOICES.INVOICE.InvoiceProvider(env.ConnectorSgbd);
        //  Invoice invoice =  invoiceprovide.CreateInvoice(new DataInvoice.SOLUTIONS.INVOICES.INVOICE.FORM.InvoiceCreateForm
        //    {
        //        IDAccount = 1,
        //        IDCampaign = 1,
        //        CustomerIDAddress = 1,
        //        ProviderIDAddress = 1,
        //        CustomerRef = "Ref cust test 1",
        //        DateInvoice = DateTime.Now,
        //        InvoiceState = DataInvoice.SOLUTIONS.INVOICES.INVOICE.ENUM.InvoiceStateEnum.PREPARE,
        //        InvoiceTitle = "Title invoice 1",
        //        InvoiceType = "Type 1",
        //         FinalAmount=100.5
        //    });
        //  Assert.IsNotNull(invoice);
        //}
    }
}
