using BookStore.Api.DTOs;
using BookStore.Api.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace BookStore.Tests.Book
{
    public class GetBookDetailQueryValidatorTests
    {
        private readonly GetByIdRequestValidator _validator;

        public GetBookDetailQueryValidatorTests()
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
        public void Should_Not_Have_Error_When_Id_Is_Positive()
        {
            var model = new GetByIdRequest { Id = 1 };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}