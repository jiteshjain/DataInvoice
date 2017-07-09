using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.STORE.FORM
{
    public class StoreCloudItemForm
    {
        /// <summary>
        /// Identifiant unique de l'objet
        /// </summary>
        public long IDItem { get; set; }

        /// <summary>
        /// Identifiant l'espace de stockage
        /// </summary>
        public int IDStore { get; set; }

        /// <summary>
        /// Catégory Primaire de l'objet
        /// </summary>
        public string VirtualPath { get; set; }



        /// <summary>
        /// Nom original du fichier
        /// </summary>
        public string NameFile { get; set; }



        /// <summary>
        /// Libellé du fichier (facultatif)
        /// </summary>
        public string LabelFile { get; set; }



        /// <summary>
        /// Type de l'objet  (ALR: Je me chargerai de cela si tu ne sais pas)
        /// </summary>
        public string Mime { get; set; }

        /// <summary>
        /// une clef pour retrouver l'élement dans l'openstack ou le localFiler  (varchar64)
        /// </summary>
        public string HostItemKey { get; set; }

        public StoreCloudItemForm() { }

        public StoreCloudItemForm(StoreCloudItem item)
        {
            this.IDItem = item.IDItem;
            this.IDStore = item.IDStore;
            this.LabelFile = item.LabelFile;
            this.NameFile = item.NameFile;
            this.VirtualPath = item.VirtualPath;
        }

    }
}
