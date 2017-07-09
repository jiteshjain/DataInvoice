using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.MANAGEMENT.ORGANIZEDREPOSITORY
{
    public class OrganizedRepositoryProvider : NGLib.DATA.DATAPO.DataPOProviderSQL
    {

        public OrganizedRepositoryProvider(NGLib.DATA.CONNECTOR.IDataConnector connector)
            : base(connector)
        {

        }


        public OrganizedRepository GetRepository(int IDRepository)
        {
             Dictionary<string, object> insaddr = new Dictionary<string, object>();
             insaddr.Add("IDRepository", IDRepository);
             OrganizedRepository retour = base.GetOneDefault<OrganizedRepository>(insaddr);

            return retour;
        }

        public OrganisedRepositoryResults GetRepositorys(int IDAccount)
        {
            //string sql = "SELECT * FROM campaigns where  IDAccount = @IDAccount";
            Dictionary<string, object> ins = new Dictionary<string, object>();
            if (IDAccount != 0) ins.Add("IDAccount", IDAccount);
            string sql = "SELECT * FROM organizedrepository WHERE IDAccount=@IDAccount";
            System.Data.DataTable ret = this.Connector.Query(sql, ins);
            OrganisedRepositoryResults results = new OrganisedRepositoryResults();
            results.LoadFromDataTable(ret);
            //return NGLib.DATA.DATAPO.DataPOParser.ListFromDataTable<Campaign>(ret);
            return results;

        }

        public OrganizedRepository Create(DataInvoice.SOLUTIONS.GENERAL.ACCOUNT.Account account, string RepositoryLabel, ENUMS.RepositoryModeEnum mode = ENUMS.RepositoryModeEnum.DATAINVOICECLOUD)
        {
            try
            {
                OrganizedRepository nouveau = new OrganizedRepository();
                nouveau.LabelRepository = RepositoryLabel;
                nouveau.IDAccount = account.IDAccount;
                nouveau.DateCreate = DateTime.Now;
                nouveau.RepositoryMode = mode;
                nouveau["IDRepository"] = DBNull.Value;

                // Insert
                base.InsertBubble(nouveau, false, true);

                return nouveau;
            }
            catch (Exception ex)
            {
                throw new Exception("Create Repository " + ex.Message, ex);
            }
        }


    }
}
