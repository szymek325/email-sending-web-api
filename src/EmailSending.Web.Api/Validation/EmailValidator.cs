using EmailSending.Web.Api.DataAccess.Entities;
using FluentValidation;

namespace EmailSending.Web.Api.Validation
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();

            RuleFor(x => x.To).NotEmpty().NotNull().When(x =>
                (x.CC == null || x.CC != null && x.CC.Count == 0) &&
                (x.BCC == null || x.BCC != null && x.BCC.Count == 0));
            RuleForEach(x => x.To).EmailAddress().When(x => x.To != null);

            RuleFor(x => x.From).EmailAddress();

            RuleFor(x => x.CC).NotEmpty().NotNull().When(x =>
                (x.To == null || x.To != null && x.To.Count == 0) &&
                (x.BCC == null || x.BCC != null && x.BCC.Count == 0));
            RuleForEach(x => x.CC).EmailAddress().When(x => x.CC != null);

            RuleFor(x => x.BCC).NotEmpty().NotNull().When(x =>
                (x.To == null || x.To != null && x.To.Count == 0) &&
                (x.CC == null || x.CC != null && x.CC.Count == 0));
            RuleForEach(x => x.BCC).EmailAddress().When(x => x.BCC != null);

            RuleFor(x => x.Subject).NotNull().NotEmpty().When(m => string.IsNullOrEmpty(m.Body));
            RuleFor(x => x.Body).NotNull().NotEmpty().When(m => string.IsNullOrEmpty(m.Subject));
        }
    }
}