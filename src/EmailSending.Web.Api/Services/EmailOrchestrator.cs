using System;
using System.Threading.Tasks;
using EmailSending.Web.Api.DataAccess;
using EmailSending.Web.Api.DataAccess.Entities;

namespace EmailSending.Web.Api.Services
{
    public class EmailOrchestrator : IEmailOrchestrator
    {
        private readonly IEmailsRepository _emailsRepository;

        public EmailOrchestrator(IEmailsRepository emailsRepository)
        {
            _emailsRepository = emailsRepository;
        }

        public Task SendEmail(Email email)
        {
            throw new NotImplementedException();
        }
    }

    public interface IEmailOrchestrator
    {
        Task SendEmail(Email email);
    }
}