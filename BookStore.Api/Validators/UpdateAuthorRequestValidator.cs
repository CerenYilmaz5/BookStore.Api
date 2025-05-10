using BookStore.Api.DTOs;
using FluentValidation;

namespace BookStore.Api.Validators
{
    // Validator for the UpdateAuthorRequest DTO
    public class UpdateAuthorRequestValidator : AbstractValidator<UpdateAuthorRequest>
    {
        public UpdateAuthorRequestValidator()
        {
            RuleFor(a => a.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(a => a.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(a => a.BirthDate)
                .LessThan(DateTime.Today).WithMessage("Birth date must be in the past.");
        }
    }
}