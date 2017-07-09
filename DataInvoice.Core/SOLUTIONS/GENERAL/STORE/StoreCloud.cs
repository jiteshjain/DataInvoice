using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.STORE
{
    /// <summary>
    /// Objet qui référence un ensemble de stockage
    /// </summary>
    public class StoreCloud : NGLib.DATA.DATAPO.DataPO
    {

        public StoreCloud()
        {
            DefineStructRow(); // meme si vide on définie quand meme la structure pour créer les champs principaux dans le datarow
        }


        public StoreCloud(System.Data.DataRow row)
        {
            this.SetRow(row); //Création du datarow depuis une requette sql
        }


        /// <summary>
        /// Definir la structure de l'objet métier (clés primaires)
        /// </summary>
        protected override void DefineStructRow()
        {
            // table : StoreCloud
            // clef primaire : IDStore varchar(16)
            this.POManager().DefineRow("storecloud", new System.Data.DataColumn("idstore", typeof(int)));
        }


        /// <summary>
        /// DataValues (Flux XML) de données diverses
        /// </summary>
        public NGLib.DATA.DATAPO.DataPOFluxXML Flux
        {
            get { if (base._fluxxml == null) _fluxxml = new NGLib.DATA.DATAPO.DataPOFluxXML(this, "Fluxxml"); return _fluxxml; }
        }



        /// <summary>
        /// Identifiant unique de l'espace de stockage
        /// </summary>
        public int IDStore
        {
            get { return base.GetInt("IDStore", 0); }
            set { base["IDStore"] = value; }
        }


        /// <summary>
        /// Si le cloud est valide/accessible ou non
        /// </summary>
        public ENUMS.StoreCloudValid Valid
        {
            get { return (ENUMS.StoreCloudValid)Convert.ToInt32(this["Valid"]); }
            set { base["Valid"] = (int)value; }
        }



        /// <summary>
        /// Les catégorys liés à ce Datastore, vitualpath,object
        /// </summary>
        public List<StoreCloudDirectory> VirtualPaths { get; set; }

        public List<StoreCloudItem> Items { get; set; }





    }
}
