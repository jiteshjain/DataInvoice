using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER
{
    public static class UserTools
    {


        public static string PasswordHash(string OrginalPassword)
        {
            return NGLib.DATA.FORMAT.CryptHash.EasyHash(OrginalPassword);
        }

    }
}
