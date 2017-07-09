using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.STORE.FORM
{
    public class StoreCloudSearchForm
    {
        public StoreCloudSearchForm()
        {
            // par default
            this.CountResults = 25;
        }

        /// <summary>
        /// Obligatoire
        /// </summary>
        public int IDStore { get; set; }

        /// <summary>
        /// Nombre de résultats
        /// </summary>
        public int CountResults { get; set; }


        /// <summary>
        /// Recherche avec le titre du document (LIKE sql)
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Catégorie précise
        /// </summary>
        public string CategoryLabel { get; set; }

        /// <summary>
        /// Path précise
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// Paths
        /// </summary>
        public List<string> paths { get; set; }

        /// <summary>
        /// Fichiers précises
        /// </summary>
        public List<StoreCloudItemForm> Files { get; set; }



    }
}