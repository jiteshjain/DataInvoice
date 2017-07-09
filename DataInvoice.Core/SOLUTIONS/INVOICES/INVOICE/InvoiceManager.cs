using DataInvoice.SOLUTIONS.INVOICES.INVOICE.USE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE
{
    public class InvoiceManager
    {

        public DataInvoice.SOLUTIONS.GENERAL.GENERATOR.DocGeneratorProvider GeneratorProvide
        {
            get { if (_GeneratorProvide == null) _GeneratorProvide = new GENERAL.GENERATOR.DocGeneratorProvider(this.InvoiceProvide.Connector); return _GeneratorProvide; }
        }
        private DataInvoice.SOLUTIONS.GENERAL.GENERATOR.DocGeneratorProvider _GeneratorProvide = null;


        public InvoiceProvider InvoiceProvide = null;

        public InvoiceManager(InvoiceProvider InvoiceProvide)
        {
            this.InvoiceProvide = InvoiceProvide;
        }


        public InvoiceManager(NGLib.DATA.CONNECTOR.IDataConnector connector)
        {
            this.InvoiceProvide = new InvoiceProvider(connector);
        }



        /// <summary>
        /// Génération de la facture
        /// </summary>
        /// <param name="invoice"></param>
        public byte[] GenerateInvoice(Invoice invoice, bool Stockage = false)
        {
            try
            {
                DataInvoice.SOLUTIONS.GENERAL.GENERATOR.DocGeneratorPO docgeneratorpo = GeneratorProvide.GetDocGenerator(1);

                NGLib.COMPONENTS.DOCUMENT.DOCGENERATOR.IDocGeneratorItem result = GeneratorProvide.Generate(docgeneratorpo, invoice); //, new FileInfo(@"C:\TEST\res.pdf")
                if (result == null || result.ContentData == null) return null;



                if (Stockage)
                    InvoiceProvide.AddFile(invoice, result.ContentData);
                return result.ContentData;



            }
            catch (Exception ex)
            {
                throw new Exception("GenerateInvoice " + ex.Message);
            }
        }




        //public byte[] DownloadInvoice(Invoice invoice)
        //{
        //    byte[] retour  = null;

        //     Stream invoicestream = this.InvoiceProvide.DownloadFile(invoice);
        //     if (invoicestream == null)
        //     {
        //         this.GenerateInvoice(invoice, false);
        //     }
        //     else retour = ReadFully(invoicestream);




        //    return retour;

        //    //Invoice invoice = GetInvoice(idInvoice);
        //    //Stream stream = invoiceProvider.DownloadFile(invoice);
        //    //System.IO.FileInfo fileout = new System.IO.FileInfo(pathIDrtemp + "test2.pdf");

        //    //System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

        //    //watch.Stop();
        //}




        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }





        public bool AuthorizedAccess(Invoice invoice, object user)
        {
            return true;
        }




        public void SendInvoice(Invoice invoice)
        {
            try
            {
                DataInvoice.SOLUTIONS.GENERAL.COMMUNICATION.CommunicationProvider comProvider = new GENERAL.COMMUNICATION.CommunicationProvider(this.InvoiceProvide.Connector);
                var config = comProvider.GetConfig(1);
                string mail = null;
                if (invoice.BuyerAddress != null) mail = invoice.BuyerAddress.ContactMail;
                if (string.IsNullOrWhiteSpace(mail)) throw new Exception("Invoice  buyer mail not found");
                // validate mail

                System.Net.Mail.MailMessage msqg = new System.Net.Mail.MailMessage("noreply@nuegy.info", mail);
                msqg.Subject = "Invoice";
                msqg.Body = "Paiement d'une facture";

                byte[] pdf = this.InvoiceProvide.DownloadFile(invoice);

                using (Stream stream = new MemoryStream(pdf))
                {
                    NGLib.COMPONENTS.NET.MailSender.AttachFile(msqg, stream, "application/pdf", "facture.pdf");
                    var item = comProvider.CreateMail(msqg, config);
                    comProvider.SendCommunication(item);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("SendInvoice " + ex.Message, ex);
            }
        }















    }
}
