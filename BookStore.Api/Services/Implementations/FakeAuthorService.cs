using BookStore.Api.Models;
using BookStore.Api.Services.Interfaces;

namespace BookStore.Api.Services.Implementations
{
    // In-memory fake implementation of the author service
    public class FakeAuthorService : IAuthorService
    {
        private readonly List<Author> _authors = new();
        private readonly IBookService _bookService;

        public FakeAuthorService(IBookService bookService)
        {
            _bookService = bookService;

            // Optional: seed authors here if desired
        }

        public IEnumerable<Author> GetAll() => _authors;

        public Author? GetById(int id) => _authors.FirstOrDefault(a => a.Id == id);

        public Author Create(Author author)
        {
            author.Id = _authors.Any() ? _authors.Max(a => a.Id) + 1 : 1;
            _authors.Add(author);
            return author;
        }

        public Author? Update(int id, Author author)
        {
            var existing = GetById(id);
            if (existing == null) return null;

            existing.FirstName = author.FirstName;
            existing.LastName = author.LastName;
            existing.BirthDate = author.BirthDate;

            return existing;
        }

        public bool Delete(int id)
        {
            if (HasBooks(id)) return false;

            var author = GetById(id);
            if (author == null) return false;

            _authors.Remove(author);
            return true;
        }

        public bool HasBooks(int authorId)
        {
            var author = GetById(authorId);
            if (author == null) return false;

            // Check if any books are authored by this author
            return _bookService.GetAll().Any(b => b.Author == $"{author.FirstName} {author.LastName}");
        }
    }
}