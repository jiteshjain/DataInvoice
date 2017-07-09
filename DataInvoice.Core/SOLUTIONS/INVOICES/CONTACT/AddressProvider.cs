using DataInvoice.SOLUTIONS.INVOICES.CONTACT.FORM;
using DataInvoice.SOLUTIONS.INVOICES.CONTACT.POCO;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.CONTACT
{
    public class AddressProvider : NGLib.DATA.DATAPO.DataPOProviderSQL
    {

        public AddressProvider(NGLib.DATA.CONNECTOR.IDataConnector connector)
            : base(connector)
        {

        }

        /// <summary>
        /// Trouver Addresse
        /// </summary>
        /// <param name="IDAddress">id</param>
        /// <returns></returns>
        public Address GetAddress(int IDAddress)
        {
            Dictionary<string, object> insaddr = new Dictionary<string, object>();
            insaddr.Add("IDAddress", IDAddress);
            Address retour = base.GetOneDefault<Address>(insaddr);

            return retour;
        }

        // CreateAddress(form)
        public Address CreateAddress(AddressForm form)
        {
            try
            {
                Address nouveau = new Address();
                nouveau.FromObject(form);
                                
                nouveau["IDAddress"] = DBNull.Value;

                // Insert
                base.InsertBubble(nouveau, false, true);

                return nouveau;
            }
            catch (Exception ex)
            {
                throw new Exception("CreateAddress" + ex.Message, ex);
            }
        }

        // UpdateAddress(form)
        public Address UpdateAdresse(Address address, AddressForm form)
        {

            form.ToPo(address);
            this.SaveAddress(address);


            try
            {
                address.FromObject(form);


                return address;
            }
            catch (Exception ex)
            {
                throw new Exception("UpdateAdresse " + ex.Message, ex);
            }
        }

        public void SaveAddress(Address address)
        {
            try
            {
                if (address.IDAddress < 1) this.InsertAddress(address);
                else base.SaveBubble(address);
            }
            catch (Exception ex)
            {
                throw new Exception("SaveAddress " + ex.Message, ex);
            }
        }


        public void InsertAddress(Address address)
        {
            try
            {
                address["IDAddress"] = DBNull.Value;
                this.InsertBubble(address, false, true);
            }
            catch (Exception ex)
            {
                throw new Exception("InsertAddress " + ex.Message, ex);
            }
        }

        /// <summary>
        /// methode qui permet d'enrichir un objet adress avec les informations provenant de https://firmapi.com/
        /// </summary>
        /// <author>z.omezzine@gmail.com</author>
        /// <param name="Siren">numéro d'immatriculation d'une entreprise</param>
        /// <param name="adresse">L'objet address à enrechir</param>
        /// <param name="safe">boolean, si true ne retournera jamais d'exeption</param>
        /// <returns>Retourne un objet Address, enrechi à partir de retour JSON de l'API firmapi.com</returns>
        public bool EnrishAdressFromSiren(Address adresse, string Siren, bool safe = false)
        {
            try
            {
                ResultObjectPoco poco = GetAdressFromSiren(Siren, safe);
                if (poco != null && poco.company != null && !string.IsNullOrEmpty(poco.company.postal_code) && adresse != null)
                {
                    adresse.Postcode = poco.company.postal_code;
                    adresse.Adress1 = poco.company.address;
                    adresse.City = poco.company.city;
                    return true;
                }
            }
            catch (Exception ex)
            {
                if (safe) return false;
                throw new Exception("EnrishAdressFromSiren " + ex.Message, ex);
            }
            return false;
        }


        /// <summary>
        /// Méthode qui retourne un objet créer a partir du json déserializé 
        /// </summary>
        /// <author>z.omezzine@gmail.com</author>
        /// <param name="Siren">numéro d'immatriculation d'une entreprise</param>
        /// <param name="safe">boolean, si true ne retournera jamais d'exeption</param>
        /// <returns>objet créer a partir du json déserializé</returns>
        public ResultObjectPoco GetAdressFromSiren(string Siren, bool safe = false)
        {
            ResultObjectPoco result = null;
            try
            {
                var client = new RestClient("https://firmapi.com/api/");
                var request = new RestRequest("v1/companies/" + Siren);
                request.RequestFormat = DataFormat.Json;

                var queryResult = client.Execute(request);
                JsonDeserializer deserial = new JsonDeserializer();
                result = deserial.Deserialize<ResultObjectPoco>(queryResult);
                if (!safe && !result.status.Equals("success")) throw new Exception("Siren API Error"); 
            }
            catch (Exception ex)
            {
                result = null;
                if (safe) return null;
                throw new Exception("EnrishAdressFromSiren " + ex.Message, ex);
            }
            return result;
        }

    }
}
