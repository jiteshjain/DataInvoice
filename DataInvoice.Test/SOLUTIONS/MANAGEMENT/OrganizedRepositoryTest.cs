using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataInvoice.Test.SOLUTIONS.MANAGEMENT
{
    [TestClass]
    public class OrganizedRepositoryTest
    {

        public DataInvoice.GLOBAL.DataInvoiceEnv env = new GLOBAL.DataInvoiceEnv();

        [TestMethod]
        public void CreateStandard()
        {

            DataInvoice.SOLUTIONS.GENERAL.ACCOUNT.AccountProvider accountProvide = new DataInvoice.SOLUTIONS.GENERAL.ACCOUNT.AccountProvider(env.Connector);
            DataInvoice.SOLUTIONS.GENERAL.ACCOUNT.Account account = accountProvide.GetAccount(1);


            DataInvoice.SOLUTIONS.MANAGEMENT.ORGANIZEDREPOSITORY.OrganizedRepositoryProvider repositoryProvider = new DataInvoice.SOLUTIONS.MANAGEMENT.ORGANIZEDREPOSITORY.OrganizedRepositoryProvider(env.Connector);

            DataInvoice.SOLUTIONS.MANAGEMENT.ORGANIZEDREPOSITORY.OrganizedRepository repo = repositoryProvider.Create(account, "COMPTABILITE_BANQUERELEVE", DataInvoice.SOLUTIONS.MANAGEMENT.ORGANIZEDREPOSITORY.ENUMS.RepositoryModeEnum.LOCALDIRECTORY);
            repo.FormatedDirectoryName = @"COMPTABILITE\{year}\BANQUES\";
            repo.FormatedFileName = "releve_{banquesigle}_{year}{month}.pdf";
            repo.AddCustomField("year", "Année", "{!datenow|yyyy}");
            repo.AddCustomField("month", "Mois", "{!datenow|MM}");
            repo.AddCustomField("banquesigle", "id de la banque (2caracteres)", "ce");
            repositoryProvider.SaveBubble(repo);

        }
    }
}
