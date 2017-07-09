using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.CONTACT.FORM
{
    public interface IWithAddress
    {

        int? IDAddressPrimary { get; set; }

        bool DedicatedAddressPrimary { get; set; }

        CONTACT.Address AddressPrimary { get; set; }

    }
}
