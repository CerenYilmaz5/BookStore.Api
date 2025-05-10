using BookStore.Api.DTOs;
using BookStore.Api.Models;

namespace BookStore.Api.Extensions
{
    // Provides mapping functionality between Book entities and DTOs
    public static class BookMappingExtensions
    {
        // Converts UpdateBookRequest DTO to Book entity
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

        // Converts Book entity to BookResponse DTO
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