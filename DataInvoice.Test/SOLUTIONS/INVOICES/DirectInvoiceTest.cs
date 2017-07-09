using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DataInvoice.SOLUTIONS.INVOICES.DIRECTINVOICE
{
    [TestClass]
    public class DirectInvoiceTest
    {
        [TestMethod]
        public void AddOutTest()
        {
            DataInvoice.GLOBAL.DataInvoiceEnv env = new GLOBAL.DataInvoiceEnv();


            DataInvoice.SOLUTIONS.INVOICES.DIRECTINVOICE.DirectInvoiceProvider directinvoiceprovide = new DataInvoice.SOLUTIONS.INVOICES.DIRECTINVOICE.DirectInvoiceProvider(env);

            DirectInvoice directinvoice = directinvoiceprovide.PrepareDirectInvoice();
            string iddirectinvoice = directinvoice.IdDirectInvoice;
            directinvoice.Invoice.InvoiceTitle = "invoice test 6sd5f";
            directinvoice.Invoice.DateInvoice = DateTime.Now.Date;
            Console.WriteLine("ID " + iddirectinvoice);
            directinvoiceprovide.SaveDirectInvoice(directinvoice);

            DirectInvoice directinvoice2 = directinvoiceprovide.GetDirectInvoice(iddirectinvoice);
                if (directinvoice2.Invoice == null) throw new Exception("obj null ...");

        }
    }
}
