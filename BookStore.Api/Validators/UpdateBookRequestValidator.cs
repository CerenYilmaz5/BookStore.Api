using BookStore.Api.DTOs;
using FluentValidation;

namespace BookStore.Api.Validators
{
    // Validator class for UpdateBookRequest DTO using FluentValidation.
    public class UpdateBookRequestValidator : AbstractValidator<UpdateBookRequest>
    {
        public UpdateBookRequestValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Title is required.");

            RuleFor(b => b.Author)
                .NotEmpty().WithMessage("Author is required.");

            RuleFor(b => b.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(b => b.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");

            RuleFor(b => b.PageCount)
                .GreaterThan(0).WithMessage("Page count must be greater than zero.");

            RuleFor(b => b.PublishedDate)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Published date cannot be in the future.");
        }
    }
}