using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataInvoice.Manager.Models
{
    public class RegisterSmallForm
    {

        public string Pseudo { get; set; }

        public string Mail { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }


        public string PostCode { get; set; }


        public string City { get; set; }



        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Mail) && string.IsNullOrWhiteSpace(Phone)) ;
            if (string.IsNullOrWhiteSpace(Pseudo)) throw new Exception("Pseudo Absent");
            if (string.IsNullOrWhiteSpace(Mail)) throw new Exception("Mail Absent");
            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(PasswordConfirm)) throw new Exception("Mot de passe invalide");
            if (!Password.Equals(PasswordConfirm)) throw new Exception("Confirmation du mot de passe invalide");

        }



        public DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.FORM.CreateUserForm ToCreateUserForm()
        {
            DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.FORM.CreateUserForm form = new DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.FORM.CreateUserForm();
            form.Mail = this.Mail;
            form.Pseudo = this.Pseudo;
            form.Phone = this.Phone;
            form.Password = this.Password;

            return form;
        }



    }
}