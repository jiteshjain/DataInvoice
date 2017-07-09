using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.CONTACT
{
    public class ContactRelation : NGLib.DATA.DATAPO.DataPO
    {
        public ContactRelation()
        {
            DefineStructRow(); // meme si vide on définie quand meme la structure pour créer les champs principaux dans le datarow
        }


        public ContactRelation(System.Data.DataRow row)
        {
            this.SetRow(row); //Création du datarow depuis une requette sql
        }


        /// <summary>
        /// Definir la structure de l'objet métier (clés primaires)
        /// </summary>
        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("contacts_relations", new System.Data.DataColumn("IDContact", typeof(int)));
        }

   

        public int IDContact
        {
            get { return Convert.ToInt32(base["IDContact"]); }
            set { base["IDContact"] = value; }
        }


        public string TypeRelation
        {
            get { return this.GetString("TypeRelation"); }
            set { base["TypeRelation"] = value; }
        }

        public int IDRelation
        {
            get { return Convert.ToInt32(base["IDRelation"]); }
            set { base["IDRelation"] = value; }
        }


        public string ModeRelation
        {
            get { return this.GetString("ModeRelation"); }
            set { base["ModeRelation"] = value; }
        }

    }
}
