using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.GENERAL.IDENTITY.ENTITY
{
    /// <summary>
    /// Entité d'isolation qui représente un syndic
    /// </summary>
    public class Entity : NGLib.DATA.DATAPO.DataPO, IEntity
    {


         public Entity()
        {
            DefineStructRow(); // meme si vide on définie quand meme la structure pour créer les champs principaux dans le datarow
        }


         public Entity(System.Data.DataRow row)
        {
            this.SetRow(row); //Création du datarow depuis une requette sql
        }


        /// <summary>
        /// Definir la structure de l'objet métier (clés primaires)
        /// </summary>
        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("entitys", new System.Data.DataColumn("IDCentity", typeof(string)));
        }


        /// <summary>
        /// DataValues (Flux XML) de données diverses
        /// </summary>
        public NGLib.DATA.DATAPO.DataPOFluxXML Flux
        {
            get { if (base._fluxxml == null)_fluxxml = new NGLib.DATA.DATAPO.DataPOFluxXML(this, "Fluxxml"); return _fluxxml; }
        }



        /// <summary>
        /// Entité unique d'isolation Client
        /// </summary>
        public string IDCentity
        {
            get { return base.GetString("IDCentity"); }
            set { base["IDCentity"] = value; }
        }

        /// <summary>
        ///libellé de l'entité unique d'isolation Client
        /// </summary>
        public string EntityLabel
        {
            get { return base.GetString("EntityLabel"); }
            set { base["EntityLabel"] = value; }
        }



        public override string ToString()
        {
            return this.IDCentity;
        }

    }
}
