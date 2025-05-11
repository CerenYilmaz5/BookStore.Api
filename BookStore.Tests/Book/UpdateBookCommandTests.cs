using BookStore.Api.Services.Implementations;
using Xunit;



namespace BookStore.Tests.Book
{
    public class UpdateBookCommandTests
    {
        private readonly FakeBookService _service;

        public UpdateBookCommandTests()
        {
            _service = new FakeBookService();
        }

        [Fact]
        public void Update_ExistingBook_ReturnsUpdatedBook()
        {
            // Arrange
            var existing = _service.GetAll().First();
            var updated = new Api.Models.Book
            {
                Title = "Updated Title",
                Author = "Updated Author",
                Price = 99,
                Stock = 20,
                PageCount = 400,
                PublishedDate = DateTime.Today.AddYears(-1)
            };

            // Act
            var result = _service.Update(existing.Id, updated);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Title", result.Title);
        }

        [Fact]
        public void Update_NonExistingBook_ReturnsNull()
        {
            // Arrange
            var updated = new Api.Models.Book
            {
                Title = "Doesn't matter",
                Author = "No One",
                Price = 10,
                Stock = 1,
                PageCount = 100,
                PublishedDate = DateTime.Today
            };

            // Act
            var result = _service.Update(-1, updated);

            // Assert
            Assert.Null(result);
        }
    }
}