using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.COMMUNICATION
{
    public class CommunicationProvider : NGLib.COMPONENTS.RELATION.COMMUNICATION.CommunicationProvider<COMMUNICATION.ComminicationItem, COMMUNICATION.CommunicationConfig>
    {


        public CommunicationProvider(NGLib.DATA.CONNECTOR.IDataConnector connector) : base(connector)
        {




        }



    }
}
