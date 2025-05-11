using BookStore.Api.DTOs;
using BookStore.Api.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace BookStore.Tests.Genre
{
    public class DeleteGenreCommandValidatorTests
    {
        private readonly GetByIdRequestValidator _validator;

        public DeleteGenreCommandValidatorTests()
        {
            _validator = new GetByIdRequestValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            var model = new GetByIdRequest { Id = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            var model = new GetByIdRequest { Id = 1 };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}