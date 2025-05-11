using BookStore.Api.Services.Implementations;
using Xunit;

namespace BookStore.Tests.Book
{
    public class GetBookDetailQueryTests
    {
        private readonly FakeBookService _service;

        public GetBookDetailQueryTests()
        {
            _service = new FakeBookService();
        }

        [Fact]
        public void GetById_ExistingBook_ReturnsBook()
        {
            // Arrange
            var existingId = _service.GetAll().First().Id;

            // Act
            var result = _service.GetById(existingId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingId, result.Id);
        }

        [Fact]
        public void GetById_NonExistingBook_ReturnsNull()
        {
            // Act
            var result = _service.GetById(-1);

            // Assert
            Assert.Null(result);
        }
    }
}