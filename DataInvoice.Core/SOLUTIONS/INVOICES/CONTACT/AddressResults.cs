using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.CONTACT
{
    public class AddressResults : NGLib.DATA.DATAPO.ResultsPO<Address>
    {

        public Address GetAdress(long IDAddress)
        {
            foreach (Address item in this)
            
                if (item.IDAddress == IDAddress) return item;
            return null;
        }



    }
}
