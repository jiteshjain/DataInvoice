using System;
using Microsoft.AspNet.Identity;

namespace DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.Identity
{
    public class IdentityRole : IRole
    {
        public IdentityRole()
        {
            Id = 0.ToString();
        }

        public IdentityRole(string name)
            : this()
        {
            Name = name;
        }

        public IdentityRole(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
