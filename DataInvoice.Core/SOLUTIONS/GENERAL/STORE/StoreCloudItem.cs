using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.STORE
{
    /// <summary>
    /// Objet qui référence un élément/fichier/Document stocké dans le cloud
    /// </summary>
    public class StoreCloudItem : NGLib.DATA.DATAPO.DataPO
    {



        public StoreCloudItem()
        {
            DefineStructRow(); // meme si vide on définie quand meme la structure pour créer les champs principaux dans le datarow
        }


        public StoreCloudItem(System.Data.DataRow row)
        {
            this.SetRow(row); //Création du datarow depuis une requette sql
        }


        /// <summary>
        /// Definir la structure de l'objet métier (clés primaires)
        /// </summary>
        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("storecloud_items", new System.Data.DataColumn("iditem", typeof(long)));
        }


        /// <summary>
        /// Identifiant unique de l'objet
        /// </summary>
        public long IDItem
        {
            get { return Convert.ToInt64(this["IDItem"]); }
            set { base["IDItem"] = value; }
        }



        /// <summary>
        /// Identifiant l'espace de stockage
        /// </summary>
        public int IDStore
        {
            get { return base.GetInt("IDStore", 0); }
            set { base["IDStore"] = value; }
        }

        /// <summary>
        /// Catégory Primaire de l'objet
        /// </summary>
        public string VirtualPath
        {
            get { return base.GetString("VirtualPath"); }
            set { base["VirtualPath"] = value; }
        }



        /// <summary>
        /// Nom original du fichier
        /// </summary>
        public string NameFile
        {
            get { return base.GetString("NameFile"); }
            set { base["NameFile"] = value; }
        }



        /// <summary>
        /// Libellé du fichier (facultatif)
        /// </summary>
        public string LabelFile
        {
            get { return base.GetString("LabelFile"); }
            set { base["LabelFile"] = value; }
        }




        /// <summary>
        /// Type de l'objet  (ALR: Je me chargerai de cela si tu ne sais pas)
        /// </summary>
        public string Mime
        {
            get { return base.GetString("Mime"); }
            set { base["Mime"] = value; }
        }

        /// <summary>
        /// Type de l'objet  (ALR: Je me chargerai de cela si tu ne sais pas)
        /// </summary>
        public string IDPhysicalFile
        {
            get { return base.GetString("idphysicalfile"); }
            set { base["idphysicalfile"] = value; }
        }










        /// <summary>
        /// une clef pour retrouver l'élement dans l'openstack ou le localFiler  (varchar64)
        /// </summary>
        public string HostItemKey
        {
            get { return base.GetString("HostItemKey"); }
            set { base["HostItemKey"] = value; }
        }



        /// <summary>
        /// Hash md5 du fichier
        /// !!! CHAMPS A AJOUTER DANS LA BASE
        /// </summary>
        public string Hash
        {
            get { return base.GetString("Hash"); }
            set { base["Hash"] = value; }
        }


        /// <summary>
        /// fichier scellé, en lecture seule (Ne devra plus etre modifié)
        /// !!! CHAMPS A AJOUTER DANS LA BASE
        /// </summary>
        public bool ItemSealed
        {
            get { return base.GetBoolean("ItemSealed", false); }
            set { base["ItemSealed"] = value; }
        }




    }
}
