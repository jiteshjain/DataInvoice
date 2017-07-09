using DataInvoice.SOLUTIONS.INVOICES.INVOICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.DIRECTINVOICE
{

    /// <summary>
    /// Provider Factures non persistantes
    /// Facture hors ligne : Permet de gérer la génération de facture pour les utilisateur annonyme
    /// </summary>
    public class DirectInvoiceProvider : NGLib.DATA.DATAPO.DataPOProviderSQL
    {
        NGLib.COMPONENTS.APP.ENV.IGlobalEnv env = null;



        /// <summary>
        /// Provider Factures non persistantes
        /// </summary>
        /// <param name="env">Environnement Avec MEMCACHED</param>
        public DirectInvoiceProvider(NGLib.COMPONENTS.APP.ENV.IGlobalEnv env)
            : base(env.ConnectorSgbd)
        { this.env = env; }


        /// <summary>
        /// Provider Factures non persistantes
        /// </summary>
        /// <param name="connector">Connecteur Sans memcached</param>
        public DirectInvoiceProvider(NGLib.DATA.CONNECTOR.IDataConnector connector)
            : base(connector)
        { }



        private InvoiceProvider invoiceProvider
        { get { if (_invoiceProvider == null) _invoiceProvider = new InvoiceProvider(this.Connector); return _invoiceProvider; } }
        private InvoiceProvider _invoiceProvider = null;


        private InvoiceManager invoiceManager
        { get { if (_invoiceManager == null) _invoiceManager = new InvoiceManager(this.Connector); return _invoiceManager; } }
        private InvoiceManager _invoiceManager = null;



        /// <summary>
        /// Example method for converting instance into hashentry list with reflection
        /// Use library (like FastMember) for this kind of mapping
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        private static List<StackExchange.Redis.HashEntry> ConvertToHashEntryList(object instance)
        {
            var propertiesInHashEntryList = new List<StackExchange.Redis.HashEntry>();
            foreach (var property in instance.GetType().GetProperties())
            {
                if (!property.Name.Equals("ObjectAdress"))
                {
                    // This is just for an example
                    propertiesInHashEntryList.Add(new StackExchange.Redis.HashEntry(property.Name, instance.GetType().GetProperty(property.Name).GetValue(instance).ToString()));
                }
                else
                {
                    var subPropertyList = ConvertToHashEntryList(instance.GetType().GetProperty(property.Name).GetValue(instance));
                    propertiesInHashEntryList.AddRange(subPropertyList);
                }
            }
            return propertiesInHashEntryList;
        }


        /// <summary>
        /// enregistre en base et dans le cache partagé
        /// </summary>
        public void SaveDirectInvoice(DirectInvoice directinvoice)
        {
            try
            {
                // Enregistrement des clef (données persistantes) en bases
                System.Data.DataRowState rowstate = directinvoice.GetRow().RowState;
                if (rowstate == System.Data.DataRowState.Detached || rowstate == System.Data.DataRowState.Added)
                    this.InsertBubble(directinvoice, true, false);
                else this.SaveBubble(directinvoice);

                // Enregistrement des données non persistantes sur MemCached
                if(env!=null)
                {
                    StackExchange.Redis.ConnectionMultiplexer redis = StackExchange.Redis.ConnectionMultiplexer.Connect(GetRedisConfig());
                    StackExchange.Redis.IDatabase redisDb = redis.GetDatabase();

                    StackExchange.Redis.RedisValue val = directinvoice.ToStringData();
                    StackExchange.Redis.RedisKey key = directinvoice.IdDirectInvoice;
                    redisDb.StringSet(key, val,null, StackExchange.Redis.When.Always, StackExchange.Redis.CommandFlags.None);


                    
                    // redng75Oj82p

                    // !!! stocker dans memecached
                }

            }
            catch (Exception ex)
            {
                throw new Exception("SaveDirectInvoice " + ex.Message,ex);
            }
        }

        public StackExchange.Redis.ConfigurationOptions GetRedisConfig()
        {
            if (this.env == null) return null;
            NGLib.DATA.DATAVALUES.DataValues dv = this.env as NGLib.DATA.DATAVALUES.DataValues;
            StackExchange.Redis.ConfigurationOptions redisconfig = new StackExchange.Redis.ConfigurationOptions();
            redisconfig.EndPoints.Add(dv.GetString("/param/redis"), Convert.ToInt32(dv.GetAttribute("/param/redis","port")));
            redisconfig.Password = dv.GetAttribute("/param/redis", "port");
            return redisconfig;
        }

        /// <summary>
        /// Obtenir du cache partagé
        /// </summary>
        /// <param name="DirectInvoiceid"></param>
        /// <returns></returns>
        public DirectInvoice GetDirectInvoice(string iddirectinvoice)
        {
            DirectInvoice retour = null;
            try
            {
                // Obtien d'abord l'objet sur MemCached si Existe
                if (env != null)
                {

                    StackExchange.Redis.ConfigurationOptions redisconfig = new StackExchange.Redis.ConfigurationOptions();
                    redisconfig.EndPoints.Add("22ec9a54-2921-43af-a4e9-9b1e7aff2b9b.pdb.ovh.net", 21784);
                    redisconfig.Password = "redng75Oj82p";

                    StackExchange.Redis.ConnectionMultiplexer redis = StackExchange.Redis.ConnectionMultiplexer.Connect(redisconfig);
                    StackExchange.Redis.IDatabase redisDb = redis.GetDatabase();


                    StackExchange.Redis.RedisKey key = iddirectinvoice;

                    StackExchange.Redis.RedisValue val = redisDb.StringGet(key, StackExchange.Redis.CommandFlags.None);
                    if(!string.IsNullOrWhiteSpace(val))
                    {
                        retour = new DirectInvoice();
                        retour.FromStringData(val);
                    }


                }

                // sinon regarde en base
                if (retour == null)
                {
                    Dictionary<string, object> ins = new Dictionary<string, object>();
                    ins.Add("iddirectinvoice", iddirectinvoice);
                    retour = this.GetOneDefault<DirectInvoice>(ins);
                }
                return retour;
            }
            catch (Exception ex)
            {
                throw new Exception("GetDirectInvoice " + ex.Message, ex);
            }

        }










        public DirectInvoice PrepareDirectInvoice()
        {
            string directinvoiceid = DirectInvoiceTools.GenerateDirectInvoice();
            DirectInvoice retour = new DirectInvoice();
            retour.DateCreate = DateTime.Now;
            retour.IdDirectInvoice = directinvoiceid;
            retour.Invoice = new INVOICE.Invoice();
            retour.Invoice.IDInvoice = -1; // indique que la facture n'est pas enregistré en base
            return retour;
        }




        /// <summary>
        /// Génération du PDF
        /// </summary>
        /// <param name="DirectInvoiceid"></param>
        /// <returns></returns>
        public byte[] GenerateInvoice(DirectInvoice directInvoice)
        {
            try
            {
                if (directInvoice == null || directInvoice.Invoice == null) throw new Exception("Les données de la facture ne sont plus disponible");
                byte[] retour = this.invoiceManager.GenerateInvoice(directInvoice.Invoice, false);

                return retour;
            }
            catch (Exception ex)
            {
                throw new Exception("GenerateInvoice "+ex.Message,ex);
            }
        }




        /// <summary>
        /// Envoi par mail la facture
        /// </summary>
        /// <param name="DirectInvoiceid"></param>
        /// <param name="copyMail"></param>
        public void SendMailInvoice(string DirectInvoiceid, string copyMail=null)
        {
            
        }


        /// <summary>
        /// Prépare l'inscription a partir des données du direct invoice et retourne le formulaire
        /// </summary>
        /// <param name="DirectInvoiceid"></param>
        public void AutoInscription(string DirectInvoiceid)
        {

        }


    }
}
