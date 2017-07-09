using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE
{
    public class InvoiceLine: NGLib.DATA.DATAPO.DataPO
    {

        public InvoiceLine()
        {
            this.DefineStructRow();
        }

        public InvoiceLine(System.Data.DataRow row)
        {
            this.SetRow(row);
        }

        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("invoices_lines", new System.Data.DataColumn("IDLine", typeof(long)));
        }


        public int IDAccount
        {
            get { return this.GetInt("IDAccount", 0); }
            set { this["IDAccount"] = value; }
        }

        public int IDInvoice
        {
            get { return this.GetInt("IDInvoice", 0); }
            set { this["IDInvoice"] = value; }
        }


        public long IDLine
        {
            get { return this.GetLong("IDLine", 0); }
            set { this["IDLine"] = value; }
        }



        public string LineReference
        {
            get { return this.GetString("LineReference"); }
            set { this["LineReference"] = value; }
        }


        public string LineLabel
        {
            get { return this.GetString("LineLabel"); }
            set { this["LineLabel"] = value; }
        }

        // string LineSubLabel
        public string LineSubLabel
        {
            get { return this.GetString("LineSubLabel"); }
            set { this["LineSubLabel"] = value; }
        }


       // public double LineQuantity
        public int LineQuantity
        {
            get { return this.GetInt("LineQuantity",0); }
            set { this["LineQuantity"] = value; }
        }

        // double LineAmount
        public double LineAmount
        {
            get { object obj = this.GetObject("LineAmount"); if (obj == null || obj == DBNull.Value) return 0; return Convert.ToDouble(obj); }
            set { this["LineAmount"] = value; }
        }

        // double LineTax
        public double LineTax
        {
            get { object obj = this.GetObject("LineTax"); if (obj == null || obj == DBNull.Value) return 0; return Convert.ToDouble(obj); }
            set { this["LineTax"] = value; }
        }

        // string LineUserfield
        public string LineUserfield
        {
            get { return this.GetString("LineUserfield"); }
            set { this["LineUserfield"] = value; }
        }



        /// <summary>
        /// Produits sans les taxes
        /// </summary>
        /// <returns></returns>
        public double GetSubTotalAmount()
        {
            if (this.LineQuantity < 1) return 0;
            return this.LineQuantity * this.LineAmount;
        }

        public double GetTotalLineTax()
        {
            return GetSubTotalAmount() * (LineTax / 100);
        }


        public double GetTotalLineAmount()
        {
            return GetSubTotalAmount() + GetTotalLineTax();
        }

    }
}
