using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.CONTACT
{
    public class ContactResults : List<Contact>
    {


        public Contact GetContact(int IDContact)
        {
            foreach (var item in this)
                if (IDContact == item.IDContact) return item;
            return null;
        }



    }
}
