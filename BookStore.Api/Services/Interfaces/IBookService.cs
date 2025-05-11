using BookStore.Api.Models;

namespace BookStore.Api.Services.Interfaces
{
    // Interface that defines all operations related to managing books.
    // These are the contracts that any BookService implementation must fulfill.
    public interface IBookService
    {
        // Returns all books in the system.
        IEnumerable<Book> GetAll();

        // Returns a specific book by its ID, or null if not found.
        Book? GetById(int id);

        // Creates a new book and returns the created entity.
        Book Create(Book book);

        // Updates an existing book by ID. Returns the updated book or null.
        Book? Update(int id, Book book);

        // Applies partial updates to a book. Returns the updated book or null.
        Book? Patch(int id, Book book);

        // Deletes a book by ID. Returns true if deleted successfully.
        bool Delete(int id);

        // Filters and optionally sorts the list of books.
        IEnumerable<Book> Filter(string? title, string? sort, string? genre = null);

    }
}