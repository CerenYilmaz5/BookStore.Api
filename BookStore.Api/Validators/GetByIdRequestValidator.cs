using BookStore.Api.DTOs;
using FluentValidation;

namespace BookStore.Api.Validators
{
    // Validator to ensure ID is positive for GetById and Delete operations
    public class GetByIdRequestValidator : AbstractValidator<GetByIdRequest>
    {
        public GetByIdRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than 0.");
        }
    }
}