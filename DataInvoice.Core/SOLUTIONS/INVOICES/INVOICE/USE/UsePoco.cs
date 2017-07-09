using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.INVOICE.USE
{


        public struct InvoiceGenerateNumberResult
        {
            public string InvoiceNumber { get; set; }

            public DateLevelEnum CoWhereDate { get; set; }

            public int InvoiceSubNumber { get; set; }


        }

        public enum DateLevelEnum
        {
            NO = 0,
            YEAR = 1,
            MONTH = 2,
            DAY = 3,

        }

    
}
