using System;
using System.Threading.Tasks;
using EmailSending.Web.Api.CustomExceptions;
using EmailSending.Web.Api.DataAccess;
using EmailSending.Web.Api.DataAccess.Entities;
using EmailSending.Web.Api.SmtpEmailSending;
using EmailSending.Web.Api.Validation;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace EmailSending.Web.Api.Services
{
    public interface IRequestOrchestrator
    {
        Task SendEmail(Email email);
    }

    public class RequestOrchestrator : IRequestOrchestrator
    {
        private readonly IEmailsRepository _emailsRepository;
        private readonly ILogger<RequestOrchestrator> _logger;
        private readonly ISmtpSender _smtpSender;
        private readonly IValidationMessageFormatter _validationMessageFormatter;
        private readonly IValidator<Email> _validator;

        public RequestOrchestrator(IEmailsRepository emailsRepository, ILogger<RequestOrchestrator> logger,
            ISmtpSender smtpSender, IValidationMessageFormatter validationMessageFormatter, IValidator<Email> validator)
        {
            _emailsRepository = emailsRepository;
            _logger = logger;
            _smtpSender = smtpSender;
            _validationMessageFormatter = validationMessageFormatter;
            _validator = validator;
        }

        public async Task SendEmail(Email email)
        {
            var result = await _validator.ValidateAsync(email);
            if (!result.IsValid)
            {
                var validationMessage = _validationMessageFormatter.GetErrorMessage(result.Errors);
                throw new InvalidInputException(validationMessage);
            }

            try
            {
                await _smtpSender.Send(email);
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