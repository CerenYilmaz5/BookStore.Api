using BookStore.Api.Services.Implementations;
using Xunit;

namespace BookStore.Tests.Book;

public class DeleteBookCommandTests
{
    [Fact]
    public void DeleteBook_WhenBookExists_ReturnsTrue()
    {
        // Arrange
        var service = new FakeBookService();
        var existingBook = service.GetAll().First();

        // Act
        var result = service.Delete(existingBook.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteBook_WhenBookDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var service = new FakeBookService();
        int nonExistentId = 999;

        // Act
        var result = service.Delete(nonExistentId);

        // Assert
        Assert.False(result);
    }
}