using BookStore.Api.Models;

namespace BookStore.Api.Services.Interfaces
{
    // This interface defines all operations that the BookService must implement
    public interface IBookService
    {
        IEnumerable<Book> GetAll();                // Retrieve all books
        Book? GetById(int id);                     // Get a specific book by its ID
        Book Create(Book book);                    // Add a new book
        Book? Update(int id, Book book);           // Update a book by ID
        Book? Patch(int id, Book book);            // Partially update a book
        bool Delete(int id);                       // Remove a book by ID
        IEnumerable<Book> Filter(string? title, string? sort); // Filter and sort books
    }
}