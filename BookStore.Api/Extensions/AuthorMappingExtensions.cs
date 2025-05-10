using BookStore.Api.DTOs;
using BookStore.Api.Models;

namespace BookStore.Api.Extensions
{
    // Provides mapping functionality between Author entities and DTOs
    public static class AuthorMappingExtensions
    {
        // Converts UpdateAuthorRequest DTO to Author entity
        public static Author ToEntity(this UpdateAuthorRequest request)
        {
            return new Author
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate
            };
        }

        // Converts Author entity to AuthorResponse DTO
        public static AuthorResponse ToResponse(this Author author)
        {
            return new AuthorResponse
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate
            };
        }
    }
}