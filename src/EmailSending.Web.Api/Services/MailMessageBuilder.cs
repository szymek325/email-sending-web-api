using System.Linq;
using System.Net.Mail;
using EmailSending.Web.Api.DataAccess.Entities;

namespace EmailSending.Web.Api.Services
{
    public interface IMailMessageBuilder
    {
        MailMessage Create(Email email);
    }

    public class MailMessageBuilder : IMailMessageBuilder
    {
        public MailMessage Create(Email email)
        {
            var mailMessage = new MailMessage();
            email.To.ToList().ForEach(x => mailMessage.To.Add(new MailAddress(x)));
            email.CC.ToList().ForEach(x => mailMessage.CC.Add(new MailAddress(x)));
            email.BCC.ToList().ForEach(x => mailMessage.Bcc.Add(new MailAddress(x)));
            mailMessage.Subject = email.Subject;
            mailMessage.Body = email.Body;
            mailMessage.IsBodyHtml = true;
            return mailMessage;
        }
    }
}