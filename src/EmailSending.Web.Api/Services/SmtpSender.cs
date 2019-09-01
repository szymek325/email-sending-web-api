using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EmailSending.Web.Api.Configuration;
using Microsoft.Extensions.Options;

namespace EmailSending.Web.Api.Services
{
    public interface ISmtpSender
    {
        Task Send(MailMessage mailMessage);
    }

    public class SmtpSender : ISmtpSender
    {
        private readonly IOptions<SmtpConfiguration> _smtpConfiguration;

        public SmtpSender(IOptions<SmtpConfiguration> smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration;
        }

        public async Task Send(MailMessage mailMessage)
        {
            var smtpClient = new SmtpClient(_smtpConfiguration.Value.Server, _smtpConfiguration.Value.Port)
            {
                Credentials =
                    new NetworkCredential(_smtpConfiguration.Value.UserName, _smtpConfiguration.Value.Password)
            };
            await smtpClient.SendMailAsync(mailMessage);
            smtpClient.Dispose();
        }
    }
}