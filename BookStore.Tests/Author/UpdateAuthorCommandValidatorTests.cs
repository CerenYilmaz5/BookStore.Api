using BookStore.Api.DTOs;
using BookStore.Api.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace BookStore.Tests.Author
{
    public class UpdateAuthorCommandValidatorTests
    {
        private readonly UpdateAuthorRequestValidator _validator;

        public UpdateAuthorCommandValidatorTests()
        {
            _validator = new UpdateAuthorRequestValidator();
        }

        [Fact]
        public void Should_Have_Error_When_LastName_Is_Empty()
        {
            var model = new UpdateAuthorRequest
            {
                FirstName = "Test",
                LastName = "",
                BirthDate = DateTime.Today.AddYears(-30)
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.LastName);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid()
        {
            var model = new UpdateAuthorRequest
            {
                FirstName = "Valid",
                LastName = "Author",
                BirthDate = DateTime.Today.AddYears(-40)
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}