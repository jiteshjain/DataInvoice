using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.CONTACT
{
    public class ContactManager
    {

        public ContactProvider contactProvider = null;

        public ContactManager(ContactProvider contactProvider)
        {
            this.contactProvider = contactProvider;
        }

        public ContactManager(NGLib.DATA.CONNECTOR.IDataConnector connect)
        {
            this.contactProvider = new ContactProvider(connect);
        }



       














    }
}
