using DataInvoice.SOLUTIONS.INVOICES.CONTACT.FORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataInvoice.SOLUTIONS.INVOICES.CONTACT
{
    public class ContactProvider: NGLib.DATA.DATAPO.DataPOProviderSQL
    {

        public ContactProvider(NGLib.DATA.CONNECTOR.IDataConnector connector)
            : base(connector)
        {

        }

        /// <summary>
        /// Trouver contact
        /// </summary>
        /// <param name="IDContact">id</param>
        /// <returns></returns>
        public Contact GetContact(int IDContact, bool Complete=true)
        {
            Dictionary<string, object> insaddr = new Dictionary<string, object>();
            insaddr.Add("IDContact", IDContact);
            Contact retour = base.GetOneDefault<Contact>(insaddr);
            if(Complete)
            {
                 CONTACT.AddressProvider addrProvide = new AddressProvider(this.Connector);
                 if (retour.IDAddressPrimary != null) retour.AddressPrimary = addrProvide.GetAddress(retour.IDAddressPrimary.Value);
            }
            return retour;
        }


        /// <summary>
        /// Récupérer plusieurs Contacts
        /// </summary>
        /// <param name="IDContact"></param>
        /// <returns></returns>
        public Dictionary<int, Contact> GetManyContact(List<int> IDContact)
        {
             string sql = "select * from contacts where IDContact in("+ string.Join(",",IDContact)+")";
            System.Data.DataTable ret = this.Connector.Query(sql);
            return NGLib.DATA.DATAPO.DataPOParser.DictionaryIntFromDataTable<Contact>(ret, "IDContact");
        }






       
        public List<Contact> GetContactsFromUser(int IDUser)
        {
            Dictionary<string, object> ins = new Dictionary<string, object>();
            ins.Add("IDUser", IDUser);
            string sql = "SELECT * FROM Contacts WHERE IDUser = @IDUser";
            System.Data.DataTable ret = this.Connector.Query(sql, ins);
            return NGLib.DATA.DATAPO.DataPOParser.ListFromDataTable<Contact>(ret);
        }
        




        public Contact CreateContact(ContactForm form)
        {
            try
            {
                Contact nouveau = new Contact();
                nouveau.FromObject(form);



                if (form.Address != null && !string.IsNullOrWhiteSpace(form.Address.CodePostal))
                {
                    CONTACT.AddressProvider addrProvide = new AddressProvider(this.Connector);
                    addrProvide.DefineAddress(nouveau, form.Address);
                }



                // Insert
                nouveau["IDContact"] = DBNull.Value;
                base.InsertBubble(nouveau, false, true);

                return nouveau;
            }
            catch (Exception ex)
            {
                throw new Exception("CreateContact" + ex.Message, ex);
            }
        }


        public List<Contact> SearchContact(ContactForm form)
        {
            Dictionary<string, object> inscont = new Dictionary<string, object>();
            if (form.IDContact != 0) inscont.Add("IDContact", form.IDContact);
            

            return base.GetListAllDefault<Contact>(paramKeySearch: inscont);

        }

        public Contact UpdateContact(ContactForm form)
        {
            try
            {
                Contact retour = this.GetContact(form.IDContact);
                if (retour == null) throw new Exception("Contact not found");
                retour.FromObject(form);
                if (form.Address != null)
                {
                    CONTACT.AddressProvider addrProvide = new AddressProvider(this.Connector);
                    addrProvide.DefineAddress(retour, form.Address);
                }
               
                base.SaveBubble(retour);

                retour = this.GetContact(form.IDContact);

                return retour;
            }
            catch (Exception ex)
            {
                throw new Exception("UpdateContact " + ex.Message, ex);
            }
        }

        public bool DeleteContact(int IDContact)
        {
            try
            {
                Contact retour = GetContact(IDContact);
                if (retour == null) throw new Exception("Contact not found");
                base.DeleteBubble(retour);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("DeleteContact " + ex.Message, ex);
            }
        }

        public List<Contact> SearchContacts(ContactForm form)
        {
            try
            {
                List<Contact> retour;

                string sql = @"SELECT contacts.* FROM contacts";
                Dictionary<string, object> ins = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(form.LastName)) ins.Add("LastName", form.LastName);
                if (!string.IsNullOrEmpty(form.Mail)) ins.Add("Mail", form.Mail);
                if (!string.IsNullOrEmpty(form.Phone)) ins.Add("Phone", form.Phone);
                if (form.Address != null && (!string.IsNullOrWhiteSpace(form.Address.CodePostal) || !string.IsNullOrWhiteSpace(form.Address.Ville) || !string.IsNullOrWhiteSpace(form.Address.Pays)))
                {
                    sql += " inner join contacts_address on IDAddressPrimary=IDAddress ";
                    if (!string.IsNullOrEmpty(form.Address.CodePostal)) ins.Add("CodePostal", form.Address.CodePostal);
                    if (!string.IsNullOrEmpty(form.Address.Ville)) ins.Add("Ville", form.Address.Ville);
                    if (!string.IsNullOrEmpty(form.Address.Pays)) ins.Add("Pays", form.Address.Pays);
                }

                List<string> filtres = new List<string>();
                ins.Keys.ToList<string>().ForEach(k => filtres.Add(k + " = @" + k));
                if (filtres.Any())
                    sql += " where " + string.Join(" and ", filtres);

                System.Data.DataTable ret = this.Connector.Query(sql,ins);
                retour = NGLib.DATA.DATAPO.DataPOParser.ListFromDataTable<Contact>(ret);

                return retour;
            }
            catch (Exception ex)
            {
                throw new Exception("SearchContacts " + ex.Message, ex);
            }
        }



    }
}
