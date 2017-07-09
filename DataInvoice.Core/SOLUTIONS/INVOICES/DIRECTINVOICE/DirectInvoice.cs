using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.DIRECTINVOICE
{
    public class DirectInvoice : NGLib.DATA.DATAPO.DataPO
    {

        public DirectInvoice()
        {
            this.DefineStructRow();
        }

        public DirectInvoice(System.Data.DataRow row)
        {
            this.SetRow(row);
        }

        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("directinvoices", new System.Data.DataColumn("iddirectinvoice", typeof(string)));
        }


        /// <summary>
        /// représente l'objet données facture hors database
        /// </summary>
        public DataInvoice.SOLUTIONS.INVOICES.INVOICE.Invoice Invoice { get; set; } // NON PERSISTANT








        public string IdDirectInvoice
        {
            get { return this.GetString("IdDirectInvoice"); }
            set { this["IdDirectInvoice"] = value; }
        }

        public DateTime DateCreate
        {
            get { return this.GetDateTime("DateCreate", DateTime.MinValue); }
            set { this["DateCreate"] = value; }
        }

        



        // Context Utilisateur

        public string FromIP
        {
            get { return this.GetString("IdDirectInvoice"); }
            set { this["IdDirectInvoice"] = value; }
        }





        public override string ToString()
        {
            return IdDirectInvoice;
        }



        public string ToStringData()
        {
            try
            {
                //// = this.GetRow();
                //string dirowstr = NGLib.DATA.FORMAT.CryptHash.ToBase64(this.POManager().to());
                //string inrowstr = "";
                //if (this.Invoice != null) inrowstr = NGLib.DATA.FORMAT.CryptHash.ToBase64(this.Invoice.POManager().ToDatasXML());

                //string strdata = dirowstr + "|" + inrowstr;
                //return strdata;
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception("ToStringData " + ex.Message);
            }
        }


        public bool FromStringData(string strdata)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(strdata)) return false;

                //string[] strdataT = strdata.Split('|');
                //if (strdataT.Length < 2) return false;

                //this.POManager().FromDatasXML(NGLib.DATA.FORMAT.CryptHash.FromBase64(strdataT[0]));
                //if(!string.IsNullOrWhiteSpace(strdataT[1]))
                //{
                //    if (this.Invoice == null) this.Invoice = new INVOICE.Invoice();
                //    this.Invoice.POManager().FromDatasXML(NGLib.DATA.FORMAT.CryptHash.FromBase64(strdataT[1]));
                //}

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("FromStringData " + ex.Message);
            }
        }



    }
}
