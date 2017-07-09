using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE
{
    public class InvoiceLog: NGLib.DATA.DATAPO.DataPO
    {

        public InvoiceLog()
        {
            this.DefineStructRow();
        }

        public InvoiceLog(System.Data.DataRow row)
        {
            this.SetRow(row);
        }

        protected override void DefineStructRow()
        {
            this.POManager().DefineRow("invoices_logs", new System.Data.DataColumn("IDLog", typeof(long)));
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


        public long IDLog
        {
            get { return this.GetLong("IDLog", 0); }
            set { this["IDLog"] = value; }
        }


        // MessageText (varchar 256)
        public string MessageText
        {
            get { return this.GetString("MessageText"); }
            set { this["MessageText"] = value; }
        }
        
        // MessageLevel (int)
        public int? MessageLevel
        {
            get { return this.GetInt("MessageLevel",0); }
            set { this["MessageLevel"] = value; }
        }

        // DateCreate
        public DateTime? DateCreate
        {
            get { return this.GetDateTime("DateCreate",DateTime.MinValue); }
            set { this["DateCreate"] = value; }
        }

        //bool InternalLog (log interne qui ne sera pas montré aux clients)
        public bool? InternalLog 
        {
            get { return this.GetBoolean("InternalLog",false); }
            set { this["InternalLog"] = value; }
        }





    }
}
