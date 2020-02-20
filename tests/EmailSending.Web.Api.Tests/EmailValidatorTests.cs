using System;
using System.Collections.Generic;
using EmailSending.Web.Api.DataAccess.Entities;
using EmailSending.Web.Api.Validation;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace EmailSending.Web.Api.Tests
{
    public class EmailValidatorTests
    {
        public EmailValidatorTests()
        {
            _sut = new EmailValidator();
        }

        private readonly IValidator<Email> _sut;

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_Should_ReturnValid_When_EverythingIsFilled1(string body)
        {
            var email = new Email(Guid.NewGuid().ToString(), new List<string> {"mat@gmail.com"}, null, null, "test",
                body);

            var result = _sut.Validate(email);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_Should_ReturnValid_When_EverythingIsFilled2(string body)
        {
            var email = new Email(Guid.NewGuid().ToString(), new List<string> {"mat@gmail.com"}, null, null, body,
                "mejl body");

            var result = _sut.Validate(email);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_Should_ReturnValid_When_EverythingIsFilled3(string body)
        {
            var email = new Email(Guid.NewGuid().ToString(), null, new List<string> {"mat@gmail.com"}, null, "test",
                body);

            var result = _sut.Validate(email);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_Should_ReturnValid_When_EverythingIsFilled4(string body)
        {
            var email = new Email(Guid.NewGuid().ToString(), null, new List<string> {"mat@gmail.com"}, null, body,
                "mejl body");

            var result = _sut.Validate(email);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_Should_ReturnErrors_When_GuidIsEmpty()
        {
            var email = new Email("", null, null, null, "", "");

            var result = _sut.Validate(email);

            result.IsValid.Should().BeFalse();
        }
    }
}