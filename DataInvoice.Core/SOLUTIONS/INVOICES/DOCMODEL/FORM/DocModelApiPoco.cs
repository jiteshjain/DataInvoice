using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.Core.SOLUTIONS.INVOICES.DOCMODEL.FORM
{
   public class DocModelApiPoco
    {
       public int IDAccount{ get; set; }

       public int? IDCampaign { get; set; }

       public int IDModel { get; set; }

       public string Title { get; set; }

       public DocModelApiPoco() { }

       public DocModelApiPoco(DataInvoice.SOLUTIONS.INVOICES.DOCMODEL.DocModel docModel)
       {
           this.IDAccount = docModel.IDAccount;
           this.IDCampaign = docModel.IDCampaign;
           this.IDModel = docModel.IDModel;
           this.Title = docModel.Title;
       }

    }
}
