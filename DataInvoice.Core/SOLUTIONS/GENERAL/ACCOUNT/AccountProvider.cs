using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.ACCOUNT
{
    public class AccountProvider : NGLib.DATA.DATAPO.DataPOProviderSQL
    {

        public AccountProvider(NGLib.DATA.CONNECTOR.IDataConnector connector)
            : base(connector)
        {

        }

        public Account GetAccount(int idAccount)
        {
            Dictionary<string, object> insaddr = new Dictionary<string, object>();
            insaddr.Add("IDAccount", idAccount);
            Account retour = base.GetOneDefault<Account>(insaddr);

            return retour;
        }

        public List<Account> GetAllAccounts()
        {
            string sql = "SELECT * FROM accounts";
            Dictionary<string, object> ins = new Dictionary<string, object>();
            System.Data.DataTable ret = this.Connector.Query(sql, ins);
            return NGLib.DATA.DATAPO.DataPOParser.ListFromDataTable<Account>(ret);
        }

        public Account CreateAccount(string AccountName)
        {
            try
            {
                Account nouveau = new Account();
                nouveau.AccountName = AccountName;

                nouveau["IDAccount"] = DBNull.Value;

                // Insert
                base.InsertBubble(nouveau, false, true);

                return nouveau;
            }
            catch (Exception ex)
            {
                throw new Exception("CreateAccount " + ex.Message, ex);
            }
        }


    }
}
