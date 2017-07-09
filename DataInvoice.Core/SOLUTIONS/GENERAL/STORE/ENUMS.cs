using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.STORE.ENUMS
{
    /// <summary>
    /// Si la ged est valide ou pas
    /// </summary>
    public enum StoreCloudValid
    {
        /// <summary>
        /// Non disponible(supprimé,en cours de génération, ...)
        /// </summary>
        CANCEL = 0,
        /// <summary>
        /// Disponible
        /// </summary>
        OK = 1,
        /// <summary>
        /// Vérrouiller par un automate car action temporaire en cours (acces interdit aux utilisateur)
        /// </summary>
        LOCKED = 2,
        /// <summary>
        /// Bloqué pour une durée plus longue (acces interdit aux utilisateur)
        /// </summary>
        BLOCKED = 3
    }

}
