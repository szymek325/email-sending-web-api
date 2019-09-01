using System;
using System.Threading.Tasks;
using EmailSending.Web.Api.DataAccess;
using EmailSending.Web.Api.DataAccess.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace EmailSending.Web.Api.Services
{
    public interface IEmailOrchestrator
    {
        Task SendEmail(Email email);
    }

    public class EmailOrchestrator : IEmailOrchestrator
    {
        private readonly IEmailsRepository _emailsRepository;
        private readonly ILogger<EmailOrchestrator> _logger;
        private readonly IMailMessageBuilder _mailMessageBuilder;
        private readonly ISmtpSender _smtpSender;
        private readonly IValidator<Email> _validator;

        public EmailOrchestrator(ILogger<EmailOrchestrator> logger, IEmailsRepository emailsRepository,
            IMailMessageBuilder mailMessageBuilder, ISmtpSender smtpSender, IValidator<Email> validator)
        {
            _logger = logger;
            _emailsRepository = emailsRepository;
            _mailMessageBuilder = mailMessageBuilder;
            _smtpSender = smtpSender;
            _validator = validator;
        }

        public async Task SendEmail(Email email)
        {
            var result = await _validator.ValidateAsync(email);
            if (!result.IsValid)
                throw new ArgumentException("todo");
            try
            {
                var message = _mailMessageBuilder.Create(email);
                await _smtpSender.Send(message);
                email.Success = true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when sending email");
                email.Success = false;
            }

            await _emailsRepository.Create(email);
        }
    }
}