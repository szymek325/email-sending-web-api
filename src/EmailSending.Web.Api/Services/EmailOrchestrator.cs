using System;
using System.Threading.Tasks;
using EmailSending.Web.Api.DataAccess;
using EmailSending.Web.Api.DataAccess.Entities;
using FluentValidation;

namespace EmailSending.Web.Api.Services
{
    public class EmailOrchestrator : IEmailOrchestrator
    {
        private readonly IEmailsRepository _emailsRepository;
        private readonly ISmtpSender _smtpSender;
        private readonly IValidator<Email> _validator;

        public EmailOrchestrator(IEmailsRepository emailsRepository, ISmtpSender smtpSender,
            IValidator<Email> validator)
        {
            _emailsRepository = emailsRepository;
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
                await _smtpSender.Send(email);
                email.Success = true;
            }
            catch (Exception e)
            {
                email.Success = false;
            }

            await _emailsRepository.Create(email);
        }
    }

    public interface IEmailOrchestrator
    {
        Task SendEmail(Email email);
    }
}