using BookStore.Api.Models;

namespace BookStore.Api.Services.Interfaces
{
    // Interface defining the contract for author service operations
    public interface IAuthorService
    {
        IEnumerable<Author> GetAll();
        Author? GetById(int id);
        Author Create(Author author);
        Author? Update(int id, Author author);
        bool Delete(int id);
        bool HasBooks(int authorId); // Used to check if the author has books before deleting
    }
}