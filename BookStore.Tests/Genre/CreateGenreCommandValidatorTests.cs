using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace BookStore.Tests.Genre
{
    public class CreateGenreRequest
    {
        public string Name { get; set; } = "";
    }

    public class CreateGenreRequestValidator : AbstractValidator<CreateGenreRequest>
    {
        public CreateGenreRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Genre name is required.")
                .MinimumLength(2).WithMessage("Genre name must be at least 2 characters.");
        }
    }

    public class CreateGenreCommandValidatorTests
    {
        private readonly CreateGenreRequestValidator _validator;

        public CreateGenreCommandValidatorTests()
        {
            _validator = new CreateGenreRequestValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new CreateGenreRequest { Name = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Name_Is_Valid()
        {
            var model = new CreateGenreRequest { Name = "Drama" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}