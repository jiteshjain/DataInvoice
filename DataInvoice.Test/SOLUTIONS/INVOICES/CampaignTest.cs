using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN;

namespace DataInvoice.Test.SOLUTIONS.INVOICES
{
    [TestClass]
    public class CampaignTest
    {
        public DataInvoice.GLOBAL.DataInvoiceEnv env = new GLOBAL.DataInvoiceEnv();
       
        [TestMethod]
        public void CreateTest()
        {
           CampaignProvider provider = new CampaignProvider(env.ConnectorSgbd);
           Campaign campaign = provider.CreateCampagne(new DataInvoice.SOLUTIONS.GENERAL.ACCOUNT.Account { IDAccount = 1 }, "Campagne test 1");
           Assert.IsNotNull(campaign);
        }

        [TestMethod]
        public void GetTest()
        {
            CampaignProvider provider = new CampaignProvider(env.ConnectorSgbd);
            Campaign campaign = provider.getCampagne(1);
            Assert.IsNotNull(campaign);
        }

    }
}
