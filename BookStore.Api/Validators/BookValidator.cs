using BookStore.Api.Models;
using FluentValidation;

namespace BookStore.Api.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.Title).NotEmpty().WithMessage("Book title is required.");
            RuleFor(b => b.Author).NotEmpty().WithMessage("Author is required.");
            RuleFor(b => b.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(b => b.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");
            RuleFor(b => b.PageCount).GreaterThan(0).WithMessage("Page count must be greater than zero.");
            RuleFor(b => b.PublishedDate).LessThanOrEqualTo(DateTime.Today).WithMessage("Published date cannot be in the future.");
        }
    }
}