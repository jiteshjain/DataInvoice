using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.STORE
{
    // DANS UN SECOND TEMPS

    /// <summary>
    /// Il s'agit d'un répertoire virtuel pour organiser les fichiers
    /// </summary>
    public class StoreCloudDirectory : NGLib.DATA.DATAPO.DataPO
    {

        public StoreCloudDirectory()
        {
            DefineStructRow(); // meme si vide on définie quand meme la structure pour créer les champs principaux dans le datarow
        }


        public StoreCloudDirectory(System.Data.DataRow row)
        {
            this.SetRow(row); //Création du datarow depuis une requette sql
        }


        /// <summary>
        /// Definir la structure de l'objet métier (clés primaires)
        /// </summary>
        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("StoreCloud_VirtalPaths", new System.Data.DataColumn("IDStore", typeof(string)), new System.Data.DataColumn("VirtualPath", typeof(string)));
        }



        /// <summary>
        /// Identifiant unique de l'espace de stockage
        /// </summary>
        public string IDStore
        {
            get { return base.GetString("IDStore"); }
            set { base["IDStore"] = value; }
        }

        /// <summary>
        /// Chemin virtuel de la catégorie dans la ged  ( / = primaryPath). Champ unique par StoreCloud
        /// Exemple /FICHIERS/Travaux   
        /// </summary>
        public string VirtualPath
        {
            get { return base.GetString("VirtualPath"); }
            set { base["VirtualPath"] = value; }
        }

    }
}
