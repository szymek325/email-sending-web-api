using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EmailSending.Web.Api.Configuration;
using EmailSending.Web.Api.DataAccess.Entities;
using Microsoft.Extensions.Options;

namespace EmailSending.Web.Api.SmtpEmailSending
{
    public interface ISmtpSender
    {
        Task Send(Email email);
    }

    public class SmtpSender : ISmtpSender
    {
        private readonly IMailMessageBuilder _mailMessageBuilder;
        private readonly IOptions<SmtpConfiguration> _smtpConfiguration;

        public SmtpSender(IMailMessageBuilder mailMessageBuilder, IOptions<SmtpConfiguration> smtpConfiguration)
        {
            _mailMessageBuilder = mailMessageBuilder;
            _smtpConfiguration = smtpConfiguration;
        }

        public async Task Send(Email email)
        {
            var mailMessage = _mailMessageBuilder.Create(email);
            var smtpClient = new SmtpClient(_smtpConfiguration.Value.Server, _smtpConfiguration.Value.Port)
            {
                EnableSsl = true,
                Credentials =
                    new NetworkCredential(_smtpConfiguration.Value.UserName, _smtpConfiguration.Value.Password)
            };
            await smtpClient.SendMailAsync(mailMessage);
            smtpClient.Dispose();
        }
    }
}