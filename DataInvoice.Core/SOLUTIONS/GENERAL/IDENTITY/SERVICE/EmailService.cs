using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DataInvoice.SOLUTIONS.GENERAL.IDENTITY.USER.Identity;

namespace DataInvoice.SOLUTIONS.GENERAL.IDENTITY.SERVICE
{
    public class EmailService : Microsoft.AspNet.Identity.IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            NGLib.COMPONENTS.NET.MailSender sender = new NGLib.COMPONENTS.NET.MailSender();
            var mailMessage = sender.Create(message.Subject, message.Body, message.Destination);
            mailMessage.IsBodyHtml = true;
            sender.SendMail(mailMessage);
            return Task.FromResult(0);
        }






        public static string ContentMailNewUser(ApplicationUser user, string token = null)
        {
            try
            {
                string websiteUrl = "https://www.datainvoice.com";

                StringBuilder mailtext = new StringBuilder();

                mailtext.AppendLine("Bienvenue sur DataInvoice. <br /><br />");
                mailtext.AppendLine("<br />");
                mailtext.AppendLine("Pour poursuivre votre inscription vous devez valider votre mail en cliquant sur le lien ci-dessous.<br /><br />");

                string link = websiteUrl + "/Login/ValidateMail/" + "{{otp}}";
                mailtext.AppendFormat("<a href='{0}'>{0}</a>", link);
                mailtext.AppendLine("<br />");

                mailtext.AppendLine("<br />");
                mailtext.AppendLine("Cordialement,<br /> L'équipe de DataInvoice. <br /><br />");

                return mailtext.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool SendMail(String Email)
        {
            ApplicationUser myuser = new ApplicationUser();
            string token = null;
            try
            {
                string mailtext = ContentMailRenewPassword(myuser, token,Email);

                NGLib.COMPONENTS.NET.MailSender sender = new NGLib.COMPONENTS.NET.MailSender();
                var fromAddress = new MailAddress("rayhanegueddari@gmail.com", "From Name");
                var toAddress = new MailAddress(Email, "To Name");
                string fromPassword = "azertyuiop12345678910wxyz";
                string subject = "Groupe DataInvoice";
                string body = mailtext;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })

                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SendMail(String Email,String token)
        {
            ApplicationUser myuser = new ApplicationUser();
            try
            {
                string mailtext = ContentMailRenewPassword(myuser, token,Email);

                NGLib.COMPONENTS.NET.MailSender sender = new NGLib.COMPONENTS.NET.MailSender();
                var fromAddress = new MailAddress("rayhanegueddari@gmail.com", "From Name");
                var toAddress = new MailAddress(Email, "To Name");
                string fromPassword = "11/06/1991+22/08/1990";
                string subject = "Groupe DataInvoice";
                string body = mailtext;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })

                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ContentMailRenewPassword(ApplicationUser user, string token,String Email)
        {
           
            try
            {
                // string websiteUrl = "https://www.datainvoice.com";
                string websiteUrl = "http://localhost:18745";

                StringBuilder mailtext = new StringBuilder();
                mailtext.AppendLine("Bonjour " + user.GetString("pseudo") + ". <br/><br />");
                mailtext.AppendLine("Vous venez de faire une demande pour renouvler votre mot de passe sur .<br /><br />");
                mailtext.AppendLine("Si vous n'êtes pas à l'origine de cette demande veuillez ignorer ce mail.<br /><br />");
                // string link = websiteUrl + "/Login/Renewpassword/" + "{{otp}}";
                string link = websiteUrl + "/Login/Renewpassword?token=" + token+ "&email="+Email;
                mailtext.AppendFormat("<a href='{0}'>{0}</a>", link);
                mailtext.AppendLine("<br />");

                mailtext.AppendLine("<br />");
                mailtext.AppendLine("Cordialement,<br /> L'équipe de DataInvoice. <br /><br />");

                return mailtext.ToString();

            }
            catch (Exception)
            {
                throw;
            }
        }










    }
}
