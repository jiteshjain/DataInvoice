using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.DOCMODEL
{
    public class DocModel : NGLib.DATA.DATAPO.DataPO
    {
        
        public DocModel()
        {
            this.DefineStructRow();
        }

        public DocModel(System.Data.DataRow row)
        {
            this.SetRow(row);
        }

        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("docs_models", new System.Data.DataColumn("IDModel", typeof(int)));
        }
                

        public int IDAccount
        {
            get { return this.GetInt("IDAccount",0); }
            set { this["IDAccount"] = value; }
        }
        
        public int? IDCampaign
        {
            get { return this.GetInt("IDCampaign",0); }
            set { this["IDCampaign"] = value; }
        }
        
        public int IDModel
        {
            get { return this.GetInt("IDModel",0); }
            set { this["IDModel"] = value; }
        }
        
        public string Title
        {
            get { return this.GetString("Title"); }
            set { this["Title"] = value; }
        }

    }
}
