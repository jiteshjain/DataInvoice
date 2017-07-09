using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.DIRECTINVOICE
{
    public class DirectInvoiceTools
    {


        public static string GenerateDirectInvoice()
        {
            return "DI"+DateTime.Now.ToString("yyyyMMdd")+ Guid.NewGuid().ToString().Replace("-","");
        }



    }
}
