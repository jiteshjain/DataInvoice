using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.PAY.TRANSACTION.ENUM
{
    public enum TransactionEvolutionEnum
    {
        /// <summary>
        /// Transaction Annulé
        /// </summary>
        CANCELLED=0, 
        /// <summary>
        /// Transaction en cours d'initialisation
        /// </summary>
        INIT=1,
        /// <summary>
        /// Transaction en erreur
        /// </summary>
        ERROR=2,
        /// <summary>
        /// Transaction en attente de paiement
        /// </summary>
        WAIT=3,
        /// <summary>
        /// Transaction Payé OK
        /// </summary>
        PAYOK=4,

    }


    public enum PayModeEnum
    {
        /// <summary>
        /// Non défini
        /// </summary>
        NA,
        CREDITCARD,
        PAYPAL




    }



    public enum TransactionLogCodeEnum
    {
        LOG
    }






}
