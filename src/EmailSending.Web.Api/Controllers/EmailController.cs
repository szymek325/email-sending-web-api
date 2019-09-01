using System;
using System.Threading.Tasks;
using EmailSending.Web.Api.CustomExceptions;
using EmailSending.Web.Api.DataAccess;
using EmailSending.Web.Api.DataAccess.Entities;
using EmailSending.Web.Api.Dto;
using EmailSending.Web.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailSending.Web.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailsRepository _emailsRepository;
        private readonly ILogger<EmailController> _logger;
        private readonly IRequestOrchestrator _requestOrchestrator;

        public EmailController(IEmailsRepository emailsRepository, ILogger<EmailController> logger,
            IRequestOrchestrator requestOrchestrator)
        {
            _emailsRepository = emailsRepository;
            _logger = logger;
            _requestOrchestrator = requestOrchestrator;
        }

        [HttpPost]
        public async Task<ActionResult> Create(EmailInput dto)
        {
            var guid = Guid.NewGuid();
            try
            {
                await _requestOrchestrator.SendEmail(new Email(guid.ToString(), dto.To, dto.CC, dto.BCC, dto.Subject,
                    dto.Body));
                return new OkObjectResult(new {guid});
            }
            catch (InvalidInputException validationException)
            {
                var exceptionType = validationException.GetType();
                return new BadRequestObjectResult(new {exceptionType, validationException.Message});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error occured");
                return new BadRequestObjectResult(new {guid, ex.Message});
            }
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var emails = await _emailsRepository.GetAll();
            return new OkObjectResult(emails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var emails = await _emailsRepository.Get(id.ToString());
            return new OkObjectResult(emails);
        }
    }
}