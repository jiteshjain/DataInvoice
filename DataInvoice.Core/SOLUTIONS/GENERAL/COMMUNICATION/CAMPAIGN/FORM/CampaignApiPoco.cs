using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN.FORM
{
    public class CampaignApiPoco
    {
        public int IDAccount { get; set; }

        public int IDCampaign { get; set; }

        public string Title { get; set; }

        public bool Enabled { get; set; }

        public DateTime DateCreate { get; set; }

        public string DefaultLogoUrl { get; set; }

        public int IdAddresseSellerDefault { get; set; }

        public int IdAddresseBuyerDefault { get; set; }

        public CampaignApiPoco() { }

        public CampaignApiPoco(Campaign campaign)
        {
            this.IDAccount = campaign.IDAccount;
            this.IDCampaign = campaign.IDCampaign;
            this.Title = campaign.Title;
            this.Enabled = campaign.Enabled;
            this.DateCreate = campaign.DateCreate;
            this.DefaultLogoUrl = campaign.DefaultLogoUrl;
            this.IdAddresseSellerDefault = campaign.IdAddresseSellerDefault;
            this.IdAddresseBuyerDefault = campaign.IdAddresseBuyerDefault;
        }
    }
}
