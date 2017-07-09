using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.PAY.TRANSACTION
{
    /// <summary>
    /// Paiement
    /// </summary>
    public class Transaction : NGLib.DATA.DATAPO.DataPO
    {



        // long IDTransaction   //primarykey            (not null)
        // int IDAccount        //cloisonement client   (not null)
        // int IDCampaign       // relié à une campagne (not null)

        // string TransactionKey
        // string TransactionLabel

        // TransactionEvolutionEnum Evolution // (tinyint)
        // string TransactionStatus
        // string TransactionStatusText


        // PayModeEnum PayMode (varchar16)



        // datetime DateCreate
        // datetime DateTreatment // tjr mettre sans les heures: value.Date;
        // datetime? DatePay

        // string ImportOrigin
        // string ImportKey

        // long? IDInvoice //si relié à une facture

        // double Amount
        // 




        // string BIC
        // string IBAN



        public TRANSACTION.TransactionLogResults Logs { get; set; }
        public INVOICES.CAMPAIGN.Campaign Campaign { get; set; }
        public SOLUTIONS.GENERAL.ACCOUNT.Account Account { get; set; }


    }
}
