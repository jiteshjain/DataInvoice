using DataInvoice.SOLUTIONS.GENERAL.ACCOUNT;
using DataInvoice.SOLUTIONS.GENERAL.STORE;
using DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN.FORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN
{
    public class CampaignProvider : NGLib.DATA.DATAPO.DataPOProviderSQL
    {

        public CampaignProvider(NGLib.DATA.CONNECTOR.IDataConnector connector)
            : base(connector)
        {

        }


        public Campaign getCampagne(int IDCampaign, bool complete=true)
        {
             Dictionary<string, object> insaddr = new Dictionary<string, object>();
             insaddr.Add("IDCampaign", IDCampaign);
            Campaign retour = base.GetOneDefault<Campaign>(insaddr);
          retour.AddresseBuyerDefault =  retour.IdAddresseBuyerDefault == 0 ? new CONTACT.Address() : (new CONTACT.AddressProvider(Connector).GetAddress(retour.IdAddresseBuyerDefault));
            retour.AddresseSellerDefault = retour.IdAddresseSellerDefault == 0 ? new CONTACT.Address() : (new CONTACT.AddressProvider(Connector).GetAddress(retour.IdAddresseSellerDefault));

            return retour;
        }

        public List<Campaign> getListCampagne(int IDAccount)
        {
            //string sql = "SELECT * FROM campaigns where  IDAccount = @IDAccount";
            Dictionary<string, object> ins = new Dictionary<string, object>();
            if (IDAccount == 0) return base.GetListAllDefault<Campaign>(50, paramKeySearch: ins);
              
              ins.Add("IDAccount", IDAccount);
            //System.Data.DataTable ret = this.Connector.Query(sql, ins);
            //return NGLib.DATA.DATAPO.DataPOParser.ListFromDataTable<Campaign>(ret);
            return base.GetListAllDefault<Campaign>(paramKeySearch: ins);
          
        }

        public Campaign CreateCampagne(Account account, string CampaignTitle)
        {
            try
            {
                Campaign nouveau = new Campaign();
                nouveau.Title = CampaignTitle;
                nouveau.IDAccount = account.IDAccount;
                nouveau.DateCreate = DateTime.Now;
                nouveau.Enabled = true ;
                nouveau["IDCampaign"] = DBNull.Value;
                StoreCloudProvider SCPrv = new StoreCloudProvider(this.Connector);
                StoreCloud sc = SCPrv.CreateStoreCloud(nouveau.Title);
                nouveau.idStoreCloud = sc.IDStore;
                // Insert
                base.InsertBubble(nouveau, false, true);               
                return nouveau;
            }
            catch (Exception ex)
            {
                throw new Exception("CreateCampagne " + ex.Message, ex);
            }
        }

        public Campaign UpdateCampagne(int idCampaign, CampaignApiPoco campaign)
        {
            try
            {
                Campaign oldCampaign = this.getCampagne(idCampaign);
                oldCampaign.FromObject(campaign);
                if(oldCampaign.idStoreCloud==0)
                {
                    StoreCloudProvider SCPrv = new StoreCloudProvider(this.Connector);
                    StoreCloud sc = SCPrv.CreateStoreCloud(oldCampaign.Title);
                    oldCampaign.idStoreCloud = sc.IDStore;
                }

                base.SaveBubble(oldCampaign);

                return oldCampaign;
            }
            catch (Exception ex)
            {
                throw new Exception("CreateCampagne " + ex.Message, ex);
            }
        }

        public Campaign UpdateCampagne( Campaign campaign)
        {
            try
            {
                if (campaign.idStoreCloud == 0)
                {
                    StoreCloudProvider SCPrv = new StoreCloudProvider(this.Connector);
                    StoreCloud sc = SCPrv.CreateStoreCloud(campaign.Title);
                    campaign.idStoreCloud = sc.IDStore;
                }
                base.SaveBubble(campaign);

                return campaign;
            }
            catch (Exception ex)
            {
                throw new Exception("CreateCampagne " + ex.Message, ex);
            }
        }


    }
}
