using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;

namespace EmailSending.Web.Api.Validation
{
    public interface IValidationMessageFormatter
    {
        string GetErrorMessage(IEnumerable<ValidationFailure> validationFailures);
    }

    public class ValidationMessageFormatter : IValidationMessageFormatter
    {
        public string GetErrorMessage(IEnumerable<ValidationFailure> validationFailures)
        {
            var builder = new StringBuilder();
            foreach (var validationFailure in validationFailures)
                builder.AppendLine($"{validationFailure.PropertyName}- {validationFailure.ErrorMessage}");
            return builder.ToString();
        }
    }
}