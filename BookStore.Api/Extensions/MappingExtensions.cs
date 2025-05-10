using BookStore.Api.DTOs;
using BookStore.Api.Models;

namespace BookStore.Api.Extensions
{
    // Static extension class that handles conversion between entity models and DTOs for Book operations.
    public static class MappingExtensions
    {
        // Converts an UpdateBookRequest DTO into a Book entity.
        public static Book ToEntity(this UpdateBookRequest request)
        {
            return new Book
            {
                Title = request.Title,
                Author = request.Author,
                Price = request.Price,
                Stock = request.Stock,
                PageCount = request.PageCount,
                PublishedDate = request.PublishedDate
            };
        }

        // Converts a Book entity into a BookResponse DTO.
        public static BookResponse ToResponse(this Book book)
        {
            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                Stock = book.Stock,
                PageCount = book.PageCount,
                PublishedDate = book.PublishedDate
            };
        }
    }
}