using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN
{
    /// <summary>
    /// Campagne de paramétrage des factures
    /// </summary>
    public class Campaign : NGLib.DATA.DATAPO.DataPO
    {

        public Campaign()
        {
            this.DefineStructRow();
        }

        public Campaign(System.Data.DataRow row)
        {
            this.SetRow(row);
        }

        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("campaigns", new System.Data.DataColumn("IDCampaign", typeof(string)));
        }



        /// <summary>
        /// DataValues (Flux XML) de données diverses
        /// </summary>
        public NGLib.DATA.DATAPO.DataPOFluxXML Flux
        {
            get { if (base._fluxxml == null) _fluxxml = new NGLib.DATA.DATAPO.DataPOFluxXML(this, "Fluxxml"); return _fluxxml; }
        }




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



        public string Title
        {
            get { return GetString("Title"); }
            set { this["Title"] = value; }
        }


        public bool Enabled
        {
            get { return this.GetBoolean("Enabled", false); }
            set { this["Enabled"] = value; }
        }


        public DateTime DateCreate
        {
            get { return Convert.ToDateTime(base["DateCreate"]); }
            set { base["DateCreate"] = value; }
        }






        public string DefaultLogoUrl
        {
            get { return this.Flux.GetString("DefaultLogoUrl"); }
            set { this.Flux.SetString("DefaultLogoUrl", value); }
        }

        public int IdAddresseSellerDefault
        {
            get { return this.GetInt("IdAddresseSellerDefault", 0); }
            set { this["IdAddresseSellerDefault"] = value; }
        }

        public int IdAddresseBuyerDefault
        {
            get { return this.GetInt("IdAddresseBuyerDefault", 0); }
            set { this["IdAddresseBuyerDefault"] = value; }
        }

        public int idStoreCloud
        {
            get { return this.GetInt("idStoreCloud", 0); }
            set { this["idStoreCloud"] = value; }
        }

        public DataInvoice.SOLUTIONS.INVOICES.CONTACT.Address AddresseSellerDefault { get; set; }

        public DataInvoice.SOLUTIONS.INVOICES.CONTACT.Address AddresseBuyerDefault { get; set; }

        /// <summary>
        /// Paramétrage pour la personalisation des facture (extend)
        /// </summary>
        public CampaignInvoiceCustomizExtend InvoiceCustomiz
        { get { if (_InvoiceCustomiz == null) _InvoiceCustomiz = new CampaignInvoiceCustomizExtend(this); return _InvoiceCustomiz; } }
        private CampaignInvoiceCustomizExtend _InvoiceCustomiz = null;


    }
}
