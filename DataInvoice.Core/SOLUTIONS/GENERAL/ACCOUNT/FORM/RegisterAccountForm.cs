using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.GENERAL.ACCOUNT.FORM
{
    public class RegisterAccountForm
    {

        
        

        public string CompagnyCountryCode { get; set; }
        public string CompagnyName { get; set; }
        public string CompanyAdress1 { get; set; }
        public string Companyadress2 { get; set; }
        public string CompanypostCode { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyCountry { get; set; }


        public RegisterAccountUserForm User { get; set; }


        /// <summary>
        /// Possiblité d'envoyer des publicité au client
        /// </summary>
        public bool AllowAdvertising { get; set; }


        public void FromPo(Account item)
        {

        }


        public void ToPo(Account item)
        {

        }


        //  et FromSirenAdress ( récupérer a partir du poco de API l'api SIREN)

    }



    /// <summary>
    /// pour Créer l'utilisateur à partir de la création de compte
    /// </summary>
    public class RegisterAccountUserForm
    {
        public string UserMail { get; set; }
        public string UserPassword { get; set; }
        public string UserConfirmation { get; set; }

        public void FromPo(GENERAL.IDENTITY.USER.LocalUser item)
        {

        }


        public void ToPo(GENERAL.IDENTITY.USER.LocalUser item)
        {

        }

    }





}
