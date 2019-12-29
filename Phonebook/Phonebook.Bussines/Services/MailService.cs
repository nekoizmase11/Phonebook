using Phonebook.Presentation.Contracts.DataTransferObjects;
using Phonebook.Presentation.Contracts.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Services
{
    public class MailService : IMailService
    {
        public void SendEmail(EmailDto email)
        {
            MailMessage mailMessage = new MailMessage(email.From, email.To);
            mailMessage.Body = email.Body;

            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Send(mailMessage);
        }

        public void SendChangePasswordLink(ChangePasswordEmailDto mailInfo)
        {
            MailMessage mailMessage = new MailMessage("cusmirzver@gmail.com", mailInfo.emailTo);
            mailMessage.IsBodyHtml = true;

            mailMessage.Subject = "Password reset";

            StringBuilder emailBodyText = new StringBuilder();
            emailBodyText.Append("Dear " + mailInfo.emailTo + "<br/><br/>");
            emailBodyText.Append("Please click on the following link to reset your password: <br/>");
            emailBodyText.Append("http://localhost:50957/Account/ChangeForgottenPassword?guid=" + mailInfo.guid);
            emailBodyText.Append("<br/><br/>");

            mailMessage.Body = emailBodyText.ToString();


            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Send(mailMessage);
        }
    }
}
