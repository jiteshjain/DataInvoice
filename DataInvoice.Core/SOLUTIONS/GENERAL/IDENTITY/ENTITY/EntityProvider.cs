using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.GENERAL.IDENTITY.ENTITY
{
    public class EntityProvider: NGLib.DATA.DATAPO.DataPOProviderSQL
    {

        public EntityProvider(NGLib.DATA.CONNECTOR.IDataConnector connector)
            : base(connector)
        {

        }

        /// <summary>
        /// Trouver une entité
        /// </summary>
        /// <param name="IDCEntity"></param>
        /// <returns></returns>
        public Entity GetEntity(string IDCEntity)
        {
            Dictionary<string, object> insEnt = new Dictionary<string, object>();
            insEnt.Add("IDCEntity", IDCEntity);
            Entity retour = base.GetOneDefault<Entity>(insEnt);

            return retour;
        }


        /// <summary>
        /// Récupérer plusieurs Entity
        /// </summary>
        /// <param name="IDCEntity"></param>
        /// <returns></returns>
        public Dictionary<int, Entity> GetManyEntity(List<int> IDCEntity)
        {
            string sql = "select * from where IDCEntity IN(" +string.Join(",",IDCEntity) +" )";
            System.Data.DataTable ret = this.Connector.Query(sql);
            return NGLib.DATA.DATAPO.DataPOParser.DictionaryIntFromDataTable<Entity>(ret, "IDCEntity");
        }



        /// <summary>
        /// Creation Entité
        /// </summary>
        /// <param name="form">Poco</param>
        /// <returns></returns>
        public Entity CreateEntity(FORM.EntityForm form)
        {
            try
            {
                Entity nouveau = new Entity();
                nouveau.EntityLabel = form.EntityLabel;
                nouveau.IDCentity = form.IDCEntity;

                // Insert
                base.InsertBubble(nouveau);//, false, true);

                return nouveau;
            }
            catch (Exception ex)
            {
                throw new Exception("CreateEntity" + ex.Message, ex);
            }
        }

        /// <summary>
        /// Recherche des entités
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public List<Entity> SearchEntity(FORM.EntityForm form)
        {
            Dictionary<string, object> insaddr = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(form.EntityLabel)) insaddr.Add("EntityLabel", form.EntityLabel);
            return base.GetListAllDefault<Entity>(paramKeySearch: insaddr);

        }

        /// <summary>
        /// Mise à jour
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public Entity UpdateEntity(FORM.EntityForm form)
        {
            try
            {
                Entity retour = this.GetEntity(form.IDCEntity);
                if (retour == null) throw new Exception("Adresse not found");
                retour.EntityLabel = form.EntityLabel;               
                
                base.SaveBubble(retour);

                retour = this.GetEntity(form.IDCEntity);

                return retour;
            }
            catch (Exception ex)
            {
                throw new Exception("UpdateAdresse " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Supprimer
        /// </summary>
        /// <param name="IDCEntity"></param>
        /// <returns></returns>
        public bool DeleteEntity(string  IDCEntity)
        {
            try
            {
                Entity retour = GetEntity(IDCEntity);
                if (retour == null) throw new Exception("Adresse not found");
                base.DeleteBubble(retour);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("DeleteAdresse " + ex.Message, ex);
            }
        }




    }
}
