using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataInvoice.SOLUTIONS.INVOICES.DOCMODEL;

namespace DataInvoice.Test.SOLUTIONS.INVOICES
{
    [TestClass]
    public class DocModelTest
    {
        public DataInvoice.GLOBAL.DataInvoiceEnv env = new GLOBAL.DataInvoiceEnv();
       
        [TestMethod]
        public void CreateTest()
        {
            DocModelProvider provider = new DocModelProvider(env.ConnectorSgbd);
            DocModel model = provider.CreateModel(new DataInvoice.SOLUTIONS.GENERAL.ACCOUNT.Account{ IDAccount=1}, 
                new DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN.Campaign{ IDCampaign=1}, "Model Test 1");
            Assert.IsNotNull(model);
        }

         [TestMethod]
        public void GetTest()
        {
              DocModelProvider provider = new DocModelProvider(env.ConnectorSgbd);
              DocModel model = provider.GetDocModel(1);
              Assert.IsNotNull(model);
        }
    }
}
