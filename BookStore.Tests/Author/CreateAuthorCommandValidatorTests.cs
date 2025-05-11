using BookStore.Api.DTOs;
using BookStore.Api.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace BookStore.Tests.Author
{
    public class CreateAuthorCommandValidatorTests
    {
        private readonly UpdateAuthorRequestValidator _validator;

        public CreateAuthorCommandValidatorTests()
        {
            _validator = new UpdateAuthorRequestValidator();
        }

        [Fact]
        public void Should_Have_Error_When_FirstName_Is_Empty()
        {
            var model = new UpdateAuthorRequest { FirstName = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Fact]
        public void Should_Have_Error_When_BirthDate_Is_Future()
        {
            var model = new UpdateAuthorRequest
            {
                FirstName = "A",
                LastName = "B",
                BirthDate = DateTime.Today.AddDays(1)
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.BirthDate);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Data_Is_Valid()
        {
            var model = new UpdateAuthorRequest
            {
                FirstName = "Jane",
                LastName = "Doe",
                BirthDate = new DateTime(1980, 1, 1)
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}