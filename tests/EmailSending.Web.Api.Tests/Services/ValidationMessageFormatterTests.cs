using System;
using System.Collections.Generic;
using EmailSending.Web.Api.Validation;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace EmailSending.Web.Api.Tests.Services
{
    public class ValidationMessageFormatterTests
    {
        public ValidationMessageFormatterTests()
        {
            _sut = new ValidationMessageFormatter();
        }

        private readonly IValidationMessageFormatter _sut;

        [Fact]
        public void GetErrorMessage_Should_ContainEachFailure_When_SomeFailuresArePassed()
        {
            var failure1 = new ValidationFailure("test1", "failure on something");
            var failure2 = new ValidationFailure("test2", "failure on something else");
            var input = new List<ValidationFailure> {failure1, failure2};

            var result = _sut.GetErrorMessage(input);

            result.Should().Contain($"{failure1.PropertyName}- {failure1.ErrorMessage}");
            result.Should().Contain($"{failure2.PropertyName}- {failure2.ErrorMessage}");
        }


        [Fact]
        public void GetErrorMessage_Should_ReturnEmptyMessage_When_ListIsEmpty()
        {
            var input = new List<ValidationFailure>();

            var result = _sut.GetErrorMessage(input);
            result.Should().BeNullOrEmpty();
        }

        [Fact]
        public void GetErrorMessage_Should_ReturnEmptyMessage_When_ListIsEmpty2()
        {
            List<ValidationFailure> input = null;

            Action act = () => _sut.GetErrorMessage(input);

            act.Should().Throw<NullReferenceException>();
        }
    }
}