using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using EmailSending.Web.Api.DataAccess.Entities;

namespace EmailSending.Web.Api.Services
{
    public interface ISmtpSender
    {
        Task Send(Email email);
    }

    public class SmtpSender
    {
        public async Task Send(Email email)
        {
            //TODO inject smtp configuratgion
            var sm = new SmtpClient("dasdsa");
            var mailMessage = new MailMessage();
            email.To.ToList().ForEach(x => mailMessage.To.Add(new MailAddress(x)));
            email.CC.ToList().ForEach(x => mailMessage.CC.Add(new MailAddress(x)));
            email.BCC.ToList().ForEach(x => mailMessage.Bcc.Add(new MailAddress(x)));
            mailMessage.Subject = email.Subject;
            mailMessage.Body = email.Body;
            mailMessage.IsBodyHtml = true;
            await sm.SendMailAsync(mailMessage);
        }
    }
}