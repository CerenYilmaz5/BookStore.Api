using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace BookStore.Tests.Genre
{
    public class UpdateGenreRequest
    {
        public string Name { get; set; } = "";
    }

    public class UpdateGenreRequestValidator : AbstractValidator<UpdateGenreRequest>
    {
        public UpdateGenreRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Genre name is required.")
                .MinimumLength(2).WithMessage("Genre name must be at least 2 characters.");
        }
    }

    public class UpdateGenreCommandValidatorTests
    {
        private readonly UpdateGenreRequestValidator _validator;

        public UpdateGenreCommandValidatorTests()
        {
            _validator = new UpdateGenreRequestValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new UpdateGenreRequest { Name = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Name_Is_Valid()
        {
            var model = new UpdateGenreRequest { Name = "Action" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}