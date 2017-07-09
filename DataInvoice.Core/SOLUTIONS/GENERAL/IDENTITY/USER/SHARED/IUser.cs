using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.SHARED
{
    public interface IIdentityUser : IUser<int>, System.Security.Principal.IIdentity, System.Security.Principal.IPrincipal
    {


    }
}
