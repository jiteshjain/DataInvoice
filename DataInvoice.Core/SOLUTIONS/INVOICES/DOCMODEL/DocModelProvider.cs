using DataInvoice.Core.SOLUTIONS.INVOICES.DOCMODEL.FORM;
using DataInvoice.SOLUTIONS.GENERAL.ACCOUNT;
using DataInvoice.SOLUTIONS.INVOICES.CAMPAIGN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.DOCMODEL
{
    public class DocModelProvider : NGLib.DATA.DATAPO.DataPOProviderSQL
    {

        public DocModelProvider(NGLib.DATA.CONNECTOR.IDataConnector connector)
            : base(connector)
        {

        }

        // GetModel(int IDModel)
        public DocModel GetDocModel(int IDDocModel)
        {
            Dictionary<string, object> insaddr = new Dictionary<string, object>();
            insaddr.Add("IDModel", IDDocModel);
            DocModel retour = base.GetOneDefault<DocModel>(insaddr);

            return retour;
        }

        public List<DocModel> GetDocModels(DocModelApiPoco form)
        {
            Dictionary<string, object> insaddr = new Dictionary<string, object>();
            if (form == null)
                  return base.GetListAllDefault<DocModel>(50, paramKeySearch: insaddr);
            else { 
                if (form.IDModel != 0) insaddr.Add("IDModel", form.IDModel);
                if (form.IDCampaign.HasValue) insaddr.Add("IDCampaign", form.IDCampaign.Value);
                if (form.IDAccount != 0) insaddr.Add("IDAccount", form.IDAccount);
                if (!string.IsNullOrEmpty(form.Title)) insaddr.Add("Title", form.Title);
                return base.GetListAllDefault<DocModel>(paramKeySearch: insaddr);
            }
            
        }

        public DocModel CreateModel(Account account, Campaign campaign, string Title)
        {

            try
            {
                DocModel nouveau = new DocModel();
                nouveau.Title = Title;
                nouveau.IDAccount=account.IDAccount;
                nouveau.IDCampaign=campaign.IDCampaign;
                nouveau["IDModel"] = DBNull.Value;

                // Insert
                base.InsertBubble(nouveau, false, true);

                return nouveau;
            }
            catch (Exception ex)
            {
                throw new Exception("CreateModel " + ex.Message, ex);
            }
        }

        public DocModel UpdateModel( DocModel model)
        {  try
            {
            // la mise à jour directe de model en paramètre crash !!!
               DocModel docModel = this.GetDocModel(model.IDModel);
               docModel.Title = model.Title;
               docModel.IDAccount = model.IDAccount;
               docModel.IDCampaign = model.IDCampaign;
               base.SaveBubble(docModel);
               return docModel;
            }
            catch (Exception ex)
            {
                throw new Exception("UpdateModel " + ex.Message, ex);
            }
        }

           

    }
}
