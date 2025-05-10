using BookStore.Api.Models;
using BookStore.Api.Services.Interfaces;

namespace BookStore.Api.Services.Implementations
{
    // This is a fake book service that stores book data in memory (not a real database).
    // It's mainly used for learning, testing, or simulating how real services work.
    public class FakeBookService : IBookService
    {
        private readonly List<Book> _books;

        public FakeBookService()
        {
            _books = new List<Book>
            {
                new Book { Id = 1, Title = "1984", Author = "George Orwell", Price = 45, Stock = 10, PageCount = 328, PublishedDate = new DateTime(1949, 6, 8) },
                new Book { Id = 2, Title = "Brave New World", Author = "Aldous Huxley", Price = 50, Stock = 5, PageCount = 311, PublishedDate = new DateTime(1932, 1, 1) }
            };
        }

        public IEnumerable<Book> GetAll() => _books;

        public Book? GetById(int id) => _books.FirstOrDefault(b => b.Id == id);

        public Book Create(Book book)
        {
            book.Id = _books.Any() ? _books.Max(b => b.Id) + 1 : 1;
            _books.Add(book);
            return book;
        }

        public Book? Update(int id, Book book)
        {
            var existing = GetById(id);
            if (existing == null) return null;

            existing.Title = book.Title;
            existing.Author = book.Author;
            existing.Price = book.Price;
            existing.Stock = book.Stock;
            existing.PageCount = book.PageCount;
            existing.PublishedDate = book.PublishedDate;

            return existing;
        }

        public Book? Patch(int id, Book book)
        {
            var existing = GetById(id);
            if (existing == null) return null;

            if (!string.IsNullOrEmpty(book.Title)) existing.Title = book.Title;
            if (!string.IsNullOrEmpty(book.Author)) existing.Author = book.Author;
            if (book.Price > 0) existing.Price = book.Price;
            if (book.Stock >= 0) existing.Stock = book.Stock;
            if (book.PageCount > 0) existing.PageCount = book.PageCount;
            if (book.PublishedDate != default) existing.PublishedDate = book.PublishedDate;

            return existing;
        }

        public bool Delete(int id)
        {
            var book = GetById(id);
            if (book == null) return false;
            _books.Remove(book);
            return true;
        }

        public IEnumerable<Book> Filter(string? title, string? sort)
        {
            var result = _books.Where(b =>
                string.IsNullOrEmpty(title) || b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

            return sort?.ToLower() switch
            {
                "title" => result.OrderBy(b => b.Title),
                "price" => result.OrderBy(b => b.Price),
                "date" => result.OrderBy(b => b.PublishedDate),
                _ => result
            };
        }
    }
}
