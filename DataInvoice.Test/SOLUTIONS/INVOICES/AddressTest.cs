using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataInvoice.Test.SOLUTIONS.INVOICES
{
    [TestClass]
    public class AddressTest
    {
        public DataInvoice.GLOBAL.DataInvoiceEnv env = new GLOBAL.DataInvoiceEnv();

        //    [TestMethod]
        //public void CreateAdresssetest()
        //{
        //    DataInvoice.SOLUTIONS.INVOICES.ADDRESS.AddressProvider adresseProvide = new DataInvoice.SOLUTIONS.INVOICES.ADDRESS.AddressProvider(env.ConnectorSgbd);
        //    DataInvoice.SOLUTIONS.INVOICES.ADDRESS.Address   adresse = adresseProvide.CreateAddress(
        //        new Core.SOLUTIONS.INVOICES.ADDRESS.FORM.AddressForm
        //        {
        //            Adress1 = "adr",
        //            City = "cty",
        //            Compagny = "cmp",
        //            ContactMail = "mail@yo.com",
        //            Country = "ctry",
        //            Postcode = "5100"
        //        });
        //    Assert.IsNotNull(adresse);

        //}

        //    [TestMethod]
        //    public void GetAdresssetest()
        //    {
        //        DataInvoice.SOLUTIONS.INVOICES.ADDRESS.AddressProvider adresseProvide = new DataInvoice.SOLUTIONS.INVOICES.ADDRESS.AddressProvider(env.ConnectorSgbd);
        //        DataInvoice.SOLUTIONS.INVOICES.ADDRESS.Address adresse = adresseProvide.GetAddress(1);
        //        Assert.IsNotNull(adresse);
        //    }

        //    [TestMethod]
        //    public void UpdateAdresssetest()
        //    {
        //        DataInvoice.SOLUTIONS.INVOICES.ADDRESS.AddressProvider adresseProvide = new DataInvoice.SOLUTIONS.INVOICES.ADDRESS.AddressProvider(env.ConnectorSgbd);
        //      DataInvoice.SOLUTIONS.INVOICES.ADDRESS.Address adresseOrigine = adresseProvide.GetAddress(1);
        //      DataInvoice.SOLUTIONS.INVOICES.ADDRESS.Address adresse = adresseProvide.UpdateAdresse(adresseOrigine, new Core.SOLUTIONS.INVOICES.ADDRESS.FORM.AddressForm
        //      {
        //          Adress1 = "adr1",
        //          City = "cty1",
        //          Compagny = "cmp",
        //          ContactMail = "mail@yo.com",
        //          Country = "ctry",
        //          Postcode = "5100"
        //      });
        //      Assert.IsTrue(adresse.Adress1 == "adr1");
        //      Assert.IsTrue(adresse.City == "cty1");
        //    }

        [TestMethod]
        public void EnrishAdressFromSirenTest()
        {
            DataInvoice.SOLUTIONS.INVOICES.CONTACT.AddressProvider adresseProvide = new DataInvoice.SOLUTIONS.INVOICES.CONTACT.AddressProvider(env.ConnectorSgbd);
            DataInvoice.SOLUTIONS.INVOICES.CONTACT.Address adresseOrigine = adresseProvide.GetAddress(1);
            //enrechir l'adress a partir du JSON retourné de l'API
            bool result = false;
            result=adresseProvide.EnrishAdressFromSiren(adresseOrigine, "535050611", false);
            Console.WriteLine(string.Format("ADRESS[{0}] => {1} : {2}", adresseOrigine.IDAddress, result, adresseOrigine.Adress1));
        }
    }
}
