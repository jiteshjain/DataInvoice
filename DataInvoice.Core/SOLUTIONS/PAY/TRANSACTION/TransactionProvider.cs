using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.PAY.TRANSACTION
{
    public class TransactionProvider
    {







        // Transaction GetTransaction(long IDTransaction, bool complete=true)
        // complete : load Account , Campaign,  logs


        public TransactionResults GetLastTransactions(int IDAccount, int? IDCampaign = null, ENUM.TransactionEvolutionEnum? Evolution = null, int CountResult = 30)
        {
            //!!!
            System.Data.DataTable ret = null;
            TransactionResults retour = new TransactionResults();
            retour.LoadFromDataTable(ret);
            return null;
        }





        public TransactionResults SearchTransactions(POCO.TransactionSearchForm form)
        {

            // !!!
            return null;
        }



        public void InsertTransaction(Transaction transaction)
        {
            // !!!
        }

        public void SaveTransaction(Transaction transaction)
        {
            // !!!
        }


        public bool CreateLog(Transaction transaction, string Message, int level = 1)
        {
            try
            {
                // !!!
                return true;
            }
            catch (Exception)
            { return false; }
        }

    }
}
