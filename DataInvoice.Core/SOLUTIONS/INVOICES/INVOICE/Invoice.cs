using DataInvoice.SOLUTIONS.INVOICES.CONTACT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE
{
    public class Invoice : NGLib.DATA.DATAPO.DataPO
    {

        public Invoice()
        {
            this.DefineStructRow();
        }

        public Invoice(System.Data.DataRow row)
        {
            this.SetRow(row);
        }

        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("invoices", new System.Data.DataColumn("IDInvoice", typeof(int)));
        }


        /// <summary>
        /// DataValues (Flux XML) de données diverses
        /// </summary>
        public NGLib.DATA.DATAPO.DataPOFluxXML Flux
        {
            get { if (base._fluxxml == null)_fluxxml = new NGLib.DATA.DATAPO.DataPOFluxXML(this, "Fluxxml"); return _fluxxml; }
        }


        /// <summary>
        /// Extentions et accesseurs suplémentaire pour l'objet
        /// </summary>
        public InvoiceData Data
        { get { if (_Data == null)_Data = new InvoiceData(this); return _Data; } }
        private InvoiceData _Data = null;





        #region InternalDetailsKeys


        public int IDAccount
        {
            get { return this.GetInt("IDAccount", 0); }
            set { this["IDAccount"] = value; }
        }

        public int IDCampaign
        {
            get { return this.GetInt("IDCampaign", 0); }
            set { this["IDCampaign"] = value; }
        }

        public int IDInvoice
        {
            get { return this.GetInt("IDInvoice", 0); }
            set { this["IDInvoice"] = value; }
        }


        public DateTime? DateCreate
        {
            get { return this.GetDateTime("DateCreate", DateTime.MinValue); }
            set { this["DateCreate"] = value; }
        }


        public DateTime? DateValidate
        {
            get { return this.GetDateTime("DateValidate", DateTime.MinValue); }
            set { this["DateValidate"] = value; }
        }

        public DateTime? DateSend
        {
            get { return this.GetDateTime("DateSend", DateTime.MinValue); }
            set { this["DateSend"] = value; }
        }

        // DateAcq
        public DateTime? DateAcq
        {
            get { return this.GetDateTime("DateAcq", DateTime.MinValue); }
            set { this["DateAcq"] = value; }
        }


        // InvoiceState (DataInvoice.SOLUTIONS.INVOICES.INVOICE.ENUM)
        public DataInvoice.SOLUTIONS.INVOICES.INVOICE.ENUM.InvoiceStateEnum InvoiceState
        {
            get { return (DataInvoice.SOLUTIONS.INVOICES.INVOICE.ENUM.InvoiceStateEnum)this.GetInt("InvoiceState", 1); }
            set { this["InvoiceState"] = (int)value; }
        }




        // InvoiceType
        public ENUM.InvoiceTypeEnum InvoiceType
        {
            get { return (ENUM.InvoiceTypeEnum) Enum.Parse(typeof(ENUM.InvoiceTypeEnum), this.GetString("InvoiceType")); }
            set { this["InvoiceType"] = value.ToString(); }
        }



        #endregion




        #region InvoiceDetails

        
        /// <summary>
        /// Référence/numéro comptable de la facture (varchar 48)
        /// </summary>
        public string RefInvoice
        {
            get { return this.GetString("RefInvoice"); }
            set { this["RefInvoice"] = value; }
        }

        public string InvoiceTitle
        {
            get { return this.GetString("InvoiceTitle"); }
            set { this["InvoiceTitle"] = value; }
        }



 
        /// <summary>
        /// Date Principale de la facture (date sans les heures)
        /// </summary>
        public DateTime? DateInvoice
        {
            get { return this.GetDateTime("DateInvoice", DateTime.MinValue, NGLib.DATA.BASICS.DataAccessorOptionEnum.Safe); }
            set { this["DateInvoice"] = value; }
        }



        /// <summary>
        /// Date de paiement de la facture
        /// </summary>
        public DateTime? DatePaid
        {
            get { return this.GetDateTime("DatePaid", DateTime.MinValue); }
            set { this["DatePaid"] = value; }
        }

        // double FinalAmount {}    (Montant final de la facture)
        public Double FinalAmount
        {
            get
            {
                long fa = 0;
                long.TryParse(this.GetObject("FinalAmount") != null ? this.GetObject("FinalAmount").ToString() : "0", out fa);
                return fa;
            }
            set { this["FinalAmount"] = value; }
        }

        public Double DefaultTaxeValue
        {
            get
            {
                long fa = 0;
                long.TryParse(this.GetObject("FinalAmoDefaultTaxeValueunt") != null ? this.GetObject("DefaultTaxeValue").ToString() : "20", out fa);
                return fa;
            }
            set { this["DefaultTaxeValue"] = value; }
        }





        // Variable libre
        public string Userfield001
        {
            get { return this.GetString("Userfield001"); }
            set { this["Userfield001"] = value; }
        }


        public string InvoiceLogo
        {
            get { return this.Flux.GetString("/param/InvoiceLogo"); }
            set { this.Flux.SetString("/param/InvoiceLogo", value); }
        }


        #endregion




        #region BUYER

        /// <summary>
        /// Référence spécial de l'acheteur
        /// </summary>
        public string RefBuyer
        {
            get { return this.GetString("BuyerRef"); }
            set { this["BuyerRef"] = value; }
        }


        /// <summary>
        /// Objet adresse de l'acheteur
        /// </summary>
        public Address BuyerAddress { get; set; }
        public long BuyerIDAddress
        {
            get { return this.GetLong("BuyerIDAddress", 0); }
            set { this["BuyerIDAddress"] = value; }
        }

        #endregion



        #region SELLER

        /// <summary>
        /// Objet Adresse du vendeur
        /// </summary>
        public Address SellerAddress { get; set; }
        public long SellerIDAddress
        {
            get { return this.GetLong("SellerIDAddress", 0); }
            set { this["SellerIDAddress"] = value; }
        }
        /// <summary>
        /// Référence spéciale du Vendeur
        /// </summary>
        public string RefSeller
        {
            get { return this.GetString("SellerRef"); }
            set { this["SellerRef"] = value; }
        }
        #endregion



        #region Fichier

        /// <summary>
        /// Fichier Facture
        /// </summary>
        public string IDFile
        {
            get { return this.GetString("IDFile"); }
            set { this["IDFile"] = value; }
        }

        public int IDDocGenerator
        {
            get { return this.GetInt("IDDocGenerator",0); }
            set { this["IDDocGenerator"] = value; }
        }

        public DataInvoice.SOLUTIONS.GENERAL.GENERATOR.DocGeneratorPO DocGeneratorTemplate { get; set; }




        #endregion




        public InvoiceLineResults Lines = new InvoiceLineResults();
        public List<InvoiceLog> Logs = new List<InvoiceLog>();




        public override string ToString()
        {
            try
            {
                return this.IDInvoice.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
            
        }



    }
}
