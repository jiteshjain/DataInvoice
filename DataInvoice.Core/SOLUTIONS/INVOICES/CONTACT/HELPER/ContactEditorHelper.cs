using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInvoice.SOLUTIONS.INVOICES.CONTACT.HELPER
{
    public class ContactEditorHelper
    {


        /*
        public static string ContactEditM(CONTACT.Contact contact)
        {
            CONTACT.Address adresse = null;// contact.AddressPrimary;
            if (contact != null) adresse = contact.AddressPrimary;

            if (contact == null) contact = new Contact();
            
            if (adresse == null) adresse = new Address();

            StringBuilder html = new StringBuilder();
            html.Append("<div class=\"row\">");
            html.Append("<div class=\"col-md-6\">");

            html.Append("<p>These assure the user will never enter invalid phone no, email or anything that has a pattern even without validations</p>");
            html.Append("<br />");
            html.Append(CivilityFieldEdit("civility", "civility", "Civilité", contact.Civility));
            html.Append(DefaultFieldEdit("LastName", "LastName", "Nom", contact.LastName));
            html.Append(DefaultFieldEdit("Firstname", "Firstname", "Prenom", contact.FirstName));
            html.Append(DefaultFieldEdit("Phone", "Phone", "Téléphone", contact.Phone));
            html.Append(DefaultFieldEdit("MobilePhone", "MobilePhone", "Téléphone Mobile", contact.MobilePhone));
            html.Append(DefaultFieldEdit("Mail", "Mail", "Mail", contact.Mail));



            html.Append(" </div>");

            html.Append(" <div class=\"col-md-6\">");

            html.Append(" <p>Do you forget small things? here is something that helps to automatically placed forgotten dollar signs, decimal places and even comma separates and many more!</p>");
            html.Append("<br />");

            html.Append(DefaultFieldEdit("Adresse1", "Adresse1", "Adresse", adresse.Adresse1));
            html.Append(DefaultFieldEdit("Adresse2", "Adresse2", "Complément Adresse", adresse.Adresse2));
            html.Append(DefaultFieldEdit("CodePostal", "CodePostal", "Code Postal", adresse.CodePostal));
            html.Append(DefaultFieldEdit("Ville", "Ville", "Ville", adresse.Ville));
            html.Append(DefaultFieldEdit("Pays", "Pays", "Pays", adresse.Pays));

            html.Append("</div>");
            html.Append("</div>");
            html.Append("<br /><br />");
            html.Append("<button class=\"btn btn-primary btn-block\" type=\"submit\">Enregistrer</button>");
            return html.ToString();
        }



        public static string DefaultFieldEdit(string id, string name, string label, string defaultValue = null, string Help = null)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<div class=\"form-group\">");
            html.AppendFormat("<label class=\"form-label\">{0}</label> <span class=\"help\">{1}</span>", label, Help);
            html.AppendFormat("<div class=\"controls\"> <input type=\"text\" class=\"form-control\" id=\"{0}\" name=\"{1}\" value=\"{2}\" ></div>", id, name, defaultValue);
            html.Append("</div>");
            return html.ToString();
        }

        public static string CivilityFieldEdit(string id, string name, string label, string defaultValue = null, string Help = null)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<div class=\"form-group\">");
            html.AppendFormat("<label class=\"form-label\">{0}</label> <span class=\"help\">{1}</span>", label, Help);
            html.Append("<div class=\"controls\">");
            html.AppendFormat("<select class=\"form-control\" id=\"{0}\" name=\"{1}\" >", id, name, defaultValue);
            html.AppendFormat("<option value='M' {0}>M</option>", NGLib.COMPONENTS.WEB.Tools.sayselected("M", defaultValue));
            html.AppendFormat("<option value='MME' {0}>MME</option>", NGLib.COMPONENTS.WEB.Tools.sayselected("MME", defaultValue));
            html.AppendFormat("<option value='METMME' {0}>M et MME</option>", NGLib.COMPONENTS.WEB.Tools.sayselected("METMME", defaultValue));
            html.AppendFormat("<option value='STE' {0}>Société</option>", NGLib.COMPONENTS.WEB.Tools.sayselected("STE", defaultValue));
            html.Append("</select>");
            html.Append("</div>");
            html.Append("</div>");

            return html.ToString();
        }

       

        */



    }
}
