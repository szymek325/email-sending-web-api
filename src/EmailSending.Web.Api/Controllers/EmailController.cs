using System;
using System.Threading.Tasks;
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
        private readonly IEmailOrchestrator _emailOrchestrator;
        private readonly ILogger<EmailController> _logger;

        public EmailController(IEmailOrchestrator emailOrchestrator, ILogger<EmailController> logger)
        {
            _emailOrchestrator = emailOrchestrator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> Create(EmailInput dto)
        {
            var guid = Guid.NewGuid();
            try
            {
                await _emailOrchestrator.SendEmail(new Email(guid.ToString(), dto.To, dto.CC, dto.BCC, dto.Subject,
                    dto.Body));
                return new OkObjectResult(new {guid});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error occured");
                return new BadRequestObjectResult(new {guid, ex.Message});
            }
        }
    }
}