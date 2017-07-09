using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE.ENUM
{
    public enum InvoiceStateEnum
    {
        /// <summary>
        /// Annulée
        /// </summary>
        CANCEL,



        /// <summary>
        /// En cours de préparation (attentes des données nécessaires)
        /// </summary>
        PREPARE,



        /// <summary>
        /// En attente de validation (par tous les intervenants)
        /// </summary>
        VALIDATE,



        /// <summary>
        /// Envoyé
        /// </summary>
        SEND,



        ///// <summary>
        ///// Attente du paiement de la facture
        ///// </summary>
        //PAYMENT,



        /// <summary>
        /// workflow terminé
        /// </summary>
        END
    }



    public enum InvoiceTypeEnum
    {
        NA=0,
        INVOICE,
        ORDERFORM,
        REPAYMENT,

    }


    public enum ContactInvoiceTypeEnum
    {
        /// <summary>
        /// Client/Acheteur
        /// </summary>
        BUYER,
        /// <summary>
        /// Fournisseur/Vendeur
        /// </summary>
        SELLER
    }




}
