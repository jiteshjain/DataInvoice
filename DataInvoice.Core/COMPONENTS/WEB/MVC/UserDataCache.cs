using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataInvoice.Core.COMPONENTS.WEB.MVC
{
    /// <summary>
    /// Données en cache pour un Utilisateur
    /// </summary>
    public class UserDataCache
    {
        //public UserDataCache()
        //{
          
        //}

        //public void SetUser(GENERAL.IDENTITY.USER.User myuser)
        //{
        //    if (this.myuser != null) throw new Exception("Utilisateur Déja Affecté");
        //    this.myuser = myuser;
        //    this.MyIDUser = myuser.IDUser;

        //}

        public UserDataCache(GENERAL.IDENTITY.USER.User myuser)
        {
            this.myuser = myuser;
            this.MyIDUser = myuser.IDUser;

        }

        public GENERAL.IDENTITY.USER.User myuser { get; private set; }

        public int MyIDUser { get; private set; }

        public string MyIDEntity { get; private set; }



        #region Variables UNIQUEMENT SI SYNDIC
        // ------------------------------------------------------------------------------


        public Dictionary<int, string> SyndicAssociations { get; private set; }



        #endregion





        //#region Variables Metiers
        // ------------------------------------------------------------------------------


        //public DataInvoice.SOLUTIONS.GENERAL.ACCOUNT..PROPERTYS.CONTACT.Contact MyContact { get; private set; }


        //public DataInvoice.SOLUTIONS..SYNDICS.SYNDIC.Syndic MySyndic { get; private set; }


        //public void SetSyndic(Coprop.SOLUTIONS.SYNDICS.SYNDIC.Syndic syndic)
        //{
        //    if (this.MySyndic != null) throw new Exception("dblset syndic");
        //    if (syndic == null) throw new Exception("set null syndic");
        //    this.MySyndic = syndic;
        //    this.MyIDEntity = syndic.IDCEntity;
        //    if (string.IsNullOrWhiteSpace(this.MyIDEntity)) throw new Exception("MyIDEntity Syndic empty");

        //}



        //public Coprop.SOLUTIONS.PROPERTYS.ASSOCIATION.Association MyAssociation { get; private set; }


        //public List<Coprop.SOLUTIONS.PROPERTYS.PROPERTY.Property> MyPropertys { get; private set; }


        //#endregion






        //public void AffectSyndic(Coprop.SOLUTIONS.SYNDICS.SYNDIC.Syndic MySyndic)
        //{
        //    this.MySyndic = MySyndic;
        //}


        //public void AffectAssociation(Coprop.SOLUTIONS.PROPERTYS.ASSOCIATION.Association MyAssociation)
        //{
        //    this.MyAssociation = MyAssociation;
        //}


        //public void Reload(NGLib.DATA.CONNECTOR.IDataConnector Connector)
        //{
        //    Coprop.SOLUTIONS.PROPERTYS.ASSOCIATION.AssociationProvider provide = new SOLUTIONS.PROPERTYS.ASSOCIATION.AssociationProvider(Connector);
        //    if (MyAssociation != null)
        //    {

        //        this.AffectAssociation(provide.GetAssociation(this.MyAssociation.IDAssociation, true));
        //    }

        //    if(this.myuser.IsInRole("SYNDIC"))
        //    {
        //        this.SyndicAssociations=provide.GetLabelsAssociation(this.MyIDEntity);
        //    }
        //    Coprop.SOLUTIONS.PROPERTYS.CONTACT.ContactProvider contactprovide = new SOLUTIONS.PROPERTYS.CONTACT.ContactProvider(Connector);
        //    this.MyContact = contactprovide.GetContactsFromUser(this.myuser.IDUser).FirstOrDefault();



        //}


    }
}
