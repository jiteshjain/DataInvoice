using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HiQPdf;

namespace DataInvoice.Test.SOLUTIONS.INVOICES
{
    [TestClass]
    public class GeneratorInvoice
    {

        public static string pathIDrtemp = @"C:\TEST\datainvoice\";


        [TestMethod]
        public void GeneratePDF()
        {
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf(); 
             byte[] pdfBuffer = null; 
            string url = "http://www.docapost.fr/";
            string keylicence = "HFR1TUx4-elB1fm59-bmUtLDIs-PC08LTwr-KC08Ly0y-LS4yJSUl-JQ==";
            htmlToPdfConverter.SerialNumber = keylicence;
            htmlToPdfConverter.ConvertUrlToFile(url, @"C:\TEST\gotest.pdf");
        }


        [TestMethod]
        public void GenerateInvoice()
        {

            DataInvoice.GLOBAL.DataInvoiceEnv env = new GLOBAL.DataInvoiceEnv();


            DataInvoice.SOLUTIONS.GENERAL.GENERATOR.DocGeneratorProvider generatorprovider = new DataInvoice.SOLUTIONS.GENERAL.GENERATOR.DocGeneratorProvider(env.Connector);
            DataInvoice.SOLUTIONS.INVOICES.INVOICE.InvoiceProvider invoiceprovider = new DataInvoice.SOLUTIONS.INVOICES.INVOICE.InvoiceProvider(env.Connector);

            DataInvoice.SOLUTIONS.GENERAL.GENERATOR.DocGeneratorPO po = generatorprovider.GetDocGenerator(1);
            DataInvoice.SOLUTIONS.INVOICES.INVOICE.Invoice invoice = invoiceprovider.GetInvoice(1);

            Console.WriteLine("MODEL GENERATOR "+po.UniqueLabel);
            Console.WriteLine("INVOICE "+invoice.InvoiceTitle);

            System.IO.FileInfo fileout = new System.IO.FileInfo(pathIDrtemp + "test2.pdf");

            System.Diagnostics.Stopwatch watch =  System.Diagnostics.Stopwatch.StartNew();
            generatorprovider.Generate(po, invoice, fileout);
            watch.Stop();


            Console.WriteLine("watch " + watch.ElapsedMilliseconds);




        }
    }
}
