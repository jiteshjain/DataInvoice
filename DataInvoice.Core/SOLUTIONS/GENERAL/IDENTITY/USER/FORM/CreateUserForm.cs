using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.FORM
{
    public class CreateUserForm
    {

        public string Pseudo { get; set; }

        public string Mail { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }        


        public int IDContact { get; set; }


        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Mail)) throw new Exception("Mail invalid");
            //if (string.IsNullOrWhiteSpace(Password)) throw new Exception("Password invalid");
        }


    }
}
