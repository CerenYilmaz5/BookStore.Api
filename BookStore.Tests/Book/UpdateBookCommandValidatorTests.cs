using BookStore.Api.DTOs;
using BookStore.Api.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace BookStore.Tests.Book
{
    public class UpdateBookCommandValidatorTests
    {
        private readonly UpdateBookRequestValidator _validator;

        public UpdateBookCommandValidatorTests()
        {
            _validator = new UpdateBookRequestValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            var model = new UpdateBookRequest { Title = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Pass_When_All_Fields_Valid()
        {
            var model = new UpdateBookRequest
            {
                Title = "Valid Book",
                Author = "Valid Author",
                Price = 15,
                Stock = 10,
                PageCount = 200,
                PublishedDate = DateTime.Today.AddDays(-1)
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}