using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.STORE
{
    /// <summary>
    /// Outils
    /// </summary>
    public static class StoreCloudTools
    {

        /// <summary>
        /// Corrige les petite erreur lors de la saisie du répertoire virtuel
        /// Si pas possible returne une erreur
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public static string CorrectVirtualPath(string virtualPath)
        {
            return NGLib.DATA.FORMAT.StringUtilities.removeDiacritics(virtualPath);

        }
    }
}
