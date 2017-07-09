using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.MANAGEMENT.ORGANIZEDREPOSITORY
{
    public class OrganisedRepositoryResults : NGLib.DATA.DATAPO.ResultsPO<OrganizedRepository>
    {

        public OrganizedRepository GetRepository(int IDRepository)
        {
            foreach (var item in this)
                if (item.IDRepository == IDRepository) return item;
            return null;
            
        }


    }
}
