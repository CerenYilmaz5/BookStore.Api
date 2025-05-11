using BookStore.Api.DTOs;
using BookStore.Api.Validators;
using Xunit;

namespace BookStore.Tests.Book;

public class DeleteBookCommandValidatorTests
{
    [Fact]
    public void Validator_Should_Fail_For_Invalid_Id()
    {
        var validator = new GetByIdRequestValidator();
        var result = validator.Validate(new GetByIdRequest { Id = 0 });

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_Pass_For_Valid_Id()
    {
        var validator = new GetByIdRequestValidator();
        var result = validator.Validate(new GetByIdRequest { Id = 3 });

        Assert.True(result.IsValid);
    }
}