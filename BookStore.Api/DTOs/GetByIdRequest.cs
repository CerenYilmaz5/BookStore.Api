namespace BookStore.Api.DTOs
{
    // This DTO is used to encapsulate the ID for validating
    // GetById or Delete actions using FluentValidation.
    public class GetByIdRequest
    {
        // The ID of the book to retrieve or delete
        public int Id { get; set; }
    }
}