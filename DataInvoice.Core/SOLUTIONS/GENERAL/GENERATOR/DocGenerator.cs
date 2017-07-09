using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.GENERATOR
{
    public class DocGeneratorPO : NGLib.COMPONENTS.DOCUMENT.DOCGENERATOR.DocGeneratorPOBase
    {
        public DocGeneratorPO()
        {
            DefineStructRow();
        }

        public DocGeneratorPO(System.Data.DataRow row)
        {
            this.SetRow(row);
        }

        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("documentgenerator", new System.Data.DataColumn("IDDocGenerator", typeof(int)));
        }

        public string UniqueLabel
        {

            get { return this.GetString("UniqueLabel"); }
            set { this["UniqueLabel"] = value; }
        }


    }
}
